using System;
using OsmSharp.Collections.Tags;

namespace OsmSharp.Osm.Filters.Tags
{
    public class TagFilterNone : TagFilter
    {
        private OsmGeoType _type;

        public TagFilterNone(OsmGeoType type)
        {
            _type = type;
        }

        public override TagsCollectionBase Evaluate(OsmGeo obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            var result = obj.Tags;

            if (obj.Type == _type)
            {
                result = TagsCollectionBase.Empty;
            }

            return result;
        }

        public override TagsCollectionBase Evaluate(TagsCollectionBase tags, OsmGeoType type)
        {
            if (tags == null)
            {
                throw new ArgumentNullException("tags");
            }

            var result = tags;

            if (type == _type)
            {
                result = TagsCollectionBase.Empty;
            }

            return result;
        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
    }
}