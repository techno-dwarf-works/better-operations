using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Extensions;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public abstract class AllowableAsyncStage<TBuffer, TMember> : AsyncStage<TBuffer, TMember>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public sealed override async Task<bool> TryExecuteAsync(TBuffer buffer)
        {
            var isAvailable = buffer.PermissionFlag.IsAllow();
            if (isAvailable)
            {
                await ExecuteAsync(buffer);
            }

            return isAvailable;
        }

        protected abstract Task ExecuteAsync(TBuffer buffer);
    }
}