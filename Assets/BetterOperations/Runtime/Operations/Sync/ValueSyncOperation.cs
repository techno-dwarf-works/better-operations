using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime
{
    public abstract class ValueSyncOperation<TBuffer, TValue, TAdapter, TMember> : SyncOperation<TBuffer, TAdapter, TMember>
        where TBuffer : ValueSyncBuffer<TValue, TMember>
        where TValue : struct
        where TAdapter : SyncAdapter<TBuffer, TMember>
        where TMember : IOperationMember
    {
    }

    public class ValueSyncOperation<TValue, TMember> : ValueSyncOperation<ValueSyncBuffer<TValue, TMember>, TValue, SyncAdapter<ValueSyncBuffer<TValue, TMember>, TMember>, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        public void Run(TValue sourceValue)
        {
            var buffer = new ValueSyncBuffer<TValue, TMember>(Members, sourceValue);
            Run(buffer);
        }
    }
}