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
using System.Linq;

namespace OsmSharp.Collections.Tags
{
    /// <summary>
    /// Represents an empty and read-only tags collection object
    /// </summary>
    internal class EmptyTagsCollection : TagsCollectionBase
    {
        /// <summary>
        /// Returns true if this collection is readonly.
        /// </summary>
        public override bool IsReadonly => true;

        /// <summary>
        /// Returns the number of tags in this collection.
        /// </summary>
        public override int Count => 0;

        /// <summary>
        /// Adds a key-value pair to this tags collection.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public override void Add(string key, string value) { }

        /// <summary>
        /// Adds a tag.
        /// </summary>
        /// <param name="tag"></param>
        public override void Add(Tag tag) { }

        /// <summary>
        /// Adds a tag or replace the existing value if any.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public override void AddOrReplace(string key, string value) { }

        /// <summary>
        /// Adds a tag or replace the existing value if any.
        /// </summary>
        /// <param name="tag"></param>
        public override void AddOrReplace(Tag tag) { }

        /// <summary>
        /// Returns true if the given tag exists.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override bool ContainsKey(string key)
        {
            return false;
        }

        /// <summary>
        /// Returns true if the given tag exists.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool TryGetValue(string key, out string value)
        {
            value = null;
            return false;
        }

        /// <summary>
        /// Returns true if the given tag exists with the given value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool ContainsKeyValue(string key, string value)
        {
            return false;
        }

        /// <summary>
        /// Removes all tags with the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override bool RemoveKey(string key)
        {
            return false;
        }

        /// <summary>
        /// Removes all tags with the given key-values.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool RemoveKeyValue(string key, string value)
        {
            return false;
        }

        /// <summary>
        /// Clears all tags.
        /// </summary>
        public override void Clear() { }

        /// <summary>
        /// Removes all tags that match the given criteria.
        /// </summary>
        /// <param name="predicate"></param>
        public override void RemoveAll(Predicate<Tag> predicate) { }

        /// <summary>
        /// Returns the enumerator
        /// </summary>
        /// <returns></returns>
        public override IEnumerator<Tag> GetEnumerator()
        {
            return Enumerable.Empty<Tag>().GetEnumerator();
        }
    }
}