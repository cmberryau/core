//// OsmSharp - OpenStreetMap (OSM) SDK
//// Copyright (C) 2013 Abelshausen Ben
//// 
//// This file is part of OsmSharp.
//// 
//// OsmSharp is free software: you can redistribute it and/or modify
//// it under the terms of the GNU General Public License as published by
//// the Free Software Foundation, either version 2 of the License, or
//// (at your option) any later version.
//// 
//// OsmSharp is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//// GNU General Public License for more details.
//// 
//// You should have received a copy of the GNU General Public License
//// along with OsmSharp. If not, see <http://www.gnu.org/licenses/>.

//using System;

//namespace OsmSharp
//{
//    /// <summary>
//    /// Provides support for lazy initialization
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public sealed class Lazy<T>
//    {
//        private readonly object _lock = new object();
//        private readonly Func<T> _create;
//        private bool _created;
//        private T _value;

//        public T Value
//        {
//            get
//            {
//                if (!Created)
//                {
//                    lock (_lock)
//                    {
//                        _value = _create();
//                        _created = true;
//                    }
//                }

//                return _value;
//            }
//        }

//        public bool Created
//        {
//            get
//            {
//                lock (_lock)
//                {
//                    return _created;
//                }
//            }
//        }

//        public Lazy(Func<T> create)
//        {
//            if (create == null)
//            {
//                throw new NullReferenceException("Create function must not be null");
//            }

//            _create = create;
//        }

//        public override string ToString()
//        {
//            return Value.ToString();
//        }
//    }
//}
