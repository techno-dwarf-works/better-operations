using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Instructions;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public abstract class AsyncStage<TBuffer, TMember> : MemberedStage<TBuffer, TMember>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public virtual ExecuteInstruction ExecuteInstruction => ExecuteInstruction.WhenAllGood;

        public abstract Task<TBuffer> ExecuteAsync(TBuffer buffer);
    }
}