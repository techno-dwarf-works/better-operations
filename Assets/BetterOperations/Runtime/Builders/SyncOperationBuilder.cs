using System;
using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Builders
{
    public abstract class SyncOperationBuilder<TBuilder, TOperation, TBuffer, TMember> : OperationBuilder<TOperation, TBuffer, SyncBufferAdapter<TBuffer, TMember>, TMember>
        where TBuilder : SyncOperationBuilder<TBuilder, TOperation, TBuffer, TMember>
        where TOperation : SyncOperation<TBuffer, SyncBufferAdapter<TBuffer, TMember>, TMember>, new()
        where TBuffer : SyncOperationBuffer<TMember>
        where TMember : IOperationMember
    {
        public TBuilder AppendNotify(Action action)
        {
            //GetOrAdd;
            var notifyStage = new NotifySyncOperationStage<TMember>();
            // var adapter = new DerivedSyncAdapter<TBuffer, TMember>(notifyStage);

            notifyStage.Register(action);
            // AppendAdapter(adapter);

            return (TBuilder)this;
        }
    }

    public abstract class SyncOperationBuilder<TBuilder, TOperation, TMember> : OperationBuilder<TOperation, SyncOperationBuffer<TMember>, SyncBufferAdapter<SyncOperationBuffer<TMember>, TMember>, TMember>
        where TBuilder : SyncOperationBuilder<TBuilder, TOperation, TMember>
        where TOperation : SyncOperation<SyncOperationBuffer<TMember>, SyncBufferAdapter<SyncOperationBuffer<TMember>, TMember>, TMember>, new()
        where TMember : IOperationMember
    {
    }

    public class SyncOperationBuilder<TMember> : SyncOperationBuilder<
        SyncOperationBuilder<TMember>,
        SyncOperation<TMember>,
        TMember
    >
        where TMember : IOperationMember
    {
    }
}