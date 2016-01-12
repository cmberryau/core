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
using OsmSharp.Collections.Tags;
using OsmSharp.Osm.Filters.Tags;

namespace OsmSharp.Osm.Filters
{
    /// <summary>
    /// A basic class defining a filter for the tags of OsmGeo object
    /// </summary>
    public abstract class TagFilter
    {
        /// <summary>
        /// Evaluates the filter against the osm object
        /// </summary>
        public TagsCollectionBase Evaluate(CompleteOsmGeo obj)
        {
            return this.Evaluate(obj.ToSimple());
        }

        /// <summary>
        /// Evaluates the filter against the osm object
        /// </summary>
        public abstract TagsCollectionBase Evaluate(OsmGeo obj);

        /// <summary>
        /// Evaluates the filter against the tags collection
        /// </summary>
        public abstract TagsCollectionBase Evaluate(TagsCollectionBase tags, OsmGeoType type);

        /// <summary>
        /// Returns description of this filter.
        /// </summary>
        public abstract override string ToString();

        /// <summary>
        /// Combines two tag filters
        /// </summary>
        public static TagFilter operator +(TagFilter lhs, TagFilter rhs)
        {
            return new TagFilterAdditive(lhs, rhs);
        }

        /// <summary>
        /// Returns a Tag filter that matches any kind of tag for any type of object
        /// </summary>
        public static TagFilter Any()
        {
            return new TagFilterAny();
        }

        /// <summary>
        /// Returns a Tag filter that matches no tag for the given type of object
        /// </summary>
        public static TagFilter None(OsmGeoType type)
        {
            return new TagFilterNone(type);
        }

        /// <summary>
        /// Returns a Tag filter for a particular key for the given type
        /// </summary>
        public static TagFilter Key(OsmGeoType type, string key)
        {
            return new TagFilterTags(type, key);
        }

        /// <summary>
        /// Returns a Tag filter for keys for the given type
        /// </summary>
        public static TagFilter Key(OsmGeoType type, ICollection<string> keys)
        {
            return new TagFilterTags(type, keys);
        }
    }
}