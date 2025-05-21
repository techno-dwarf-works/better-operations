namespace Better.Operations.Runtime.Permissions
{
    public static class PermissionValues
    {
        public const int MaxDeny = int.MinValue;
        public const int MinDeny = -1;
        public const int Neutral = 0;
        public const int MinAllow = 1;
        public const int MaxAllow = int.MaxValue;
    }
}