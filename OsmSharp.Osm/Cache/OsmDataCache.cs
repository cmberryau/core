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
using OsmSharp.Osm.Data;

namespace OsmSharp.Osm.Cache
{
    /// <summary>
    /// An osm data cache for simple OSM objects.
    /// </summary>
    public abstract class OsmDataCache : IOsmGeoSource
    {
        /// <summary>
        /// The number of nodes held in the cache
        /// </summary>
        public abstract int NodeCount
        {
            get;
        }

        /// <summary>
        /// The number of ways held in the cache
        /// </summary>
        public abstract int WayCount
        {
            get;
        }

        /// <summary>
        /// The number of relations held in the cache
        /// </summary>
        public abstract int RelationCount
        {
            get;
        }

        /// <summary>
        /// Adds a node
        /// </summary>
        public abstract void AddNode(Node node);

        /// <summary>
        /// Adds a list of nodes
        /// </summary>
        public abstract void AddNodes(IList<Node> nodes);

        /// <summary>
        /// Adds a dictionary of node ids and nodes
        /// </summary>
        public abstract void AddNodes(IDictionary<long, Node> nodes);

        /// <summary>
        /// Returns the node with the given id if present
        /// </summary>
        public Node GetNode(long id)
        {
            Node node;

            if (TryGetNode(id, out node))
            {
                return node;
            }

            return null;
        }

        /// <summary>
        /// Returns a dictionary of fetched nodes given the ids
        /// </summary>
        /// <param name="ids">The ids of the nodes to fetch</param>
        /// <param name="remaining_ids">The ids that were unable to be found</param>
        public abstract IDictionary<long, Node> GetNodes(IList<long> ids, out IList<long> remaining_ids);

        /// <summary>
        /// Returns a list of fetched nodes given the ids
        /// </summary>
        /// <param name="ids">The ids of the nodes to fetch</param>
        /// <param name="remaining_ids">The ids that were unable to be found</param>
        public abstract IList<Node> GetNodesList(IList<long> ids, out IList<long> remaining_ids);

        /// <summary>
        /// Removes the node with the given id
        /// </summary>
        public abstract bool RemoveNode(long id);

        /// <summary>
        /// Retruns true if the node exists
        /// </summary>
        public abstract bool ContainsNode(long id);

        /// <summary>
        /// Tries to get the node with the given id
        /// </summary>
        public abstract bool TryGetNode(long id, out Node node);

        /// <summary>
        /// Adds a way
        /// </summary>
        public abstract void AddWay(Way way);

        /// <summary>
        /// Adds a list of ways
        /// </summary>
        public abstract void AddWays(IList<Way> ways);

        /// <summary>
        /// Adds a dictionary of ways
        /// </summary>
        public abstract void AddWays(IDictionary<long, Way> ways);

        /// <summary>
        /// Returns the way with the given id if present
        /// </summary>
        public Way GetWay(long id)
        {
            Way way;

            if (TryGetWay(id, out way))
            {
                return way;
            }

            return null;
        }

        /// <summary>
        /// Returns a dictionary of fetched ways given the ids
        /// </summary>
        /// <param name="ids">The ids of the ways to fetch</param>
        /// <param name="remaining_ids">The ids that were unable to be found</param>
        public abstract IDictionary<long, Way> GetWays(IList<long> ids, out IList<long> remaining_ids);

        /// <summary>
        /// Returns a list of fetched ways given the ids
        /// </summary>
        /// <param name="ids">The ids of the ways to fetch</param>
        /// <param name="remaining_ids">The ids that were unable to be found</param>
        public abstract IList<Way> GetWaysList(IList<long> ids, out IList<long> remaining_ids);

        /// <summary>
        /// Removes the way with the given id
        /// </summary>
        public abstract bool RemoveWay(long id);

        /// <summary>
        /// Returns true if the way exists
        /// </summary>
        public abstract bool ContainsWay(long id);

        /// <summary>
        /// Tries to get the way with the given id
        /// </summary>
        public abstract bool TryGetWay(long id, out Way way);

        /// <summary>
        /// Adds a new relation
        /// </summary>
        public abstract void AddRelation(Relation relation);

        /// <summary>
        /// Adds a list of relations
        /// </summary>
        public abstract void AddRelations(IList<Relation> relations);

        /// <summary>
        /// Adds a dictionary of relations
        /// </summary>
        public abstract void AddRelations(IDictionary<long, Relation> relations);

        /// <summary>
        /// Returns the relation with the given id if present.
        /// </summary>
        public Relation GetRelation(long id)
        {
            Relation relation;

            if (TryGetRelation(id, out relation))
            {
                return relation;
            }

            return null;
        }

        /// <summary>
        /// Returns a dictionary of fetched relations given the ids
        /// </summary>
        /// <param name="ids">The ids of the relations to fetch</param>
        /// <param name="remaining_ids">The ids that were unable to be found</param>
        public abstract IDictionary<long, Relation> GetRelations(IList<long> ids, out IList<long> remaining_ids);

        /// <summary>
        /// Returns a list of fetched relations given the ids
        /// </summary>
        /// <param name="ids">The ids of the relations to fetch</param>
        /// <param name="remaining_ids">The ids that were unable to be found</param>
        public abstract IList<Relation> GetRelationsList(IList<long> ids, out IList<long> remaining_ids);

        /// <summary>
        /// Removes the relation with the given id
        /// </summary>
        public abstract bool RemoveRelation(long id);

        /// <summary>
        /// Retruns true if the relation exists
        /// </summary>
        public abstract bool ContainsRelation(long id);

        /// <summary>
        /// Tries to get the relation with the given id
        /// </summary>
        public abstract bool TryGetRelation(long id, out Relation relation);

        /// <summary>
        /// Clears all data from this cache.
        /// </summary>
        public abstract void Clear();
    }
}