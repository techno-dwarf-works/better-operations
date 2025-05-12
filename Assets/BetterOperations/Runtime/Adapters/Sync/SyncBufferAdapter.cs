using Better.Operations.Runtime.Buffers;

namespace Better.Operations.Runtime.Adapters
{
    public abstract class SyncBufferAdapter<TBuffer, TMember> : BufferStageAdapter<TBuffer, TMember>
        where TBuffer : SyncOperationBuffer<TMember>
        where TMember : IOperationMember
    {
        public abstract void Run(TBuffer buffer);
    }

    public abstract class SyncBufferAdapter<TMember> : SyncBufferAdapter<SyncOperationBuffer<TMember>, TMember>
        where TMember : IOperationMember
    {
    }
}