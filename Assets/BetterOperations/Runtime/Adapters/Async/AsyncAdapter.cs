using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Instructions;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Adapters
{
    public abstract class AsyncAdapter<TBuffer, TMember> : MemberedAdapter<TBuffer, TMember>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public abstract ExecuteInstruction ExecuteInstruction { get; }

        public abstract Task<bool> TryExecuteAsync(TBuffer buffer);
    }

    public class AsyncAdapter<TBuffer, TStage, TMember> : AsyncAdapter<TBuffer, TMember>
        where TBuffer : AsyncBuffer<TMember>
        where TStage : AsyncStage<TBuffer, TMember>
        where TMember : IOperationMember
    {
        public override OperationStage<TBuffer> Stage => RelativeStage;
        public override ExecuteInstruction ExecuteInstruction => RelativeStage.ExecuteInstruction;
        public TStage RelativeStage { get; }

        public AsyncAdapter(TStage stage)
        {
            RelativeStage = stage;
        }

        public override Task<bool> TryExecuteAsync(TBuffer buffer)
        {
            return RelativeStage.TryExecuteAsync(buffer);
        }
    }
}