using System.Threading.Tasks;
using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime
{
    public abstract class ValueAsyncOperation<TBuffer, TAdapter, TValue, TMember> : AsyncOperation<TBuffer, TAdapter, TMember>
        where TBuffer : ValueAsyncBuffer<TValue, TMember>
        where TAdapter : AsyncAdapter<TBuffer, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    public abstract class ValueAsyncOperation<TBuffer, TValue, TMember> : ValueAsyncOperation<TBuffer, AsyncAdapter<TBuffer, TMember>, TValue, TMember>
        where TBuffer : ValueAsyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    public class ValueAsyncOperation<TValue, TMember> : ValueAsyncOperation<ValueAsyncBuffer<TValue, TMember>, TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        public Task<ValueAsyncBuffer<TValue, TMember>> ExecuteAsync(TValue sourceValue)
        {
            var buffer = new ValueAsyncBuffer<TValue, TMember>(Members, sourceValue);
            return ExecuteAsync(buffer);
        }
    }

    public class ValueAsyncOperation<TValue> : ValueAsyncOperation<TValue, IOperationMember>
        where TValue : struct
    {
    }
}