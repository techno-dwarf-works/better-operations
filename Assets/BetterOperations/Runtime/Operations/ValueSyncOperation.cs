using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;

namespace Better.Operations.Runtime
{
    public abstract class ValueSyncOperation<TBuffer, TValue, TAdapter, TMember> : SyncOperation<TBuffer, TAdapter, TMember>
        where TBuffer : ValueSyncOperationBuffer<TValue, TMember>
        where TValue : struct
        where TAdapter : ValueSyncBufferAdapter<TBuffer, TValue, TMember>
        where TMember : IOperationMember
    {
    }

    public class ValueSyncOperation<TValue, TMember> : ValueSyncOperation<ValueSyncOperationBuffer<TValue, TMember>, TValue, ValueSyncBufferAdapter<TValue, TMember>, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }
}