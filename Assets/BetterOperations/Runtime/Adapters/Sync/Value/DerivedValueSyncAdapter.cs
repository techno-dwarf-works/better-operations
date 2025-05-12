using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Adapters
{
    public class DerivedValueSyncAdapter<TBuffer, TValue, TMember> : ValueSyncBufferAdapter<TBuffer, TValue, TMember>
        where TBuffer : ValueSyncOperationBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        private SyncOperationStage<TBuffer, TMember> Stage { get; }

        public DerivedValueSyncAdapter(ValueSyncOperationStage<TBuffer, TValue, TMember> stage)
        {
            Stage = stage;
        }

        public override void Run(TBuffer buffer)
        {
            Stage.Run(buffer);
        }
    }
}