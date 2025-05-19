using System.Threading;
using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime
{
    public abstract class ContextualAsyncOperation<TBuffer, TContext, TMember> : AsyncOperation<TBuffer, TMember>
        where TBuffer : ContextualAsyncBuffer<TContext, TMember>
        where TMember : IOperationMember
    {
    }

    public class ContextualAsyncOperation<TContext, TMember> : ContextualAsyncOperation<ContextualAsyncBuffer<TContext, TMember>, TContext, TMember>
        where TMember : IOperationMember
    {
        public Task<ContextualAsyncBuffer<TContext, TMember>> ExecuteAsync(TContext context, CancellationToken cancellationToken)
        {
            var buffer = new ContextualAsyncBuffer<TContext, TMember>(Members, context, cancellationToken);
            return ExecuteAsync(buffer);
        }
    }

    public class ContextualAsyncOperation<TContext> : ContextualAsyncOperation<TContext, IOperationMember>
    {
    }
}