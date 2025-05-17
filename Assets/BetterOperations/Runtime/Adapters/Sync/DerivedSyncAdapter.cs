using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Adapters
{
    public class DerivedSyncAdapter<TBuffer, TMember> : SyncAdapter<TBuffer, SyncStage<TBuffer, TMember>, TMember>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public DerivedSyncAdapter(SyncStage<TBuffer, TMember> stage) : base(stage)
        {
        }

        public override TBuffer Run(TBuffer buffer)
        {
            return RelativeStage.Execute(buffer);
        }
    }
}