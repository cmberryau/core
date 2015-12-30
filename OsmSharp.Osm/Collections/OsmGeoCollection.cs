// OsmSharp - OpenStreetMap (OSM) SDK
// Copyright (C) 2013 Abelshausen Ben
// 
// This file is part of OsmSharp.
// 
// OsmSharp is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// OsmSharp is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with OsmSharp. If not, see <http://www.gnu.org/licenses/>.

using OsmSharp.Osm.Data;
using OsmSharp.Osm.Streams;
using System;
using System.Collections.Generic;

namespace OsmSharp.Osm.Collections
{
    /// <summary>
    /// A lightweight collection of geometries, basically a wrapper for 3 dictionaries
    /// </summary>
    public class OsmGeoCollection : OsmStreamSource, IEnumerable<OsmGeo>, IOsmGeoSource
    {
        /// <summary>
        /// Returns true if the OsmGeoCollection can be reset
        /// </summary>
        public override bool CanReset
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// The total count of geometries
        /// </summary>
        public long Count
        {
            get
            {
                return Nodes.Count + Ways.Count + Relations.Count;
            }
        }

        /// <summary>
        /// The dictionary of nodes contained in the collections
        /// </summary>
        public Dictionary<long, Node> Nodes
        {
            get;
            private set;
        }

        /// <summary>
        /// The dictionary of ways contained in the collections
        /// </summary>
        public Dictionary<long, Way> Ways
        {
            get;
            private set;
        }

        /// <summary>
        /// The dictionary of relations contained in the collections
        /// </summary>
        public Dictionary<long, Relation> Relations
        {
            get;
            private set;
        }

        private IEnumerator<KeyValuePair<long, Node>> _node_enumerator;
        private IEnumerator<KeyValuePair<long, Way>> _way_enumerator;
        private IEnumerator<KeyValuePair<long, Relation>> _relation_enumerator;
        private OsmGeo _current;

        /// <summary>
        /// Creates a new OsmGeoCollection object
        /// </summary>
        public OsmGeoCollection()
        {
            Nodes = new Dictionary<long, Node>();
            Ways = new Dictionary<long, Way>();
            Relations = new Dictionary<long, Relation>();
        }

        /// <summary>
        /// Initializes the OsmGeoCollection object
        /// </summary>
        public override void Initialize()
        {
            _node_enumerator = Nodes.GetEnumerator();
            _way_enumerator = Ways.GetEnumerator();
            _relation_enumerator = Relations.GetEnumerator();

            _node_enumerator.Reset();
            _way_enumerator.Reset();
            _relation_enumerator.Reset();

            _current = null;
        }

        /// <summary>
        /// Returns the the node with the given id
        /// </summary>
        /// <param name="id">The id to use for lookup</param>
        /// <returns>The node if it exists in the collection, null otherwise</returns>
        public Node GetNode(long id)
        {
            Node node = null;
            Nodes.TryGetValue(id, out node);

            return node;
        }

        /// <summary>
        /// Returns the the way with the given id
        /// </summary>
        /// <param name="id">The id to use for lookup</param>
        /// <returns>The way if it exists in the collection, null otherwise</returns>
        public Way GetWay(long id)
        {
            Way way = null;
            Ways.TryGetValue(id, out way);

            return way;
        }

        /// <summary>
        /// Returns the the relation with the given id
        /// </summary>
        /// <param name="id">The id to use for lookup</param>
        /// <returns>The relation if it exists in the collection, null otherwise</returns>
        public Relation GetRelation(long id)
        {
            Relation relation = null;
            Relations.TryGetValue(id, out relation);

            return relation;
        }

        /// <summary>
        /// Completes the collection, fetching optional missing elements
        /// </summary>
        /// <param name="source">The source to search for missing elements from</param>
        /// <param name="relation_nodes">Find missing nodes from relations?</param>
        /// <param name="relation_ways">Find missing ways from relations?</param>
        /// <param name="relation_relations">Find missing relations from relations?</param>
        public OsmGeoCollection Complete(IDataSourceReadOnly source,
                                         bool relation_nodes = false,
                                         bool relation_ways = false,
                                         bool relation_relations = false)
        {
            var missing_node_ids = new List<long>();
            var missing_way_ids = new List<long>();

            // search relations for missing relations
            if (relation_relations)
            {
                var missing_relation_ids = new List<long>();

                do
                {
                    foreach (var relation in Relations)
                    {
                        foreach (var member in relation.Value.Members)
                        {
                            if (member.MemberType.Value == OsmGeoType.Relation)
                            {
                                if (!Relations.ContainsKey(member.MemberId.Value))
                                {
                                    missing_relation_ids.Add(member.MemberId.Value);
                                }
                            }
                        }
                    }

                    var found_relations = source.GetRelations(missing_relation_ids);

                    foreach (var found_relation in found_relations)
                    {
                        if (!Relations.ContainsKey(found_relation.Id.Value))
                        {
                            Relations.Add(found_relation.Id.Value, found_relation);
                        }
                    }

                } while (missing_relation_ids.Count > 0);
            }

            // search relations for missing ways and nodes
            if (relation_ways || relation_nodes)
            {
                foreach (var relation in Relations)
                {
                    foreach (var member in relation.Value.Members)
                    {
                        if (member.MemberType.Value == OsmGeoType.Node)
                        {
                            if (relation_nodes)
                            {
                                if (!Nodes.ContainsKey(member.MemberId.Value))
                                {
                                    missing_node_ids.Add(member.MemberId.Value);
                                }
                            }
                        }
                        else if (member.MemberType.Value == OsmGeoType.Way)
                        {
                            if (relation_ways)
                            {
                                if (!Ways.ContainsKey(member.MemberId.Value))
                                {
                                    missing_way_ids.Add(member.MemberId.Value);
                                }
                            }
                        }
                        else if (member.MemberType.Value == OsmGeoType.Relation)
                        {
                            if (relation_relations)
                            {
                                if (!Relations.ContainsKey(member.MemberId.Value))
                                {
                                    throw new NotImplementedException();
                                }
                            }
                        }
                    }
                }
            }

            // fetch missing ways
            var found_ways = source.GetWays(missing_way_ids);
            foreach (var found_way in found_ways)
            {
                Ways.Add(found_way.Id.Value, found_way);
            }

            // search ways for missing nodes
            foreach (var way in Ways)
            {
                foreach (var node_id in way.Value.Nodes)
                {
                    if (!Nodes.ContainsKey(node_id))
                    {
                        missing_node_ids.Add(node_id);
                    }
                }
            }

            // fetch missing nodes
            var found_nodes = source.GetNodes(missing_node_ids);
            foreach (var found_node in found_nodes)
            {
                Nodes.Add(found_node.Id.Value, found_node);
            }

            return this;
        }

        /// <summary>
        /// Returns the IEnumerator
        /// </summary>
        /// <returns></returns>
        public new IEnumerator<OsmGeo> GetEnumerator()
        {
            foreach (var node in Nodes)
            {
                yield return node.Value;
            }

            foreach (var way in Ways)
            {
                yield return way.Value;
            }

            foreach (var relation in Relations)
            {
                yield return relation.Value;
            }
        }

        /// <summary>
        /// Moves to the next OsmGeo with optional ignores
        /// </summary>
        /// <param name="ignore_nodes">Ignore nodes for the move</param>
        /// <param name="ignore_ways">Ignore ways for the move</param>
        /// <param name="ignore_relations">Ignore relations for the move</param>
        public override bool MoveNext(bool ignore_nodes, bool ignore_ways, bool ignore_relations)
        {
            var result = false;

            if (!ignore_nodes)
            {
                result = _node_enumerator.MoveNext();

                if (result)
                {
                    _current = _node_enumerator.Current.Value;

                    return result;
                }
            }

            if (!ignore_ways)
            {
                result = _way_enumerator.MoveNext();

                if (result)
                {
                    _current = _way_enumerator.Current.Value;

                    return result;
                }
            }

            if (!ignore_relations)
            {
                result = _relation_enumerator.MoveNext();

                if (result)
                {
                    _current = _relation_enumerator.Current.Value;

                    return result;
                }
            }

            return result;
        }

        /// <summary>
        /// The current OsmGeo
        /// </summary>
        public override OsmGeo Current()
        {
            return _current;
        }

        /// <summary>
        /// Resets the enumeration
        /// </summary>
        public override void Reset()
        {
            _node_enumerator.Reset();
            _way_enumerator.Reset();
            _relation_enumerator.Reset();

            _current = null;
        }
    }
}
