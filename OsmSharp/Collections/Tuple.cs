#if NET35

using System.Text;

namespace System
{
    internal interface ITuple
    {
        string ToString(StringBuilder sb);
        int Size { get; }
    }

    public static class Tuple
    {
        public static Tuple<T1> Create<T1>(T1 item1)
        {
            return new Tuple<T1>(item1);
        }

        public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        {
            return new Tuple<T1, T2>(item1, item2);
        }
    }

    public class Tuple<T1> : ITuple
    {
        private readonly T1 m_Item1;

        public T1 Item1 { get { return m_Item1; } }

        public Tuple(T1 item1)
        {
            m_Item1 = item1;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            return ((ITuple)this).ToString(sb);
        }

        string ITuple.ToString(StringBuilder sb)
        {
            sb.Append(m_Item1);
            sb.Append(")");
            return sb.ToString();
        }

        int ITuple.Size
        {
            get
            {
                return 1;
            }
        }
    }

    public class Tuple<T1, T2> : ITuple
    {
        private readonly T1 m_Item1;
        private readonly T2 m_Item2;

        public T1 Item1 { get { return m_Item1; } }
        public T2 Item2 { get { return m_Item2; } }

        public Tuple(T1 item1, T2 item2)
        {
            m_Item1 = item1;
            m_Item2 = item2;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            return ((ITuple)this).ToString(sb);
        }

        string ITuple.ToString(StringBuilder sb)
        {
            sb.Append(m_Item1);
            sb.Append(", ");
            sb.Append(m_Item2);
            sb.Append(")");
            return sb.ToString();
        }

        int ITuple.Size
        {
            get
            {
                return 2;
            }
        }
    }

    public class Tuple<T1, T2, T3> : ITuple
    {
        private readonly T1 m_Item1;
        private readonly T2 m_Item2;
        private readonly T3 m_Item3;

        public T1 Item1 { get { return m_Item1; } }
        public T2 Item2 { get { return m_Item2; } }
        public T3 Item3 { get { return m_Item3; } }

        public Tuple(T1 item1, T2 item2, T3 item3)
        {
            m_Item1 = item1;
            m_Item2 = item2;
            m_Item3 = item3;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            return ((ITuple)this).ToString(sb);
        }

        string ITuple.ToString(StringBuilder sb)
        {
            sb.Append(m_Item1);
            sb.Append(", ");
            sb.Append(m_Item2);
            sb.Append(", ");
            sb.Append(m_Item3);
            sb.Append(")");
            return sb.ToString();
        }

        int ITuple.Size
        {
            get
            {
                return 3;
            }
        }
    }

    public class Tuple<T1, T2, T3, T4> : ITuple
    {
        private readonly T1 m_Item1;
        private readonly T2 m_Item2;
        private readonly T3 m_Item3;
        private readonly T4 m_Item4;

        public T1 Item1 { get { return m_Item1; } }
        public T2 Item2 { get { return m_Item2; } }
        public T3 Item3 { get { return m_Item3; } }
        public T4 Item4 { get { return m_Item4; } }

        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            m_Item1 = item1;
            m_Item2 = item2;
            m_Item3 = item3;
            m_Item4 = item4;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            return ((ITuple)this).ToString(sb);
        }

        string ITuple.ToString(StringBuilder sb)
        {
            sb.Append(m_Item1);
            sb.Append(", ");
            sb.Append(m_Item2);
            sb.Append(", ");
            sb.Append(m_Item3);
            sb.Append(", ");
            sb.Append(m_Item4);
            sb.Append(")");
            return sb.ToString();
        }

        int ITuple.Size
        {
            get
            {
                return 4;
            }
        }
    }

    public class Tuple<T1, T2, T3, T4, T5> : ITuple
    {
        private readonly T1 m_Item1;
        private readonly T2 m_Item2;
        private readonly T3 m_Item3;
        private readonly T4 m_Item4;
        private readonly T5 m_Item5;

        public T1 Item1 { get { return m_Item1; } }
        public T2 Item2 { get { return m_Item2; } }
        public T3 Item3 { get { return m_Item3; } }
        public T4 Item4 { get { return m_Item4; } }
        public T5 Item5 { get { return m_Item5; } }

        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
        {
            m_Item1 = item1;
            m_Item2 = item2;
            m_Item3 = item3;
            m_Item4 = item4;
            m_Item5 = item5;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            return ((ITuple)this).ToString(sb);
        }

        string ITuple.ToString(StringBuilder sb)
        {
            sb.Append(m_Item1);
            sb.Append(", ");
            sb.Append(m_Item2);
            sb.Append(", ");
            sb.Append(m_Item3);
            sb.Append(", ");
            sb.Append(m_Item4);
            sb.Append(", ");
            sb.Append(m_Item5);
            sb.Append(")");
            return sb.ToString();
        }

        int ITuple.Size
        {
            get
            {
                return 5;
            }
        }
    }
}

#endif