using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime
{
    public abstract class ValueSyncOperation<TBuffer, TValue, TMember> : SyncOperation<TBuffer, TMember>
        where TBuffer : ValueSyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    public class ValueSyncOperation<TValue, TMember> : ValueSyncOperation<ValueSyncBuffer<TValue, TMember>, TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        public ValueSyncBuffer<TValue, TMember> Execute(TValue source)
        {
            var buffer = new ValueSyncBuffer<TValue, TMember>(Members, source);
            return Execute(buffer);
        }
    }

    public class ValueSyncOperation<TValue> : ValueSyncOperation<TValue, IOperationMember>
        where TValue : struct
    {
    }
}