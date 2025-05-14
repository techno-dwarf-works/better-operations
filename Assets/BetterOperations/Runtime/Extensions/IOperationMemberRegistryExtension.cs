using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Extensions
{
    public static class IOperationMemberRegistryExtension
    {
        public static bool Register<TMember>(this IOperationMemberRegistry<TMember> self, IOperationMember member)
            where TMember : IOperationMember
        {
            if (member is TMember castedMember)
            {
                var registered = self.Register(castedMember);
                return registered;
            }

            return false;
        }

        public static bool IsRegistered<TMember>(this IOperationMemberRegistry<TMember> self, IOperationMember member)
            where TMember : IOperationMember
        {
            if (member is TMember castedMember)
            {
                var registered = self.IsRegistered(castedMember);
                return registered;
            }

            return false;
        }

        public static bool Unregister<TMember>(this IOperationMemberRegistry<TMember> self, IOperationMember member)
            where TMember : IOperationMember
        {
            if (member is TMember castedMember)
            {
                var unregistered = self.Unregister(castedMember);
                return unregistered;
            }

            return false;
        }
    }
}