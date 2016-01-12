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
using OsmSharp.Math.Geo;
using OsmSharp.Osm.Filters;
using OsmSharp.Osm.Collections;
using OsmSharp.Osm.Tiles;
using OsmSharp.Collections.Tags;

namespace OsmSharp.Osm.Data
{
    /// <summary>
    /// Base class for IDataSourceReadOnly-implementations.
    /// </summary>
    public abstract class DataSourceReadOnlyBase : IDataSourceReadOnly
    {
        /// <summary>
        /// The default zoom level that this data source reads at
        /// </summary>
        public virtual int DefaultZoomLevel 
        {
            get
            {
                return 15;
            }
        }

        /// <summary>
        /// Returns true when this data-source is readonly.
        /// </summary>
        public virtual bool IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// Returns true if this datasource supports concurrent copies.
        /// </summary>
        public virtual bool SupportsConcurrentCopies
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the node with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Node GetNode(long id)
        {
            IList<Node> nodes = this.GetNodes(new List<long>(new long[] { id }));
            if (nodes.Count > 0)
            {
                return nodes[0];
            }
            return null;
        }

        /// <summary>
        /// Returns all nodes with the given ids.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public abstract IList<Node> GetNodes(IList<long> ids);

        /// <summary>
        /// Returns the relation with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Relation GetRelation(long id)
        {
            IList<Relation> relations = this.GetRelations(new List<long>(new long[] { id }));
            if (relations.Count > 0)
            {
                return relations[0];
            }
            return null;
        }

        /// <summary>
        /// Returns the relation with the given ids.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public abstract IList<Relation> GetRelations(IList<long> ids);

        /// <summary>
        /// Returns all relations containing the object with the given type and id.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract IList<Relation> GetRelationsFor(OsmGeoType type, long id);

        /// <summary>
        /// Returns all relations containg the given object as a member.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public IList<Relation> GetRelationsFor(OsmGeo obj)
        {
            return this.GetRelationsFor(obj.Type, obj.Id.Value);
        }

        /// <summary>
        /// Returns the way with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Way GetWay(long id)
        {
            IList<Way> ways = this.GetWays(new List<long>(new long[] { id }));
            if (ways.Count > 0)
            {
                return ways[0];
            }
            return null;
        }

        /// <summary>
        /// Returns the ways with the given ids.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public abstract IList<Way> GetWays(IList<long> ids);

        /// <summary>
        /// Returns all the ways containing the node with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract IList<Way> GetWaysFor(long id);

        /// <summary>
        /// Returns all the ways containing the given node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual IList<Way> GetWaysFor(Node node)
        {
            return this.GetWaysFor(node.Id.Value);
        }

        /// <summary>
        /// Returns all objects inside the given boundingbox and according to the given filter.
        /// </summary>
        /// <param name="box"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public abstract IList<OsmGeo> Get(GeoCoordinateBox box, Filter filter);

        /// <summary>
        /// Returns all data within the given tile
        /// </summary>
        /// <param name="tile">The tile to fetch geometries from</param>
        /// <param name="filter">Filtering options for the results</param>
        /// <returns>An OsmGeoCollection object containing the data within the given tile</returns>
        public virtual OsmGeoCollection GetCollection(Tile tile, Filter filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all data within the given tiles
        /// </summary>
        /// <param name="tiles">The tiles to fetch geometries from</param>
        /// <param name="filter">Filtering options for the results</param>
        /// <returns>An OsmGeoCollection object containing the data within the given tile</returns>
        public virtual OsmGeoCollection GetCollection(IList<Tile> tiles, Filter filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns ways matching the tag and values passed
        /// </summary>
        public virtual OsmGeoCollection GetGeosGivenTag(OsmGeoType type, string tag, List<string> values)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns ways matching the tags passed
        /// </summary>
        public virtual OsmGeoCollection GetGeosGivenTags(OsmGeoType type, Dictionary<string, List<string>> tags)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the unique tags for the given geo type
        /// </summary>
        /// <param name="type">The geo type</param>
        /// <param name="keys">The key filter, only return tag combinations with these keys</param>
        public virtual HashSet<TagsCollectionBase> UniqueTags(OsmGeoType type, List<string> keys = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the unique tags for the given geo type
        /// </summary>
        /// <param name="type">The geo type</param>
        /// <param name="key">The key filter, only return tag combinations with this key</param>
        public virtual HashSet<TagsCollectionBase> UniqueTags(OsmGeoType type, string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the boundingbox of the data in this datasource.
        /// </summary>
        public abstract GeoCoordinateBox BoundingBox
        {
            get;
        }

        /// <summary>
        /// Returns the id of this datasource.
        /// </summary>
        public abstract Guid Id
        {
            get;
        }

        /// <summary>
        /// Returns true if this datasource has a boundingbox.
        /// </summary>
        public abstract bool HasBoundingBox
        {
            get;
        }

        /// <summary>
        /// Provides a copy of the source that is safe to
        /// read from at the same time as the source and any
        /// other copies
        /// </summary>
        public virtual IDataSourceReadOnly ConcurrentCopy()
        {
            throw new NotImplementedException();
        }
    }
}
