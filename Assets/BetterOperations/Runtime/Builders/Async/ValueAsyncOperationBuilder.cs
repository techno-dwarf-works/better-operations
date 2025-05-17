using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Builders
{
    public abstract class ValueAsyncOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TValue, TMember> : AsyncOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TMember>
        where TBuilder : ValueAsyncOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TValue, TMember>, new()
        where TOperation : ValueAsyncOperation<TBuffer, TAdapter, TValue, TMember>, new()
        where TBuffer : ValueAsyncBuffer<TValue, TMember>
        where TAdapter : AsyncAdapter<TBuffer, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    public abstract class ValueAsyncOperationBuilder<TBuilder, TOperation, TBuffer, TValue, TMember> : ValueAsyncOperationBuilder<TBuilder, TOperation, TBuffer, AsyncAdapter<TBuffer, TMember>, TValue, TMember>
        where TBuilder : ValueAsyncOperationBuilder<TBuilder, TOperation, TBuffer, TValue, TMember>, new()
        where TOperation : ValueAsyncOperation<TBuffer, TValue, TMember>, new()
        where TBuffer : ValueAsyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    public abstract class ValueAsyncOperationBuilder<TBuilder, TValue, TMember> : ValueAsyncOperationBuilder<TBuilder, ValueAsyncOperation<TValue, TMember>, ValueAsyncBuffer<TValue, TMember>, TValue, TMember>
        where TBuilder : ValueAsyncOperationBuilder<TBuilder, TValue, TMember>, new()
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    public class ValueAsyncOperationBuilder<TValue, TMember> : ValueAsyncOperationBuilder<ValueAsyncOperationBuilder<TValue, TMember>, TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }
}