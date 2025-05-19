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
        protected virtual TStage StageAt<TStage>(int index)
            where TStage : SyncStage<TBuffer, TMember>, new()
        {
            var joinIndex = index - 1;
            var adapter = Adapters.ElementAtOrDefault(joinIndex, true);
            if (adapter?.Stage is not TStage stage)
            {
                stage = new();
                adapter = new SyncAdapter<TBuffer, TStage, TMember>(stage);
                Adapters.Insert(index, adapter);
            }

            return stage;
        }

        #region Notifications

        private NotificationSyncStage<TBuffer, TMember> NotificationStageAt(int index)
        {
            return StageAt<NotificationSyncStage<TBuffer, TMember>>(index);
        }

        protected virtual TBuilder InsertNotification(int index, NotificationSyncStage<TBuffer, TMember>.OnNotification notification)
        {
            var notificationSyncStage = NotificationStageAt(index);
            notificationSyncStage.Register(notification);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(NotificationSyncStage<TBuffer, TMember>.OnNotification notification)
        {
            return InsertNotification(0, notification);
        }

        public TBuilder AppendNotification(NotificationSyncStage<TBuffer, TMember>.OnNotification notification)
        {
            return InsertNotification(Adapters.Count, notification);
        }

        protected virtual TBuilder InsertNotification(int index, NotificationSyncStage<TBuffer, TMember>.GetNotificationBy getter)
        {
            var notificationSyncStage = NotificationStageAt(index);
            notificationSyncStage.Register(getter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(NotificationSyncStage<TBuffer, TMember>.GetNotificationBy getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(NotificationSyncStage<TBuffer, TMember>.GetNotificationBy getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        #endregion
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