using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Builders
{
    public abstract class ContextualAsyncOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TContext, TMember> : AsyncOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TMember>
        where TBuilder : ContextualAsyncOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TContext, TMember>, new()
        where TOperation : ContextualAsyncOperation<TBuffer, TAdapter, TContext, TMember>, new()
        where TBuffer : ContextualAsyncBuffer<TContext, TMember>
        where TAdapter : AsyncAdapter<TBuffer, TMember>
        where TMember : IOperationMember
    {
    }

    public abstract class ContextualAsyncOperationBuilder<TBuilder, TOperation, TBuffer, TContext, TMember> : ContextualAsyncOperationBuilder<TBuilder, TOperation, TBuffer, AsyncAdapter<TBuffer, TMember>, TContext, TMember>
        where TBuilder : ContextualAsyncOperationBuilder<TBuilder, TOperation, TBuffer, TContext, TMember>, new()
        where TOperation : ContextualAsyncOperation<TBuffer, TContext, TMember>, new()
        where TBuffer : ContextualAsyncBuffer<TContext, TMember>
        where TMember : IOperationMember
    {
    }

    public abstract class ContextualAsyncOperationBuilder<TBuilder, TContext, TMember> : ContextualAsyncOperationBuilder<TBuilder, ContextualAsyncOperation<TContext, TMember>, ContextualAsyncBuffer<TContext, TMember>, TContext, TMember>
        where TBuilder : ContextualAsyncOperationBuilder<TBuilder, TContext, TMember>, new()
        where TMember : IOperationMember
    {
    }

    public class ContextualAsyncOperationBuilder<TContext, TMember> : ContextualAsyncOperationBuilder<ContextualAsyncOperationBuilder<TContext, TMember>, TContext, TMember>
        where TMember : IOperationMember
    {
    }

    public class ContextualAsyncOperationBuilder<TContext> : ContextualAsyncOperationBuilder<ContextualAsyncOperationBuilder<TContext>, ContextualAsyncOperation<TContext>, ContextualAsyncBuffer<TContext, IOperationMember>, TContext, IOperationMember>
    {
    }
}