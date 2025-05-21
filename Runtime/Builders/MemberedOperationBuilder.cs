using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Builders
{
    public abstract class MemberedOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TMember> : OperationBuilder<TBuilder, TOperation, TBuffer, TAdapter>
        where TBuilder : OperationBuilder<TBuilder, TOperation, TBuffer, TAdapter>, new()
        where TOperation : MemberedOperation<TBuffer, TAdapter, TMember>, new()
        where TBuffer : MemberedBuffer<TMember>
        where TAdapter : MemberedAdapter<TBuffer, TMember>
        where TMember : IOperationMember
    {
    }

    public abstract class MemberedOperationBuilder<TBuilder, TOperation, TAdapter, TMember> : MemberedOperationBuilder<TBuilder, TOperation, MemberedBuffer<TMember>, TAdapter, TMember>
        where TBuilder : OperationBuilder<TBuilder, TOperation, MemberedBuffer<TMember>, TAdapter>, new()
        where TOperation : MemberedOperation<MemberedBuffer<TMember>, TAdapter, TMember>, new()
        where TMember : IOperationMember
        where TAdapter : MemberedAdapter<MemberedBuffer<TMember>, TMember>
    {
    }
}