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

using System;
using System.Collections.Generic;

namespace OsmSharp.Osm.Cache
{
    /// <summary>
    /// An osm data cache for simple OSM objects kept in memory.
    /// </summary>
    public class OsmDataCacheMemory : OsmDataCache
    {
        /// <summary>
        /// The number of nodes held in the cache
        /// </summary>
        public override int NodeCount
        {
            get
            {
                return Nodes.Count;
            }
        }

        /// <summary>
        /// The number of ways held in the cache
        /// </summary>
        public override int WayCount
        {
            get
            {
                return Ways.Count;
            }
        }

        /// <summary>
        /// The number of relations held in the cache
        /// </summary>
        public override int RelationCount
        {
            get
            {
                return Relations.Count;
            }
        }

        /// <summary>
        /// The dictionary of all nodes in the cache
        /// </summary>
        protected readonly IDictionary<long, Node> Nodes;

        /// <summary>
        /// The dictionary of all ways in the cache
        /// </summary>
        protected readonly IDictionary<long, Way> Ways;

        /// <summary>
        /// The dictionary of all relations in the cache
        /// </summary>
        protected readonly IDictionary<long, Relation> Relations;

        /// <summary>
        /// Creates a new instance of OsmDataCacheMemory
        /// </summary>
        public OsmDataCacheMemory()
        {
            Nodes = new Dictionary<long, Node>();
            Ways = new Dictionary<long, Way>();
            Relations = new Dictionary<long, Relation>();
        }

        /// <summary>
        /// Adds a node
        /// </summary>
        public override void AddNode(Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (node.Id == null)
            {
                throw new Exception("node.Id is null");
            }

            Nodes[node.Id.Value] = node;
        }

        /// <summary>
        /// Adds a list of nodes
        /// </summary>
        public override void AddNodes(IList<Node> nodes)
        {
            if (nodes == null)
            {
                throw new ArgumentNullException("nodes");
            }

            foreach (var node in nodes)
            {
                if (!Nodes.ContainsKey(node.Id.Value))
                {
                    Nodes[node.Id.Value] = node;
                }
            }
        }

        /// <summary>
        /// Adds a dictionary of node ids and nodes
        /// </summary>
        public override void AddNodes(IDictionary<long, Node> nodes)
        {
            if (nodes == null)
            {
                throw new ArgumentNullException("nodes");
            }

            foreach (var node in nodes)
            {
                if (!Nodes.ContainsKey(node.Key))
                {
                    Nodes[node.Key] = node.Value;
                }
            }
        }

        /// <summary>
        /// Returns a dictionary of fetched nodes given the ids
        /// </summary>
        /// <param name="ids">The ids of the nodes to fetch</param>
        /// <param name="remaining_ids">The ids that were unable to be found</param>
        public override IDictionary<long, Node> GetNodes(IList<long> ids, out IList<long> remaining_ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException("ids");
            }

            var nodes = new Dictionary<long, Node>();
            remaining_ids = new List<long>();

            foreach (var id in ids)
            {
                if (Nodes.ContainsKey(id))
                {
                    nodes[id] = Nodes[id];
                }
                else
                {
                    remaining_ids.Add(id);
                }
            }

            return nodes;
        }

        /// <summary>
        /// Returns a list of fetched nodes given the ids
        /// </summary>
        /// <param name="ids">The ids of the nodes to fetch</param>
        /// <param name="remaining_ids">The ids that were unable to be found</param>
        public override IList<Node> GetNodesList(IList<long> ids, out IList<long> remaining_ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException("ids");
            }

            var nodes = new List<Node>();
            remaining_ids = new List<long>();

            foreach (var id in ids)
            {
                if (Nodes.ContainsKey(id))
                {
                    if (!nodes.Contains(Nodes[id]))
                    {
                        nodes.Add(Nodes[id]);
                    }
                }
                else
                {
                    remaining_ids.Add(id);
                }
            }

            return nodes;
        }

        /// <summary>
        /// Removes the node with the given id
        /// </summary>
        public override bool RemoveNode(long id)
        {
            return Nodes.Remove(id);
        }

        /// <summary>
        /// Returns true if the node exists
        /// </summary>
        public override bool ContainsNode(long id)
        {
            return Nodes.ContainsKey(id);
        }

        /// <summary>
        /// Returns true if the node given the id was successfully fetched
        /// </summary>
        public override bool TryGetNode(long id, out Node node)
        {
            return Nodes.TryGetValue(id, out node);
        }

        /// <summary>
        /// Adds a way
        /// </summary>
        public override void AddWay(Way way)
        {
            if (way == null)
            {
                throw new ArgumentNullException("way");
            }

            if (way.Id == null)
            {
                throw new Exception("way.Id is null");
            }

            Ways[way.Id.Value] = way;
        }

        /// <summary>
        /// Adds a list of ways
        /// </summary>
        public override void AddWays(IList<Way> ways)
        {
            if (ways == null)
            {
                throw new ArgumentNullException("ways");
            }

            foreach (var way in ways)
            {
                if (!Ways.ContainsKey(way.Id.Value))
                {
                    Ways[way.Id.Value] = way;
                }
            }
        }

        /// <summary>
        /// Adds a dictionary of ways
        /// </summary>
        public override void AddWays(IDictionary<long, Way> ways)
        {
            if (ways == null)
            {
                throw new ArgumentNullException("ways");
            }

            foreach (var way in ways)
            {
                Ways[way.Key] = way.Value;
            }
        }

        /// <summary>
        /// Returns a dictionary of fetched ways given the ids
        /// </summary>
        /// <param name="ids">The ids of the ways to fetch</param>
        /// <param name="remaining_ids">The ids that were unable to be found</param>
        public override IDictionary<long, Way> GetWays(IList<long> ids, out IList<long> remaining_ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException("ids");
            }

            var ways = new Dictionary<long, Way>();
            remaining_ids = new List<long>();

            foreach (var id in ids)
            {
                if (Ways.ContainsKey(id))
                {
                    ways[id] = Ways[id];
                }
                else
                {
                    remaining_ids.Add(id);
                }
            }

            return ways;
        }

        /// <summary>
        /// Returns a list of fetched ways given the ids
        /// </summary>
        /// <param name="ids">The ids of the ways to fetch</param>
        /// <param name="remaining_ids">The ids that were unable to be found</param>
        public override IList<Way> GetWaysList(IList<long> ids, out IList<long> remaining_ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException("ids");
            }

            var ways = new List<Way>();
            remaining_ids = new List<long>();

            foreach (var id in ids)
            {
                if (Ways.ContainsKey(id))
                {
                    if (!ways.Contains(Ways[id]))
                    {
                        ways.Add(Ways[id]);
                    }
                }
                else
                {
                    if (!remaining_ids.Contains(id))
                    {
                        remaining_ids.Add(id);
                    }
                }
            }

            return ways;
        }

        /// <summary>
        /// Removes the way with the given id
        /// </summary>
        public override bool RemoveWay(long id)
        {
            return Ways.Remove(id);
        }

        /// <summary>
        /// Returns true if the way exists
        /// </summary>
        public override bool ContainsWay(long id)
        {
            return Ways.ContainsKey(id);
        }

        /// <summary>
        /// Returns true if the way given the id was successfully fetched
        /// </summary>
        public override bool TryGetWay(long id, out Way way)
        {
            return Ways.TryGetValue(id, out way);
        }

        /// <summary>
        /// Adds a new relation
        /// </summary>
        public override void AddRelation(Relation relation)
        {
            if (relation == null)
            {
                throw new ArgumentNullException("relation");
            }

            if (relation.Id == null)
            {
                throw new Exception("relation.Id is null");
            }

            Relations[relation.Id.Value] = relation;
        }

        /// <summary>
        /// Adds a list of relations
        /// </summary>
        public override void AddRelations(IList<Relation> relations)
        {
            if (relations == null)
            {
                throw new ArgumentNullException("relations");
            }

            foreach (var relation in relations)
            {
                if (!Relations.ContainsKey(relation.Id.Value))
                {
                    Relations[relation.Id.Value] = relation;
                }
            }
        }

        /// <summary>
        /// Adds a dictionary of relations
        /// </summary>
        public override void AddRelations(IDictionary<long, Relation> relations)
        {
            if (relations == null)
            {
                throw new ArgumentNullException("relations");
            }

            foreach (var relation in relations)
            {
                if (!Relations.ContainsKey(relation.Key))
                {
                    Relations[relation.Key] = relation.Value;
                }
            }
        }

        /// <summary>
        /// Returns a dictionary of fetched relations given the ids
        /// </summary>
        /// <param name="ids">The ids of the relations to fetch</param>
        /// <param name="remaining_ids">The ids that were unable to be found</param>
        public override IDictionary<long, Relation> GetRelations(IList<long> ids, out IList<long> remaining_ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException("ids");
            }

            var relations = new Dictionary<long, Relation>();
            remaining_ids = new List<long>();

            foreach (var id in ids)
            {
                if (Relations.ContainsKey(id))
                {
                    relations[id] = Relations[id];
                }
                else
                {
                    remaining_ids.Add(id);
                }
            }

            return relations;
        }

        /// <summary>
        /// Returns a list of fetched relations given the ids
        /// </summary>
        /// <param name="ids">The ids of the relations to fetch</param>
        /// <param name="remaining_ids">The ids that were unable to be found</param>
        public override IList<Relation> GetRelationsList(IList<long> ids, out IList<long> remaining_ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException("ids");
            }

            var relations = new List<Relation>();
            remaining_ids = new List<long>();

            foreach (var id in ids)
            {
                if (Relations.ContainsKey(id))
                {
                    if (!relations.Contains(Relations[id]))
                    {
                        relations.Add(Relations[id]);
                    }
                }
                else
                {
                    remaining_ids.Add(id);
                }
            }

            return relations;
        }

        /// <summary>
        /// Removes the relation with the given id
        /// </summary>
        public override bool RemoveRelation(long id)
        {
            return Relations.Remove(id);
        }

        /// <summary>
        /// Retruns true if the relation exists
        /// </summary>
        public override bool ContainsRelation(long id)
        {
            return Relations.ContainsKey(id);
        }

        /// <summary>
        /// Returns true if the relation given the id was successfully fetched
        /// </summary>
        public override bool TryGetRelation(long id, out Relation relation)
        {
            return Relations.TryGetValue(id, out relation);
        }

        /// <summary>
        /// Clears the data in this cache
        /// </summary>
        public override void Clear()
        {
            Nodes.Clear();
            Ways.Clear();
            Relations.Clear();
        }
    }
}
