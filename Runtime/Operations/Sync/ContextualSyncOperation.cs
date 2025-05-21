using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime
{
    public abstract class ContextualSyncOperation<TBuffer, TContext, TMember> : SyncOperation<TBuffer, TMember>
        where TBuffer : ContextualSyncBuffer<TContext, TMember>
        where TMember : IOperationMember
    {
    }

    public class ContextualSyncOperation<TContext, TMember> : ContextualSyncOperation<ContextualSyncBuffer<TContext, TMember>, TContext, TMember>
        where TMember : IOperationMember
    {
        public ContextualSyncBuffer<TContext, TMember> Execute(TContext context)
        {
            var buffer = new ContextualSyncBuffer<TContext, TMember>(Members, context);
            return Execute(buffer);
        }
    }

    public class ContextualSyncOperation<TContext> : ContextualSyncOperation<TContext, IOperationMember>
    {
    }
}