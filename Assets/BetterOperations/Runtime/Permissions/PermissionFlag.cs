using System;
using Better.Commons.Runtime.Utility;
using Better.Operations.Runtime.Extensions;

namespace Better.Operations.Runtime.Permissions
{
    public readonly struct PermissionFlag : IComparable<PermissionFlag>
    {
        public int Value { get; }

        private PermissionFlag(int value)
        {
            Value = value;
        }

        public int CompareTo(PermissionFlag other)
        {
            return Value.CompareTo(other.Value);
        }

        public bool Equals(PermissionFlag other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is PermissionFlag other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator <(PermissionFlag a, PermissionFlag b) => a.Value < b.Value;
        public static bool operator >(PermissionFlag a, PermissionFlag b) => a.Value > b.Value;
        public static bool operator <=(PermissionFlag a, PermissionFlag b) => a.Value <= b.Value;
        public static bool operator >=(PermissionFlag a, PermissionFlag b) => a.Value >= b.Value;
        public static bool operator ==(PermissionFlag a, PermissionFlag b) => a.Value == b.Value;
        public static bool operator !=(PermissionFlag a, PermissionFlag b) => a.Value != b.Value;

        public static PermissionFlag MostSignificant(PermissionFlag a, PermissionFlag b)
        {
            if (a.Value == b.Value)
            {
                return a;
            }

            if (a.IsNeutral())
            {
                return b;
            }

            if (b.IsNeutral())
            {
                return a;
            }

            var absA = Math.Abs(a.Value);
            var absB = Math.Abs(b.Value);

            if (absA > absB)
            {
                return a;
            }

            if (absB > absA)
            {
                return b;
            }

            if (a.Value < 0 && b.Value > 0)
            {
                return a;
            }

            if (b.Value < 0 && a.Value > 0)
            {
                return b;
            }

            var unexpectedMessage = $"Unexpected state of {nameof(a)}({a}) and {nameof(b)}({b})";
            DebugUtility.LogException<InvalidOperationException>(unexpectedMessage);
            return default;
        }

        public static PermissionFlag Create(int value)
        {
            var permissionFlag = new PermissionFlag(value);
            return permissionFlag;
        }
    }
}