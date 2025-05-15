using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime
{
    public abstract class ValueSyncOperation<TBuffer, TAdapter, TValue, TMember> : SyncOperation<TBuffer, TAdapter, TMember>
        where TBuffer : ValueSyncBuffer<TValue, TMember>
        where TAdapter : SyncAdapter<TBuffer, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    public abstract class ValueSyncOperation<TBuffer, TValue, TMember> : ValueSyncOperation<TBuffer, SyncAdapter<TBuffer, TMember>, TValue, TMember>
        where TBuffer : ValueSyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    public class ValueSyncOperation<TValue, TMember> : ValueSyncOperation<ValueSyncBuffer<TValue, TMember>, TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        public TValue Run(TValue sourceValue)
        {
            var buffer = new ValueSyncBuffer<TValue, TMember>(Members, sourceValue);
            Run(buffer);

            return buffer.ModifiedValue;
        }
    }
}