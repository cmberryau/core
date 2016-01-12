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
using OsmSharp.Collections.Tags;

namespace OsmSharp.Osm.Filters.Tags
{
    /// <summary>
    /// Filter that combines two other tag filters in an addititive
    /// </summary>
    internal class TagFilterAdditive : TagFilter
    {
        private TagFilter _a;
        private TagFilter _b;

        /// <summary>
        /// Creates a new additive filter
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public TagFilterAdditive(TagFilter a, TagFilter b)
        {
            if (a == null)
            {
                throw new ArgumentNullException("a");
            }

            if (b == null)
            {
                throw new ArgumentNullException("b");
            }

            _a = a;
            _b = b;
        }

        public override TagsCollectionBase Evaluate(OsmGeo obj)
        {
            var a_result = _a.Evaluate(obj);
            var b_result = _b.Evaluate(obj);

            return a_result.Union(b_result);
        }

        public override TagsCollectionBase Evaluate(TagsCollectionBase tags, OsmGeoType type)
        {
            var a_result = _a.Evaluate(tags, type);
            var b_result = _b.Evaluate(tags, type);

            return a_result.Union(b_result);
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}