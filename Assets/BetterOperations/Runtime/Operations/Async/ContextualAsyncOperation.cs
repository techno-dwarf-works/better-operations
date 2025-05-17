using System.Threading.Tasks;
using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime
{
    public abstract class ContextualAsyncOperation<TBuffer, TAdapter, TContext, TMember> : AsyncOperation<TBuffer, TAdapter, TMember>
        where TBuffer : ContextualAsyncBuffer<TContext, TMember>
        where TAdapter : AsyncAdapter<TBuffer, TMember>
        where TMember : IOperationMember
    {
    }

    public abstract class ContextualAsyncOperation<TBuffer, TContext, TMember> : ContextualAsyncOperation<TBuffer, AsyncAdapter<TBuffer, TMember>, TContext, TMember>
        where TBuffer : ContextualAsyncBuffer<TContext, TMember>
        where TMember : IOperationMember
    {
    }

    public class ContextualAsyncOperation<TContext, TMember> : ContextualAsyncOperation<ContextualAsyncBuffer<TContext, TMember>, TContext, TMember>
        where TMember : IOperationMember
    {
        public Task<ContextualAsyncBuffer<TContext, TMember>> ExecuteAsync(TContext context)
        {
            var buffer = new ContextualAsyncBuffer<TContext, TMember>(Members, context);
            return ExecuteAsync(buffer);
        }
    }

    public class ContextualAsyncOperation<TContext> : ContextualAsyncOperation<TContext, IOperationMember>
    {
    }
}