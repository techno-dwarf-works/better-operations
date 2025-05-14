using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Adapters
{
    public abstract class SyncAdapter<TBuffer, TMember> : MemberedAdapter<TBuffer, TMember>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public abstract void Run(TBuffer buffer);
    }

    public abstract class SyncAdapter<TBuffer, TStage, TMember> : SyncAdapter<TBuffer, TMember>
        where TBuffer : SyncBuffer<TMember>
        where TStage : SyncStage<TBuffer, TMember>
        where TMember : IOperationMember
    {
        public override OperationStage<TBuffer> Stage => RelativeStage;
        public TStage RelativeStage { get; }

        protected SyncAdapter(TStage stage)
        {
            RelativeStage = stage;
        }
    }
}