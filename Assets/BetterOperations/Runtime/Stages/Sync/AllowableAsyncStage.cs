using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Extensions;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public abstract class AllowableSyncStage<TBuffer, TMember> : SyncStage<TBuffer, TMember>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public sealed override bool TryExecute(TBuffer buffer)
        {
            var isAvailable = buffer.PermissionFlag.IsAllow();
            if (isAvailable)
            {
                Execute(buffer);
            }

            return isAvailable;
        }

        protected abstract void Execute(TBuffer buffer);
    }
}