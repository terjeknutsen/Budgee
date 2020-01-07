using System;

namespace Budgee.Framework
{
    public abstract class Value<T> : IEquatable<T>
    {
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is T other && Equals(other);
        }
        public bool Equals(T other)
        {
            return CompareProperties(other);
        }

        protected abstract bool CompareProperties(T other);
        public override int GetHashCode()
        {
            var hashCode = 1779266240;
            foreach (var prop in GetType().GetProperties())
                hashCode = hashCode * -1521134295 + prop.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Value<T> left, Value<T> right) => Equals(left, right);
        public static bool operator !=(Value<T> left, Value<T> right) => !Equals(left, right);
    }
}
