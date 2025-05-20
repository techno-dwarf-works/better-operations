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

        protected virtual TBuilder InsertNotification(int index, ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnNotificationAsync notification)
        {
            var notificationAsyncStage = new ValueNotificationAsyncStage<TBuffer, TValue, TMember>(notification);
            var adapter = new AsyncAdapter<TBuffer, ValueNotificationAsyncStage<TBuffer, TValue, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

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

        protected virtual TBuilder InsertNotification(int index, ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnTokenableNotificationAsync notification)
        {
            var notificationAsyncStage = new ValueNotificationAsyncStage<TBuffer, TValue, TMember>(notification);
            var adapter = new AsyncAdapter<TBuffer, ValueNotificationAsyncStage<TBuffer, TValue, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnTokenableNotificationAsync notification)
        {
            return InsertNotification(0, notification);
        }

        public TBuilder AppendNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnTokenableNotificationAsync notification)
        {
            return InsertNotification(Adapters.Count, notification);
        }

        protected virtual TBuilder InsertNotification(int index, ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetContinuousMemberDelegate getter)
        {
            var notificationAsyncStage = new ValueNotificationAsyncStage<TBuffer, TValue, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, ValueNotificationAsyncStage<TBuffer, TValue, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertNotification(int index, ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetCancellableMemberDelegate getter)
        {
            var notificationAsyncStage = new ValueNotificationAsyncStage<TBuffer, TValue, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, ValueNotificationAsyncStage<TBuffer, TValue, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetCancellableMemberDelegate getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetCancellableMemberDelegate getter)
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