using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Builders
{
    public abstract class ValueSyncOperationBuilder<TBuilder, TOperation, TBuffer, TValue, TMember> : SyncOperationBuilder<TBuilder, TOperation, TBuffer, TMember>
        where TBuilder : ValueSyncOperationBuilder<TBuilder, TOperation, TBuffer, TValue, TMember>, new()
        where TOperation : ValueSyncOperation<TBuffer, TValue, TMember>, new()
        where TBuffer : ValueSyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        #region Notifications

        protected virtual TBuilder InsertNotification(int index, ValueNotificationSyncStage<TBuffer, TValue, TMember>.OnNotification notification)
        {
            var notificationSyncStage = new ValueNotificationSyncStage<TBuffer, TValue, TMember>(notification);
            var adapter = new SyncAdapter<TBuffer, ValueNotificationSyncStage<TBuffer, TValue, TMember>, TMember>(notificationSyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ValueNotificationSyncStage<TBuffer, TValue, TMember>.OnNotification notification)
        {
            return InsertNotification(0, notification);
        }

        public TBuilder AppendNotification(ValueNotificationSyncStage<TBuffer, TValue, TMember>.OnNotification notification)
        {
            return InsertNotification(Adapters.Count, notification);
        }

        protected virtual TBuilder InsertNotification(int index, ValueNotificationSyncStage<TBuffer, TValue, TMember>.GetDelegate getter)
        {
            var notificationSyncStage = new ValueNotificationSyncStage<TBuffer, TValue, TMember>(getter);
            var adapter = new SyncAdapter<TBuffer, ValueNotificationSyncStage<TBuffer, TValue, TMember>, TMember>(notificationSyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ValueNotificationSyncStage<TBuffer, TValue, TMember>.GetDelegate getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(ValueNotificationSyncStage<TBuffer, TValue, TMember>.GetDelegate getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        #endregion
    }

    public abstract class ValueSyncOperationBuilder<TBuilder, TValue, TMember> : ValueSyncOperationBuilder<TBuilder, ValueSyncOperation<TValue, TMember>, ValueSyncBuffer<TValue, TMember>, TValue, TMember>
        where TBuilder : ValueSyncOperationBuilder<TBuilder, TValue, TMember>, new()
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    public class ValueSyncOperationBuilder<TValue, TMember> : ValueSyncOperationBuilder<ValueSyncOperationBuilder<TValue, TMember>, TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    public class ValueSyncOperationBuilder<TValue> : ValueSyncOperationBuilder<ValueSyncOperationBuilder<TValue>, ValueSyncOperation<TValue>, ValueSyncBuffer<TValue, IOperationMember>, TValue, IOperationMember>
        where TValue : struct
    {
    }
}