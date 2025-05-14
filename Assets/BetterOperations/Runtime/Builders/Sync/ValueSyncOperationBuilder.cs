using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Builders
{
    public abstract class ValueSyncOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TValue, TMember> : SyncOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TMember>
        where TBuilder : ValueSyncOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TValue, TMember>
        where TOperation : ValueSyncOperation<TBuffer, TAdapter, TValue, TMember>, new()
        where TBuffer : ValueSyncBuffer<TValue, TMember>
        where TAdapter : SyncAdapter<TBuffer, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    public abstract class ValueSyncOperationBuilder<TBuilder, TOperation, TBuffer, TValue, TMember> : ValueSyncOperationBuilder<TBuilder, TOperation, TBuffer, SyncAdapter<TBuffer, TMember>, TValue, TMember>
        where TBuilder : ValueSyncOperationBuilder<TBuilder, TOperation, TBuffer, TValue, TMember>
        where TOperation : ValueSyncOperation<TBuffer, TValue, TMember>, new()
        where TBuffer : ValueSyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    public abstract class ValueSyncOperationBuilder<TBuilder, TValue, TMember> : ValueSyncOperationBuilder<TBuilder, ValueSyncOperation<TValue, TMember>, ValueSyncBuffer<TValue, TMember>, TValue, TMember>
        where TBuilder : ValueSyncOperationBuilder<TBuilder, TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    public class ValueSyncOperationBuilder<TValue, TMember> : ValueSyncOperationBuilder<ValueSyncOperationBuilder<TValue, TMember>, TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }
}