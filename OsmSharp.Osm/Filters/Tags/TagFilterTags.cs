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

namespace OsmSharp.Osm.Filters.Tags
{
    internal class TagFilterTags : TagFilter
    {
        private ICollection<string> _keys;
        private OsmGeoType _type;

        public TagFilterTags(OsmGeoType type, string key)
        {
            if (string.IsNullOrEmpty("key"))
            {
                throw new ArgumentException("Must not be null or empty", "key");
            }

            _keys = new HashSet<string>() { key };
            _type = type;
        }

        public TagFilterTags(OsmGeoType type, ICollection<string> keys)
        {
            if (keys == null)
            {
                throw new NullReferenceException("keys");
            }

            if (keys.Count <= 0)
            {
                throw new ArgumentException("Must have at least one key", "keys");
            }

            _keys = keys;
            _type = type;
        }

        public override TagsCollectionBase Evaluate(OsmGeo obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (obj.Type == _type)
            {
                return obj.Tags.KeepKeysOf(_keys);
            }

            return TagsCollectionBase.Empty;
        }

        public override TagsCollectionBase Evaluate(TagsCollectionBase tags, OsmGeoType type)
        {
            if (tags == null)
            {
                throw new ArgumentNullException("tags");
            }

            if (type == _type)
            {
                return tags.KeepKeysOf(_keys);
            }

            return TagsCollectionBase.Empty;
        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
    }
}