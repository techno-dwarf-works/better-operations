using Better.Commons.Runtime.Extensions;
using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Builders
{
    public abstract class ValueAsyncOperationBuilder<TBuilder, TOperation, TBuffer, TValue, TMember> : AsyncOperationBuilder<TBuilder, TOperation, TBuffer, TMember>
        where TBuilder : ValueAsyncOperationBuilder<TBuilder, TOperation, TBuffer, TValue, TMember>, new()
        where TOperation : ValueAsyncOperation<TBuffer, TValue, TMember>, new()
        where TBuffer : ValueAsyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        #region Notifications

        private ValueNotificationAsyncStage<TBuffer, TValue, TMember> NotificationStageAt(int index)
        {
            return StageAt<ValueNotificationAsyncStage<TBuffer, TValue, TMember>>(index);
        }

        protected virtual TBuilder InsertNotification(int index, ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnNotificationAsync notification)
        {
            var notificationAsyncStage = NotificationStageAt(index);
            notificationAsyncStage.Register(notification);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnNotificationAsync notification)
        {
            return InsertNotification(0, notification);
        }

        public TBuilder AppendNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnNotificationAsync notification)
        {
            return InsertNotification(Adapters.Count, notification);
        }

        protected virtual TBuilder InsertNotification(int index, ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetNotificationBy getter)
        {
            var notificationAsyncStage = NotificationStageAt(index);
            notificationAsyncStage.Register(getter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetNotificationBy getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetNotificationBy getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertNotification(int index, ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetTokenableNotificationBy getter)
        {
            var notificationAsyncStage = NotificationStageAt(index);
            notificationAsyncStage.Register(getter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetTokenableNotificationBy getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetTokenableNotificationBy getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertNotification(int index, ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnTokenableNotificationAsync getter)
        {
            var notificationAsyncStage = NotificationStageAt(index);
            notificationAsyncStage.Register(getter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnTokenableNotificationAsync getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnTokenableNotificationAsync getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertNotification(int index, ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetNotification getter)
        {
            var notificationAsyncStage = NotificationStageAt(index);
            notificationAsyncStage.Register(getter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetNotification getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetNotification getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertNotification(int index, ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetTokenableNotification getter)
        {
            var notificationAsyncStage = NotificationStageAt(index);
            notificationAsyncStage.Register(getter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetTokenableNotification getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetTokenableNotification getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        #endregion
    }

    public abstract class ValueAsyncOperationBuilder<TBuilder, TValue, TMember> : ValueAsyncOperationBuilder<TBuilder, ValueAsyncOperation<TValue, TMember>, ValueAsyncBuffer<TValue, TMember>, TValue, TMember>
        where TBuilder : ValueAsyncOperationBuilder<TBuilder, TValue, TMember>, new()
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    public class ValueAsyncOperationBuilder<TValue, TMember> : ValueAsyncOperationBuilder<ValueAsyncOperationBuilder<TValue, TMember>, TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    public class ValueAsyncOperationBuilder<TValue> : ValueAsyncOperationBuilder<ValueAsyncOperationBuilder<TValue>, ValueAsyncOperation<TValue>, ValueAsyncBuffer<TValue, IOperationMember>, TValue, IOperationMember>
        where TValue : struct
    {
    }
}