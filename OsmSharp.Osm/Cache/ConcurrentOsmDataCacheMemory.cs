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

using System.Collections.Generic;

namespace OsmSharp.Osm.Cache
{
    /// <summary>
    /// An osm data cache for simple OSM objects kept in memory, additionally
    /// safe to use in multiple threads!
    /// </summary>
    public class ConcurrentOsmDataCacheMemory : OsmDataCacheMemory
    {
        /// <summary>
        /// The number of nodes held in the cache
        /// </summary>
        public override int NodeCount
        {
            get
            {
                lock (_nodes_lock)
                {
                    return base.NodeCount;
                }
            }
        }

        /// <summary>
        /// The number of ways held in the cache
        /// </summary>
        public override int WayCount
        {
            get
            {
                lock (_ways_lock)
                {
                    return base.WayCount;
                }
            }
        }

        /// <summary>
        /// The number of relations held in the cache
        /// </summary>
        public override int RelationCount
        {
            get
            {
                lock (_relations_lock)
                {
                    return base.RelationCount;
                }
            }
        }

        private readonly object _nodes_lock;
        private readonly object _ways_lock;
        private readonly object _relations_lock;

        /// <summary>
        /// Creates a new instance of OsmDataCacheMemoryThreadSafe
        /// </summary>
        public ConcurrentOsmDataCacheMemory()
            : base()
        {
            _nodes_lock = new object();
            _ways_lock = new object();
            _relations_lock = new object();
        }

        /// <summary>
        /// Adds a node
        /// </summary>
        public override void AddNode(Node node)
        {
            lock (_nodes_lock)
            {
                base.AddNode(node);
            }
        }

        /// <summary>
        /// Adds a list of nodes
        /// </summary>
        public override void AddNodes(IList<Node> nodes)
        {
            lock (_nodes_lock)
            {
                base.AddNodes(nodes);
            }
        }

        /// <summary>
        /// Adds a dictionary of node ids and nodes
        /// </summary>
        public override void AddNodes(IDictionary<long, Node> nodes)
        {
            lock (_nodes_lock)
            {
                base.AddNodes(nodes);
            }
        }

        /// <summary>
        /// Returns a dictionary of fetched nodes given the ids
        /// </summary>
        /// <param name="ids">The ids of the nodes to fetch</param>
        /// <param name="remaining_ids">The ids that were unable to be found</param>
        public override IDictionary<long, Node> GetNodes(IList<long> ids, out IList<long> remaining_ids)
        {
            lock (_nodes_lock)
            {
                return base.GetNodes(ids, out remaining_ids);
            }
        }

        /// <summary>
        /// Returns a list of fetched nodes given the ids
        /// </summary>
        /// <param name="ids">The ids of the nodes to fetch</param>
        /// <param name="remaining_ids">The ids that were unable to be found</param>
        public override IList<Node> GetNodesList(IList<long> ids, out IList<long> remaining_ids)
        {
            lock (_nodes_lock)
            {
                return base.GetNodesList(ids, out remaining_ids);
            }
        }

        /// <summary>
        /// Removes the node with the given id
        /// </summary>
        public override bool RemoveNode(long id)
        {
            lock (_nodes_lock)
            {
                return base.RemoveNode(id);
            }
        }

        /// <summary>
        /// Returns true if the node exists
        /// </summary>
        public override bool ContainsNode(long id)
        {
            lock (_nodes_lock)
            {
                return base.ContainsNode(id);
            }
        }

        /// <summary>
        /// Returns true if the node given the id was successfully fetched
        /// </summary>
        public override bool TryGetNode(long id, out Node node)
        {
            lock (_nodes_lock)
            {
                return base.TryGetNode(id, out node);
            }
        }

        /// <summary>
        /// Adds a way
        /// </summary>
        public override void AddWay(Way way)
        {
            lock (_ways_lock)
            {
                base.AddWay(way);
            }
        }

        /// <summary>
        /// Adds a list of ways
        /// </summary>
        public override void AddWays(IList<Way> ways)
        {
            lock (_ways_lock)
            {
                base.AddWays(ways);
            }
        }

        /// <summary>
        /// Adds a dictionary of ways
        /// </summary>
        public override void AddWays(IDictionary<long, Way> ways)
        {
            lock (_ways_lock)
            {
                base.AddWays(ways);
            }
        }

        /// <summary>
        /// Returns a dictionary of fetched ways given the ids
        /// </summary>
        /// <param name="ids">The ids of the ways to fetch</param>
        /// <param name="remaining_ids">The ids that were unable to be found</param>
        public override IDictionary<long, Way> GetWays(IList<long> ids, out IList<long> remaining_ids)
        {
            lock (_ways_lock)
            {
                return base.GetWays(ids, out remaining_ids);
            }
        }

        /// <summary>
        /// Returns a list of fetched ways given the ids
        /// </summary>
        /// <param name="ids">The ids of the ways to fetch</param>
        /// <param name="remaining_ids">The ids that were unable to be found</param>
        public override IList<Way> GetWaysList(IList<long> ids, out IList<long> remaining_ids)
        {
            lock (_ways_lock)
            {
                return base.GetWaysList(ids, out remaining_ids);
            }
        }

        /// <summary>
        /// Removes the way with the given id
        /// </summary>
        public override bool RemoveWay(long id)
        {
            lock (_ways_lock)
            {
                return base.RemoveWay(id);
            }
        }

        /// <summary>
        /// Returns true if the way exists
        /// </summary>
        public override bool ContainsWay(long id)
        {
            lock (_ways_lock)
            {
                return base.ContainsWay(id);
            }
        }

        /// <summary>
        /// Returns true if the way given the id was successfully fetched
        /// </summary>
        public override bool TryGetWay(long id, out Way way)
        {
            lock (_ways_lock)
            {
                return base.TryGetWay(id, out way);
            }
        }

        /// <summary>
        /// Adds a new relation
        /// </summary>
        public override void AddRelation(Relation relation)
        {
            lock (_relations_lock)
            {
                base.AddRelation(relation);
            }
        }

        /// <summary>
        /// Adds a list of relations
        /// </summary>
        public override void AddRelations(IList<Relation> relations)
        {
            lock (_relations_lock)
            {
                base.AddRelations(relations);
            }
        }

        /// <summary>
        /// Adds a dictionary of relations
        /// </summary>
        public override void AddRelations(IDictionary<long, Relation> relations)
        {
            lock (_relations_lock)
            {
                base.AddRelations(relations);
            }
        }

        /// <summary>
        /// Returns a dictionary of fetched relations given the ids
        /// </summary>
        /// <param name="ids">The ids of the relations to fetch</param>
        /// <param name="remaining_ids">The ids that were unable to be found</param>
        public override IDictionary<long, Relation> GetRelations(IList<long> ids, out IList<long> remaining_ids)
        {
            lock (_relations_lock)
            {
                return base.GetRelations(ids, out remaining_ids);
            }
        }

        /// <summary>
        /// Returns a list of fetched relations given the ids
        /// </summary>
        /// <param name="ids">The ids of the relations to fetch</param>
        /// <param name="remaining_ids">The ids that were unable to be found</param>
        public override IList<Relation> GetRelationsList(IList<long> ids, out IList<long> remaining_ids)
        {
            lock (_relations_lock)
            {
                return base.GetRelationsList(ids, out remaining_ids);
            }
        }

        /// <summary>
        /// Removes the relation with the given id
        /// </summary>
        public override bool RemoveRelation(long id)
        {
            lock (_relations_lock)
            {
                return base.RemoveRelation(id);
            }
        }

        /// <summary>
        /// Retruns true if the relation exists
        /// </summary>
        public override bool ContainsRelation(long id)
        {
            lock (_relations_lock)
            {
                return base.ContainsRelation(id);
            }
        }

        /// <summary>
        /// Returns true if the relation given the id was successfully fetched
        /// </summary>
        public override bool TryGetRelation(long id, out Relation relation)
        {
            lock (_relations_lock)
            {
                return base.TryGetRelation(id, out relation);
            }
        }

        /// <summary>
        /// Clears the data in this cache
        /// </summary>
        public override void Clear()
        {
            lock (_nodes_lock)
            {
                lock (_ways_lock)
                {
                    lock (_relations_lock)
                    {
                        base.Clear();
                    }
                }
            }
        }
    }
}