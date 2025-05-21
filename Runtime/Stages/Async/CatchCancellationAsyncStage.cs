using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Instructions;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Permissions;

namespace Better.Operations.Runtime.Stages
{
    public class CatchCancellationAsyncStage<TBuffer, TMember> : AsyncStage<TBuffer, TMember>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public override ExecuteInstruction ExecuteInstruction => ExecuteInstruction.Optional;

        public override Task<bool> TryExecuteAsync(TBuffer buffer)
        {
            if (!buffer.IsCancellationRequested)
            {
                return Task.FromResult(false);
            }

            var permissionFlag = PermissionFlag.Create(PermissionValues.MaxDeny);
            var flagChanged = buffer.SetPermissionFlag(permissionFlag);
            
            return Task.FromResult(flagChanged);
        }
    }
}