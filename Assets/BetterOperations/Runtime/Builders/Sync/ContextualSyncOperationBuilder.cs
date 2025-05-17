using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Builders
{
    public abstract class ContextualSyncOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TContext, TMember> : SyncOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TMember>
        where TBuilder : ContextualSyncOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TContext, TMember>, new()
        where TOperation : ContextualSyncOperation<TBuffer, TAdapter, TContext, TMember>, new()
        where TBuffer : ContextualSyncBuffer<TContext, TMember>
        where TAdapter : SyncAdapter<TBuffer, TMember>
        where TMember : IOperationMember
    {
    }

    public abstract class ContextualSyncOperationBuilder<TBuilder, TOperation, TBuffer, TContext, TMember> : ContextualSyncOperationBuilder<TBuilder, TOperation, TBuffer, SyncAdapter<TBuffer, TMember>, TContext, TMember>
        where TBuilder : ContextualSyncOperationBuilder<TBuilder, TOperation, TBuffer, TContext, TMember>, new()
        where TOperation : ContextualSyncOperation<TBuffer, TContext, TMember>, new()
        where TBuffer : ContextualSyncBuffer<TContext, TMember>
        where TMember : IOperationMember
    {
    }

    public abstract class ContextualSyncOperationBuilder<TBuilder, TContext, TMember> : ContextualSyncOperationBuilder<TBuilder, ContextualSyncOperation<TContext, TMember>, ContextualSyncBuffer<TContext, TMember>, TContext, TMember>
        where TBuilder : ContextualSyncOperationBuilder<TBuilder, TContext, TMember>, new()
        where TMember : IOperationMember
    {
    }

    public class ContextualSyncOperationBuilder<TContext, TMember> : ContextualSyncOperationBuilder<ContextualSyncOperationBuilder<TContext, TMember>, TContext, TMember>
        where TMember : IOperationMember
    {
    }

    public class ContextualSyncOperationBuilder<TContext> : ContextualSyncOperationBuilder<ContextualSyncOperationBuilder<TContext>, ContextualSyncOperation<TContext>, ContextualSyncBuffer<TContext, IOperationMember>, TContext, IOperationMember>
    {
    }
}