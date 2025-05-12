using System;
using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Builders
{
    public abstract class ValueSyncOperationBuilder<TBuilder, TOperation, TBuffer, TValue, TAdapter, TMember> : SyncOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TMember>
        where TBuilder : ValueSyncOperationBuilder<TBuilder, TOperation, TBuffer, TValue, TAdapter, TMember>
        where TOperation : ValueSyncOperation<TBuffer, TValue, TAdapter, TMember>, new()
        where TBuffer : ValueSyncOperationBuffer<TValue, TMember>
        where TValue : struct
        where TAdapter : ValueSyncBufferAdapter<TBuffer, TValue, TMember>
        where TMember : IOperationMember
    {
    }

    public class ValueSyncOperationBuilder<TBuilder, TValue, TMember> : ValueSyncOperationBuilder<TBuilder, ValueSyncOperation<TValue, TMember>, ValueSyncOperationBuffer<TValue, TMember>, TValue, ValueSyncBufferAdapter<TValue, TMember>, TMember>
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