using Better.Operations.Runtime.Buffers;

namespace Better.Operations.Runtime.Adapters
{
    public abstract class ValueSyncBufferAdapter<TBuffer, TValue, TMember> : SyncBufferAdapter<TBuffer, TMember>
        where TBuffer : ValueSyncOperationBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    public abstract class ValueSyncBufferAdapter<TValue, TMember> : ValueSyncBufferAdapter<ValueSyncOperationBuffer<TValue, TMember>, TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }
}