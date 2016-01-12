// OsmSharp - OpenStreetMap (OSM) SDK
// Copyright (C) 2015 Abelshausen Ben
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
using System.Threading;
using OsmSharp.Collections.Tags;

namespace OsmSharp.Osm.Streams
{
    /// <summary>
    /// Any target of osm data (Nodes, Ways and Relations).
    /// </summary>
    public abstract class OsmStreamTarget
    {
        private readonly TagsCollectionBase _meta;

        /// <summary>
        /// The progress this OsmStreamTarget is making in a concurrent pull
        /// </summary>
        public uint PullProgress
        {
            get
            {
                return _pull_progress;
            }
        }

        /// <summary>
        /// Returns true if the stream target supports concurrent copies
        /// </summary>
        public virtual bool SupportsConcurrentCopies
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Holds the source for this target.
        /// </summary>
        private OsmStreamSource _source;

        /// <summary>
        /// Provides a lock for the source
        /// </summary>
        private object _source_lock;

        /// <summary>
        /// Concurrent pull progress
        /// </summary>
        private volatile uint _pull_progress;

        /// <summary>
        /// Should the pull be cancelled
        /// </summary>
        private volatile bool _cancel_pull;

        /// <summary>
        /// Creates a new target.
        /// </summary>
        protected OsmStreamTarget()
        {
            _meta = new TagsCollection();

            _cancel_pull = false;
            _pull_progress = 0;
        }

        /// <summary>
        /// Initializes the target.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Adds a geometry to the target
        /// </summary>
        public void Add(OsmGeo geo)
        {
            if (geo == null)
            {
                throw new NullReferenceException();
            }

            if (geo.Type == OsmGeoType.Node)
            {
                AddNode(geo as Node);
            }
            else if (geo.Type == OsmGeoType.Way)
            {
                AddWay(geo as Way);
            }
            else if (geo.Type == OsmGeoType.Relation)
            {
                AddRelation(geo as Relation);
            }
        }

        /// <summary>
        /// Adds a node to the target.
        /// </summary>
        /// <param name="simpleNode"></param>
        public abstract void AddNode(Node simpleNode);

        /// <summary>
        /// Adds a way to the target.
        /// </summary>
        /// <param name="simpleWay"></param>
        public abstract void AddWay(Way simpleWay);

        /// <summary>
        /// Adds a relation to the target.
        /// </summary>
        /// <param name="simpleRelation"></param>
        public abstract void AddRelation(Relation simpleRelation);

        /// <summary>
        /// Registers a reader on this writer.
        /// </summary>
        /// <param name="source"></param>
        public virtual void RegisterSource(OsmStreamSource source)
        {
            _source = source;
        }

        /// <summary>
        /// Returns the registered reader.
        /// </summary>
        protected OsmStreamSource Source
        {
            get
            {
                return _source;
            }
        }

        /// <summary>
        /// Pulls the changes from the source to this target.
        /// </summary>
        public void Pull()
        {
            _source.Initialize();
            this.Initialize();
            if (this.OnBeforePull())
            {
                this.DoPull();
                this.OnAfterPull();
            }
            this.Flush();
            this.Close();
        }

        /// <summary>
        /// Pulls the changes from the source to this target.
        /// </summary>
        /// <param name="num_threads">The number of threads to use</param>
        public void Pull(int num_threads)
        {
            if (num_threads == 0 || num_threads == 1)
            {
                this.Pull();
            }
            else
            {
                if (SupportsConcurrentCopies)
                {
                    if (this.OnBeforePull())
                    {
                        var threads = new List<Thread>();

                        if (_source_lock == null)
                        {
                            _source_lock = new object();
                        }

                        _source.Initialize();
                        this.Initialize();

                        _cancel_pull = false;
                        _pull_progress = 0;

                        for (int i = 0; i < num_threads; i++)
                        {
                            var thread = new Thread(() => this.StreamSourceToTargetThread(_source, this));
                            threads.Add(thread);
                            thread.Start();
                        }

                        foreach (var thread in threads)
                        {
                            thread.Join();
                        }

                        this.OnAfterPull();
                    }
                    this.Flush();
                    this.Close();
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        private void StreamSourceToTargetThread(OsmStreamSource source, OsmStreamTarget target)
        {
            var concurrent_target = target.ConcurrentCopy();

            concurrent_target.Initialize();

            OsmGeo current;

            while (ConcurrentMoveNext(source, out current))
            {
                concurrent_target.Add(current);
            }

            concurrent_target.Flush();
            concurrent_target.Close();
        }

        private bool ConcurrentMoveNext(OsmStreamSource source, out OsmGeo current,
                                        bool ignore_nodes = false, bool igonre_ways = false,
                                        bool ignore_relations = false)
        {
            bool available = false;

            current = null;

            lock (_source_lock)
            {
                if (_cancel_pull)
                {
                    return false;
                }

                available = source.MoveNext();

                if (available)
                {                    
                    current = source.Current();
                    _pull_progress++;
                }
            }

            return available;
        }

        /// <summary>
        /// Aborts the current pull the target is performing
        /// </summary>
        public void AbortThreadedPull()
        {
            _cancel_pull = true;
        }

        /// <summary>
        /// Pulls the next object and returns true if there was one.
        /// </summary>
        /// <returns></returns>
        public bool PullNext()
        {
            if (_source.MoveNext())
            {
                object sourceObject = _source.Current();
                if (sourceObject is Node)
                {
                    this.AddNode(sourceObject as Node);
                }
                else if (sourceObject is Way)
                {
                    this.AddWay(sourceObject as Way);
                }
                else if (sourceObject is Relation)
                {
                    this.AddRelation(sourceObject as Relation);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Does the pull operation until source is exhausted.
        /// </summary>
        protected void DoPull()
        {
            this.DoPull(false, false, false);
        }

        /// <summary>
        /// Does the pull operation until source is exhausted.
        /// </summary>
        /// <param name="ignoreNodes">Makes the source skip all nodes.</param>
        /// <param name="ignoreWays">Makes the source skip all ways.</param>
        /// <param name="ignoreRelations">Makes the source skip all relations.</param>
        protected void DoPull(bool ignoreNodes, bool ignoreWays, bool ignoreRelations)
        {
            _cancel_pull = false;
            _pull_progress = 0;

            while (_source.MoveNext(ignoreNodes, ignoreWays, ignoreRelations))
            {
                if (_cancel_pull)
                {
                    return;
                }

                object sourceObject = _source.Current();
                if (sourceObject is Node)
                {
                    this.AddNode(sourceObject as Node);
                }
                else if (sourceObject is Way)
                {
                    this.AddWay(sourceObject as Way);
                }
                else if (sourceObject is Relation)
                {
                    this.AddRelation(sourceObject as Relation);
                }

                _pull_progress++;
            }
        }

        /// <summary>
        /// Called right before pull and right after initialization.
        /// </summary>
        public virtual bool OnBeforePull()
        {
            return true;
        }

        /// <summary>
        /// Called right after pull and right before flush.
        /// </summary>
        public virtual void OnAfterPull()
        {

        }

        /// <summary>
        /// Gets the meta-data.
        /// </summary>
        public TagsCollectionBase Meta
        {
            get
            {
                return _meta;
            }
        }

        /// <summary>
        /// Gets all meta-data from all sources and filters that provide this target of data.
        /// </summary>
        /// <returns></returns>
        public TagsCollection GetAllMeta()
        {
            var tags = this.Source.GetAllMeta();
            tags.AddOrReplace(new TagsCollection(_meta));
            return tags;
        }

        /// <summary>
        /// Closes the current target.
        /// </summary>
        public virtual void Close()
        {

        }

        /// <summary>
        /// Flushes the current target.
        /// </summary>
        public virtual void Flush()
        {

        }

        /// <summary>
        /// Provides a copy of the OsmStreamTarget that is safe to write to
        /// while the original target is being written to
        /// </summary>
        /// <returns>The copy of the OsmStreamTarget</returns>
        public virtual OsmStreamTarget ConcurrentCopy()
        {
            throw new NotImplementedException();
        }
    }
}