using System;
using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Builders
{
    public abstract class SyncOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TMember> : OperationBuilder<TOperation, TBuffer, TAdapter, TMember>
        where TBuilder : SyncOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TMember>
        where TOperation : SyncOperation<TBuffer, TAdapter, TMember>, new()
        where TBuffer : SyncOperationBuffer<TMember>
        where TAdapter : SyncBufferAdapter<TBuffer, TMember>
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

    public class SyncOperationBuilder<TBuilder, TMember> : SyncOperationBuilder<TBuilder, SyncOperation<TMember>, SyncOperationBuffer<TMember>, SyncBufferAdapter<SyncOperationBuffer<TMember>, TMember>, TMember>
        where TBuilder : SyncOperationBuilder<TBuilder, TMember>
        where TMember : IOperationMember
    {
    }

    public class SyncOperationBuilder<TMember> : SyncOperationBuilder<SyncOperationBuilder<TMember>, TMember>
        where TMember : IOperationMember
    {
    }
}