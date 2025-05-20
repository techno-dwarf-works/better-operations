using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Extensions;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Permissions;

namespace Better.Operations.Runtime.Stages
{
    public abstract class PermissionableAsyncStage<TBuffer, TMember> : AsyncStage<TBuffer, TMember>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public sealed override async Task<bool> TryExecuteAsync(TBuffer buffer)
        {
            var isAvailable = IsAvailable(buffer.PermissionFlag);
            if (isAvailable)
            {
                await ExecuteAsync(buffer);
            }

            return isAvailable;
        }

        protected virtual bool IsAvailable(PermissionFlag permissionFlag)
        {
            var isAllow = permissionFlag.IsAllow();
            return isAllow;
        }

        protected abstract Task ExecuteAsync(TBuffer buffer);
    }
}