using System.Threading;
using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime
{
    public abstract class ValueAsyncOperation<TBuffer, TValue, TMember> : AsyncOperation<TBuffer, TMember>
        where TBuffer : ValueAsyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    public class ValueAsyncOperation<TValue, TMember> : ValueAsyncOperation<ValueAsyncBuffer<TValue, TMember>, TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        public Task<ValueAsyncBuffer<TValue, TMember>> ExecuteAsync(TValue sourceValue, CancellationToken cancellationToken)
        {
            var bufferTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, AliveToken);
            var buffer = new ValueAsyncBuffer<TValue, TMember>(Members, sourceValue, bufferTokenSource.Token);
            return ExecuteAsync(buffer);
        }
    }

    public class ValueAsyncOperation<TValue> : ValueAsyncOperation<TValue, IOperationMember>
        where TValue : struct
    {
    }
}