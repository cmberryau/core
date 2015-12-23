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
using OsmSharp.Collections.Tags;

namespace OsmSharp.Osm.Cache
{
    /// <summary>
    /// Specialised tags collection cache
    /// </summary>
    public sealed class ConcurrentTagsCollectionCache
    {
        private readonly IDictionary<OsmGeoType, IDictionary<int, TagsCollectionBase>> _collections;
        private readonly IDictionary<OsmGeoType, object> _locks;

        /// <summary>
        /// Creates a new instance of TagsCollectionCache
        /// </summary>
        public ConcurrentTagsCollectionCache()
        {
            _collections = new Dictionary<OsmGeoType, IDictionary<int, TagsCollectionBase>>();

            _collections[OsmGeoType.Node] = new Dictionary<int, TagsCollectionBase>();
            _collections[OsmGeoType.Way] = new Dictionary<int, TagsCollectionBase>();
            _collections[OsmGeoType.Relation] = new Dictionary<int, TagsCollectionBase>();

            _locks = new Dictionary<OsmGeoType, object>();

            _locks[OsmGeoType.Node] = new object();
            _locks[OsmGeoType.Way] = new object();
            _locks[OsmGeoType.Relation] = new object();
        }

        /// <summary>
        /// Adds a node tags collection to the cache
        /// </summary>
        /// <param name="id">The id of the tags collection</param>
        /// <param name="collection">The tags collection</param>
        public void AddNodeTagsCollection(int id, TagsCollectionBase collection)
        {
            AddTagsCollection(id, collection, OsmGeoType.Node);
        }

        /// <summary>
        /// Gets a node tags collection from the cache
        /// </summary>
        /// <param name="id">The id of the tags collection</param>
        public TagsCollectionBase GetNodeTagsCollection(int id)
        {
            return GetTagsCollection(id, OsmGeoType.Node);
        }

        /// <summary>
        /// Tries to get a node tags collection from the cache
        /// </summary>
        /// <param name="id">The id of the tags collection</param>
        /// <param name="collection">The collection to assign</param>
        /// <returns>True if successfully fetched, false otherwise</returns>
        public bool TryGetNodeTagsCollection(int id, out TagsCollectionBase collection)
        {
            return TryGetTagsCollection(id, out collection, OsmGeoType.Node);
        }

        /// <summary>
        /// Evaluates if the cache has a node tags collection
        /// </summary>
        /// <param name="id">The given id to evaluate</param>
        /// <returns>True if the cache contains the tags collection</returns>
        public bool ContainsNodeTagsCollection(int id)
        {
            return ContainsTagsCollection(id, OsmGeoType.Node);
        }

        /// <summary>
        /// Removes a node tags collection from the cache
        /// </summary>
        /// <param name="id">The given id to remove from the cache</param>
        /// <returns>True if the entry was removed, false if not</returns>
        public bool RemoveNodeTagsCollection(int id)
        {
            return RemoveTagsCollection(id, OsmGeoType.Node);
        }

        /// <summary>
        /// Adds a way tags collection to the cache
        /// </summary>
        /// <param name="id">The id of the tags collection</param>
        /// <param name="collection">The tags collection</param>
        public void AddWayTagsCollection(int id, TagsCollectionBase collection)
        {
            AddTagsCollection(id, collection, OsmGeoType.Way);
        }

        /// <summary>
        /// Gets a way tags collection from the cache
        /// </summary>
        /// <param name="id">The id of the tags collection</param>
        public TagsCollectionBase GetWayTagsCollection(int id)
        {
            return GetTagsCollection(id, OsmGeoType.Way);
        }

        /// <summary>
        /// Tries to get a way tags collection from the cache
        /// </summary>
        /// <param name="id">The id of the tags collection</param>
        /// <param name="collection">The collection to assign</param>
        /// <returns>True if successfully fetched, false otherwise</returns>
        public bool TryGetWayTagsCollection(int id, out TagsCollectionBase collection)
        {
            return TryGetTagsCollection(id, out collection, OsmGeoType.Way);
        }

        /// <summary>
        /// Evaluates if the cache has a way tags collection
        /// </summary>
        /// <param name="id">The given id to evaluate</param>
        /// <returns>True if the cache contains the tags collection</returns>
        public bool ContainsWayTagsCollection(int id)
        {
            return ContainsTagsCollection(id, OsmGeoType.Way);
        }

        /// <summary>
        /// Removes a way tags collection from the cache
        /// </summary>
        /// <param name="id">The given id to remove from the cache</param>
        /// <returns>True if the entry was removed, false if not</returns>
        public bool RemoveWayTagsCollection(int id)
        {
            return RemoveTagsCollection(id, OsmGeoType.Way);
        }

        /// <summary>
        /// Adds a relation tags collection to the cache
        /// </summary>
        /// <param name="id">The id of the tags collection</param>
        /// <param name="collection">The tags collection</param>
        public void AddRelationTagsCollection(int id, TagsCollectionBase collection)
        {
            AddTagsCollection(id, collection, OsmGeoType.Relation);
        }

        /// <summary>
        /// Gets a relation tags collection from the cache
        /// </summary>
        /// <param name="id">The id of the tags collection</param>
        public TagsCollectionBase GetRelationTagsCollection(int id)
        {
            return GetTagsCollection(id, OsmGeoType.Relation);
        }

        /// <summary>
        /// Tries to get a relation tags collection from the cache
        /// </summary>
        /// <param name="id">The id of the tags collection</param>
        /// <param name="collection">The collection to assign</param>
        /// <returns>True if successfully fetched, false otherwise</returns>
        public bool TryGetRelationTagsCollection(int id, out TagsCollectionBase collection)
        {
            return TryGetTagsCollection(id, out collection, OsmGeoType.Relation);
        }

        /// <summary>
        /// Evaluates if the cache has a relation tags collection
        /// </summary>
        /// <param name="id">The given id to evaluate</param>
        /// <returns>True if the cache contains the tags collection</returns>
        public bool ContainsRelationTagsCollection(int id)
        {
            return ContainsTagsCollection(id, OsmGeoType.Relation);
        }

        /// <summary>
        /// Removes a relation tags collection from the cache
        /// </summary>
        /// <param name="id">The given id to remove from the cache</param>
        /// <returns>True if the entry was removed, false if not</returns>
        public bool RemoveRelationTagsCollection(int id)
        {
            return RemoveTagsCollection(id, OsmGeoType.Relation);
        }

        /// <summary>
        /// Adds a collection to the cache
        /// </summary>
        /// <param name="id">The id of the tags collection</param>
        /// <param name="collection">The tags collection</param>
        /// <param name="type">The type of osm geo</param>
        public void AddTagsCollection(int id, TagsCollectionBase collection, OsmGeoType type)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            lock (_locks[type])
            {
                _collections[type][id] = collection;
            }
        }

        /// <summary>
        /// Gets a way tags collection from the cache
        /// </summary>
        /// <param name="id">The id of the tags collection</param>
        /// <param name="type">The type of osm geo</param>
        public TagsCollectionBase GetTagsCollection(int id, OsmGeoType type)
        {
            lock (_locks[type])
            {
                TagsCollectionBase collection;

                if (TryGetTagsCollection(id, out collection, type))
                {
                    return collection;
                }
            }

            return null;
        }

        /// <summary>
        /// Tries to get a way tags collection from the cache
        /// </summary>
        /// <param name="id">The id of the tags collection</param>
        /// <param name="collection">The collection to assign</param>
        /// <param name="type">The type of osm geo</param>
        /// <returns>True if successfully fetched, false otherwise</returns>        
        public bool TryGetTagsCollection(int id, out TagsCollectionBase collection, OsmGeoType type)
        {
            lock (_locks[type])
            {
                return _collections[type].TryGetValue(id, out collection);
            }
        }

        /// <summary>
        /// Evaluates if the cache has a relation tags collection
        /// </summary>
        /// <param name="id">The given id to evaluate</param>
        /// <param name="type">The type of osm geo</param>
        /// <returns>True if the cache contains the tags collection</returns>
        public bool ContainsTagsCollection(int id, OsmGeoType type)
        {
            lock (_locks[type])
            {
                return _collections[type].ContainsKey(id);
            }
        }

        /// <summary>
        /// Removes a relation tags collection from the cache
        /// </summary>
        /// <param name="id">The given id to remove from the cache</param>
        /// <param name="type">The type of osm geo</param>
        /// <returns>True if the entry was removed, false if not</returns>
        public bool RemoveTagsCollection(int id, OsmGeoType type)
        {
            lock (_locks[type])
            {
                return _collections[type].Remove(id);
            }
        }
    }
}