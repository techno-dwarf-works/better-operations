using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Extensions
{
    public static class MemberedOperationExtensions
    {
        public static bool TryRegister<TBuffer, TAdapter, TMember>(this MemberedOperation<TBuffer, TAdapter, TMember> self, IOperationMember member)
            where TBuffer : MemberedBuffer<TMember>
            where TAdapter : MemberedAdapter<TBuffer, TMember>
            where TMember : IOperationMember
        {
            if (member is TMember castedMember)
            {
                var registered = self.Register(castedMember);
                return registered;
            }

            return false;
        }

        public static bool IsRegistered<TBuffer, TAdapter, TMember>(this MemberedOperation<TBuffer, TAdapter, TMember> self, IOperationMember member)
            where TBuffer : MemberedBuffer<TMember>
            where TAdapter : MemberedAdapter<TBuffer, TMember>
            where TMember : IOperationMember
        {
            if (member is TMember castedMember)
            {
                var registered = self.IsRegistered(castedMember);
                return registered;
            }

            return false;
        }

        public static bool Unregister<TBuffer, TAdapter, TMember>(this MemberedOperation<TBuffer, TAdapter, TMember> self, IOperationMember member)
            where TBuffer : MemberedBuffer<TMember>
            where TAdapter : MemberedAdapter<TBuffer, TMember>
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