using System;
using System.Collections.Generic;
using Better.Commons.Runtime.Utility;
using Better.Operations.Runtime.Permissions;

namespace Better.Operations.Runtime.Extensions
{
    public static class PermissionFlagExtensions
    {
        public static PermissionFlag MostSignificant(this IEnumerable<PermissionFlag> self)
        {
            if (self == null)
            {
                var message = nameof(self);
                DebugUtility.LogException<ArgumentNullException>(message);
                return default;
            }

            var significantPermission = new PermissionFlag();
            foreach (var permissionFlag in self)
            {
                significantPermission = PermissionFlag.MostSignificant(significantPermission, permissionFlag);
            }

            return significantPermission;
        }

        public static bool IsMaxDeny(this PermissionFlag self)
        {
            return self.Value == PermissionValues.MaxDeny;
        }

        public static bool IsMinDeny(this PermissionFlag self)
        {
            return self.Value == PermissionValues.MinDeny;
        }

        public static bool IsDeny(this PermissionFlag self)
        {
            return self.Value < PermissionValues.Neutral;
        }

        public static bool IsAllDeny(this IEnumerable<PermissionFlag> self)
        {
            if (self == null)
            {
                var message = nameof(self);
                DebugUtility.LogException<ArgumentNullException>(message);
                return default;
            }

            foreach (var permissionFlag in self)
            {
                if (!permissionFlag.IsDeny())
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsAnyDeny(this IEnumerable<PermissionFlag> self)
        {
            if (self == null)
            {
                var message = nameof(self);
                DebugUtility.LogException<ArgumentNullException>(message);
                return default;
            }

            foreach (var permissionFlag in self)
            {
                if (permissionFlag.IsDeny())
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsNeutral(this PermissionFlag self)
        {
            return self.Value == PermissionValues.Neutral;
        }

        public static bool IsAllNeutral(this IEnumerable<PermissionFlag> self)
        {
            if (self == null)
            {
                var message = nameof(self);
                DebugUtility.LogException<ArgumentNullException>(message);
                return default;
            }

            foreach (var permissionFlag in self)
            {
                if (!permissionFlag.IsNeutral())
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsAnyNeutral(this IEnumerable<PermissionFlag> self)
        {
            if (self == null)
            {
                var message = nameof(self);
                DebugUtility.LogException<ArgumentNullException>(message);
                return default;
            }

            foreach (var permissionFlag in self)
            {
                if (permissionFlag.IsNeutral())
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsAllow(this PermissionFlag self)
        {
            return self.Value >= PermissionValues.Neutral;
        }

        public static bool IsAllAllow(this IEnumerable<PermissionFlag> self)
        {
            if (self == null)
            {
                var message = nameof(self);
                DebugUtility.LogException<ArgumentNullException>(message);
                return default;
            }

            foreach (var permissionFlag in self)
            {
                if (!permissionFlag.IsAllow())
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsAnyAllow(this IEnumerable<PermissionFlag> self)
        {
            if (self == null)
            {
                var message = nameof(self);
                DebugUtility.LogException<ArgumentNullException>(message);
                return default;
            }

            foreach (var permissionFlag in self)
            {
                if (permissionFlag.IsAllow())
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsMinAllow(this PermissionFlag self)
        {
            return self.Value == PermissionValues.MinAllow;
        }

        public static bool IsMaxAllow(this PermissionFlag self)
        {
            return self.Value == PermissionValues.MaxAllow;
        }
    }
}