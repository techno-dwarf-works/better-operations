using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime
{
    public abstract class ContextualSyncOperation<TBuffer, TAdapter, TContext, TMember> : SyncOperation<TBuffer, TAdapter, TMember>
        where TBuffer : ContextualSyncBuffer<TContext, TMember>
        where TAdapter : SyncAdapter<TBuffer, TMember>
        where TMember : IOperationMember
    {
    }

    public abstract class ContextualSyncOperation<TBuffer, TContext, TMember> : ContextualSyncOperation<TBuffer, SyncAdapter<TBuffer, TMember>, TContext, TMember>
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