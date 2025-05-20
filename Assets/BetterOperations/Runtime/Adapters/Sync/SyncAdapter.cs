using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Instructions;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Adapters
{
    public abstract class SyncAdapter<TBuffer, TMember> : MemberedAdapter<TBuffer, TMember>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public abstract ExecuteInstruction ExecuteInstruction { get; }

        public abstract bool TryExecute(TBuffer buffer);
    }

    public class SyncAdapter<TBuffer, TStage, TMember> : SyncAdapter<TBuffer, TMember>
        where TBuffer : SyncBuffer<TMember>
        where TStage : SyncStage<TBuffer, TMember>
        where TMember : IOperationMember
    {
        public override ExecuteInstruction ExecuteInstruction => Stage.ExecuteInstruction;
        public TStage Stage { get; }

        public SyncAdapter(TStage stage)
        {
            Stage = stage;
        }

        public override bool TryExecute(TBuffer buffer)
        {
            return Stage.TryExecute(buffer);
        }
    }
}