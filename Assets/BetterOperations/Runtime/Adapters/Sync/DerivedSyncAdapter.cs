using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Adapters
{
    public class DerivedSyncAdapter<TBuffer, TMember> : SyncBufferAdapter<TBuffer, TMember>
        where TBuffer : SyncOperationBuffer<TMember>
        where TMember : IOperationMember
    {
        private SyncOperationStage<TBuffer, TMember> Stage { get; }

        public DerivedSyncAdapter(SyncOperationStage<TBuffer, TMember> stage)
        {
            Stage = stage;
        }

        public override void Run(TBuffer buffer)
        {
            Stage.Run(buffer);
        }
    }
}