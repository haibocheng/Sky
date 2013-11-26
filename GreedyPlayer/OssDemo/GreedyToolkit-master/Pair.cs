using System.Collections.Generic;
using System.Text;
using GreedyToolkit.Extension;

namespace GreedyToolkit
{
    public class Pair<TFirst, TSecond>
    {
        public TFirst First { get; protected set; }
        public TSecond Second { get; protected set; }

        public Pair(TFirst first, TSecond second)
        {
            First = first;
            Second = second;
        }

        public bool Equals(Pair<TFirst, TSecond> other)
        {
            if (!First.Equals(other.First))
            {
                return false;
            }
            return Second.Equals(other.Second);
        }

        public override bool Equals(object other)
        {
            var pair = other as Pair<TFirst, TSecond>;
            if (pair == null)
            {
                return false;
            }
            return Equals(pair);
        }

        public override int GetHashCode()
        {
            return First.GetHashCode() << 5 ^ Second.GetHashCode();
        }

        public void ToCompactString(StringBuilder builder)
        {
            builder.AppendFormat("<{0}, {1}>", First, Second);
        }

        public string ToString(char separator)
        {
            return "{0}{1}{2}".Fill(First, separator, Second);
        }

        public override string ToString()
        {
            return ToString(',');
        }
    }

    public class Pair<T> : Pair<T, T>
    {
        public Pair(T first, T second)
            : base(first, second)
        {
        }
    }

    public class PairComparer<TFirst, TSecond> : IEqualityComparer<Pair<TFirst, TSecond>>
    {
        internal readonly static PairComparer<TFirst, TSecond> Instance;

        private readonly static EqualityComparer<TFirst> firstComparer;

        private readonly static EqualityComparer<TSecond> secondComparer;

        static PairComparer()
        {
            Instance = new PairComparer<TFirst, TSecond>();
            firstComparer = EqualityComparer<TFirst>.Default;
            secondComparer = EqualityComparer<TSecond>.Default;
        }

        private PairComparer()
        {
        }

        public bool Equals(Pair<TFirst, TSecond> x, Pair<TFirst, TSecond> y)
        {
            if (!firstComparer.Equals(x.First, y.First))
            {
                return false;
            }
            else
            {
                return secondComparer.Equals(x.Second, y.Second);
            }
        }

        public int GetHashCode(Pair<TFirst, TSecond> source)
        {
            return source.GetHashCode();
        }
    }
}