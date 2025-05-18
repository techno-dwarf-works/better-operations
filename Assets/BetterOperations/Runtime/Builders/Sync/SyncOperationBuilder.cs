using Better.Commons.Runtime.Extensions;
using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Builders
{
    public abstract class SyncOperationBuilder<TBuilder, TOperation, TBuffer, TMember> : MemberedOperationBuilder<TBuilder, TOperation, TBuffer, SyncAdapter<TBuffer, TMember>, TMember>
        where TBuilder : SyncOperationBuilder<TBuilder, TOperation, TBuffer, TMember>, new()
        where TOperation : SyncOperation<TBuffer, TMember>, new()
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
        protected TBuilder InsertNotification(int index, NotificationSyncStage<TBuffer, TMember>.OnNotification notification)
        {
            var joinIndex = index - 1;
            var adapter = Adapters.ElementAtOrDefault(joinIndex, true);
            if (adapter?.Stage is not NotificationSyncStage<TBuffer, TMember> notificationSyncStage)
            {
                notificationSyncStage = new();
                var aaadapter = new DerivedSyncAdapter<TBuffer, TMember>(notificationSyncStage);
                Adapters.Insert(index, aaadapter);
            }

            // notificationSyncStage.Register(notification);
            return (TBuilder)this;
        }
    }

    public abstract class SyncOperationBuilder<TBuilder, TMember> : SyncOperationBuilder<TBuilder, SyncOperation<TMember>, SyncBuffer<TMember>, TMember>
        where TBuilder : SyncOperationBuilder<TBuilder, TMember>, new()
        where TMember : IOperationMember
    {
    }

    public class SyncOperationBuilder<TMember> : SyncOperationBuilder<SyncOperationBuilder<TMember>, TMember>
        where TMember : IOperationMember
    {
    }

    public class SyncOperationBuilder : SyncOperationBuilder<SyncOperationBuilder, SyncOperation, SyncBuffer<IOperationMember>, IOperationMember>
    {
    }
}