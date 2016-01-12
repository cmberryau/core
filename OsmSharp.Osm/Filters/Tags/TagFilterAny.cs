using System;
using OsmSharp.Collections.Tags;

namespace OsmSharp.Osm.Filters.Tags
{
    internal class TagFilterAny : TagFilter
    {
        public TagFilterAny()
        {

        }

        public override TagsCollectionBase Evaluate(OsmGeo obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            return obj.Tags;
        }

        public override TagsCollectionBase Evaluate(TagsCollectionBase tags, OsmGeoType type)
        {
            if (tags == null)
            {
                throw new ArgumentNullException("tags");
            }

            return tags;
        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
    }
}