using Better.Commons.Runtime.Extensions;
using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Builders
{
    public abstract class ContextualAsyncOperationBuilder<TBuilder, TOperation, TBuffer, TContext, TMember> : AsyncOperationBuilder<TBuilder, TOperation, TBuffer, TMember>
        where TBuilder : ContextualAsyncOperationBuilder<TBuilder, TOperation, TBuffer, TContext, TMember>, new()
        where TOperation : ContextualAsyncOperation<TBuffer, TContext, TMember>, new()
        where TBuffer : ContextualAsyncBuffer<TContext, TMember>
        where TMember : IOperationMember
    {
        #region Notifications

        private ContextualNotificationAsyncStage<TBuffer, TContext, TMember> NotificationStageAt(int index)
        {
            return StageAt<ContextualNotificationAsyncStage<TBuffer, TContext, TMember>>(index);
        }

        protected virtual TBuilder InsertNotification(int index, ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.OnNotificationAsync notification)
        {
            var notificationAsyncStage = NotificationStageAt(index);
            notificationAsyncStage.Register(notification);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.OnNotificationAsync notification)
        {
            return InsertNotification(0, notification);
        }

        public TBuilder AppendNotification(ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.OnNotificationAsync notification)
        {
            return InsertNotification(Adapters.Count, notification);
        }

        protected virtual TBuilder InsertNotification(int index, ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.GetNotificationBy getter)
        {
            var notificationAsyncStage = NotificationStageAt(index);
            notificationAsyncStage.Register(getter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.GetNotificationBy getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.GetNotificationBy getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertNotification(int index, ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.GetTokenableNotificationBy getter)
        {
            var notificationAsyncStage = NotificationStageAt(index);
            notificationAsyncStage.Register(getter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.GetTokenableNotificationBy getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.GetTokenableNotificationBy getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertNotification(int index, ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.OnTokenableNotificationAsync getter)
        {
            var notificationAsyncStage = NotificationStageAt(index);
            notificationAsyncStage.Register(getter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.OnTokenableNotificationAsync getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.OnTokenableNotificationAsync getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertNotification(int index, ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.GetNotification getter)
        {
            var notificationAsyncStage = NotificationStageAt(index);
            notificationAsyncStage.Register(getter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.GetNotification getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.GetNotification getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertNotification(int index, ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.GetTokenableNotification getter)
        {
            var notificationAsyncStage = NotificationStageAt(index);
            notificationAsyncStage.Register(getter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.GetTokenableNotification getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.GetTokenableNotification getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        #endregion
    }

    public abstract class ContextualAsyncOperationBuilder<TBuilder, TContext, TMember> : ContextualAsyncOperationBuilder<TBuilder, ContextualAsyncOperation<TContext, TMember>, ContextualAsyncBuffer<TContext, TMember>, TContext, TMember>
        where TBuilder : ContextualAsyncOperationBuilder<TBuilder, TContext, TMember>, new()
        where TMember : IOperationMember
    {
    }

    public class ContextualAsyncOperationBuilder<TContext, TMember> : ContextualAsyncOperationBuilder<ContextualAsyncOperationBuilder<TContext, TMember>, TContext, TMember>
        where TMember : IOperationMember
    {
    }

    public class ContextualAsyncOperationBuilder<TContext> : ContextualAsyncOperationBuilder<ContextualAsyncOperationBuilder<TContext>, ContextualAsyncOperation<TContext>, ContextualAsyncBuffer<TContext, IOperationMember>, TContext, IOperationMember>
    {
    }
}