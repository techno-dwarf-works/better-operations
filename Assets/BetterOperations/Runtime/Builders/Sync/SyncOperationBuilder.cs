using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Builders
{
    public abstract class SyncOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TMember> : MemberedOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TMember>
        where TBuilder : SyncOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TMember>
        where TOperation : SyncOperation<TBuffer, TAdapter, TMember>, new()
        where TBuffer : SyncBuffer<TMember>
        where TAdapter : SyncAdapter<TBuffer, TMember>
        where TMember : IOperationMember
    {
        // protected TBuilder InsertCallback(int index, Action action)
        // {
        //     var joinIndex = index - 1;
        //     var adapter = Adapters.ElementAtOrDefault(joinIndex, true);
        //     if (adapter?.Stage is not CallbackSyncStage<TBuffer, TMember> callbackSyncStage)
        //     {
        //         callbackSyncStage = new();
        //         adapter = new DerivedSyncAdapter<TBuffer, TMember>(callbackSyncStage);
        //         Adapters.Insert(index, adapter);
        //     }
        //
        //     callbackSyncStage.Register(action);
        //     return (TBuilder)this;
        // }
    }

    public abstract class SyncOperationBuilder<TBuilder, TOperation, TBuffer, TMember> : MemberedOperationBuilder<TBuilder, TOperation, TBuffer, SyncAdapter<TBuffer, TMember>, TMember>
        where TBuilder : SyncOperationBuilder<TBuilder, TOperation, TBuffer, TMember>
        where TOperation : SyncOperation<TBuffer, SyncAdapter<TBuffer, TMember>, TMember>, new()
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
    }

    public class SyncOperationBuilder<TBuilder, TMember> : SyncOperationBuilder<TBuilder, SyncOperation<TMember>, SyncBuffer<TMember>, TMember>
        where TBuilder : SyncOperationBuilder<TBuilder, TMember>
        where TMember : IOperationMember
    {
    }

    public class SyncOperationBuilder<TMember> : SyncOperationBuilder<SyncOperationBuilder<TMember>, TMember>
        where TMember : IOperationMember
    {
    }
}