using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Builders
{
    public abstract class ContextualSyncOperationBuilder<TBuilder, TOperation, TBuffer, TContext, TMember> : SyncOperationBuilder<TBuilder, TOperation, TBuffer, TMember>
        where TBuilder : ContextualSyncOperationBuilder<TBuilder, TOperation, TBuffer, TContext, TMember>, new()
        where TOperation : ContextualSyncOperation<TBuffer, TContext, TMember>, new()
        where TBuffer : ContextualSyncBuffer<TContext, TMember>
        where TMember : IOperationMember
    {
        #region Notifications

        protected virtual TBuilder InsertNotification(int index, ContextualNotificationSyncStage<TBuffer, TContext, TMember>.OnNotification notification)
        {
            var notificationSyncStage = new ContextualNotificationSyncStage<TBuffer, TContext, TMember>(notification);
            var adapter = new SyncAdapter<TBuffer, ContextualNotificationSyncStage<TBuffer, TContext, TMember>, TMember>(notificationSyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ContextualNotificationSyncStage<TBuffer, TContext, TMember>.OnNotification notification)
        {
            return InsertNotification(0, notification);
        }

        public TBuilder AppendNotification(ContextualNotificationSyncStage<TBuffer, TContext, TMember>.OnNotification notification)
        {
            return InsertNotification(Adapters.Count, notification);
        }

        protected virtual TBuilder InsertNotification(int index, ContextualNotificationSyncStage<TBuffer, TContext, TMember>.GetDelegate getter)
        {
            var notificationSyncStage = new ContextualNotificationSyncStage<TBuffer, TContext, TMember>(getter);
            var adapter = new SyncAdapter<TBuffer, ContextualNotificationSyncStage<TBuffer, TContext, TMember>, TMember>(notificationSyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ContextualNotificationSyncStage<TBuffer, TContext, TMember>.GetDelegate getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(ContextualNotificationSyncStage<TBuffer, TContext, TMember>.GetDelegate getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        #endregion
    }

    public abstract class ContextualSyncOperationBuilder<TBuilder, TContext, TMember> : ContextualSyncOperationBuilder<TBuilder, ContextualSyncOperation<TContext, TMember>, ContextualSyncBuffer<TContext, TMember>, TContext, TMember>
        where TBuilder : ContextualSyncOperationBuilder<TBuilder, TContext, TMember>, new()
        where TMember : IOperationMember
    {
    }

    public class ContextualSyncOperationBuilder<TContext, TMember> : ContextualSyncOperationBuilder<ContextualSyncOperationBuilder<TContext, TMember>, TContext, TMember>
        where TMember : IOperationMember
    {
    }

    public class ContextualSyncOperationBuilder<TContext> : ContextualSyncOperationBuilder<ContextualSyncOperationBuilder<TContext>, ContextualSyncOperation<TContext>, ContextualSyncBuffer<TContext, IOperationMember>, TContext, IOperationMember>
    {
    }
}