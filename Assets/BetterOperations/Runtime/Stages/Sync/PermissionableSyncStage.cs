using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Extensions;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Permissions;

namespace Better.Operations.Runtime.Stages
{
    public abstract class PermissionableSyncStage<TBuffer, TMember> : SyncStage<TBuffer, TMember>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public sealed override bool TryExecute(TBuffer buffer)
        {
            var isAvailable = IsAvailable(buffer.PermissionFlag);
            if (isAvailable)
            {
                Execute(buffer);
            }

            return isAvailable;
        }

        protected virtual bool IsAvailable(PermissionFlag permissionFlag)
        {
            var isAllow = permissionFlag.IsAllow();
            return isAllow;
        }

        protected abstract void Execute(TBuffer buffer);
    }
}