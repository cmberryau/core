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

using OsmSharp.Osm.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsmSharp.Osm.Data
{
    /// <summary>
    /// Base class for database tools
    /// </summary>
    public abstract class SchemaTools
    {
        #region constants

        /// <summary>
        /// Zoom level that tile ids shall be calculated at,
        /// note if you change, drop the db and rebuild
        /// </summary>
        public const int DefaultTileZoomLevel = 16;

        #endregion constants

        /// <summary>
        /// Converts a short to it's member type representation
        /// </summary>
        public static OsmGeoType? ConvertMemberType(short member_type)
        {
            switch (member_type)
            {
                case (short)OsmGeoType.Node:
                    return OsmGeoType.Node;
                case (short)OsmGeoType.Way:
                    return OsmGeoType.Way;
                case (short)OsmGeoType.Relation:
                    return OsmGeoType.Relation;
            }

            throw new ArgumentOutOfRangeException("Invalid member type.");
        }

        /// <summary>
        /// Converts a OsmGeoType enum to it's short representation
        /// </summary>
        public static short? ConvertMemberTypeShort(OsmGeoType? memberType)
        {
            if (memberType.HasValue)
            {
                return (short)memberType.Value;
            }

            return null;
        }

        /// <summary>
        /// Constructs an id list from the complete given list of longs
        /// </summary>
        /// <param name="ids">The list of longs to construct the id list from</param>
        public static string ConstructIdList(IList<long> ids)
        {
            return ConstructIdList(ids, 0, ids.Count);
        }

        /// <summary>
        /// Constructs an id list from the complete given enumerable of longs
        /// </summary>
        /// <param name="ids">The enumerable of longs to construct the id list from</param>
        public static string ConstructIdList(IEnumerable<long> ids)
        {
            var ids_list = ids.ToList();

            return ConstructIdList(ids_list, 0, ids_list.Count);
        }

        /// <summary>
        /// Constructs an id list from the given list of longs
        /// </summary>
        /// <param name="ids">The list of longs to construct the id list from</param>
        /// <param name="start">The start index</param>
        /// <param name="end">The end index</param>
        public static string ConstructIdList(IList<long> ids, int start, int end)
        {
            var sb = new StringBuilder();

            if (ids.Count > 0 && ids.Count > start)
            {
                sb.Append(ids[start].ToString());

                for (var i = start + 1; i < end; i++)
                {
                    var id_string = ids[i].ToString();

                    sb.Append(",");
                    sb.Append(id_string);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Constructs an id list from the given list of ints
        /// </summary>
        /// <param name="ids">The list of ints to construct the id list from</param>
        /// <param name="start">The start index</param>
        /// <param name="end">The end index</param>
        public static string ConstructIdList(IList<int> ids, int start, int end)
        {
            var sb = new StringBuilder();

            if (ids.Count > 0 && ids.Count > start)
            {
                sb.Append(ids[start].ToString());

                for (var i = start + 1; i < end; i++)
                {
                    var id_string = ids[i].ToString();

                    sb.Append(",");
                    sb.Append(id_string);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Constructs an id list from the given tilerange object
        /// </summary>
        public static List<long> ConstructIdList(TileRange tile_range)
        {
            var tile_ids = new List<long>();

            foreach (var tile in tile_range)
            {
                tile_ids.Add((long)tile.Id);
            }

            return tile_ids;
        }
    }
}
