using Better.Commons.Runtime.Extensions;
using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Builders
{
    public abstract class AsyncOperationBuilder<TBuilder, TOperation, TBuffer, TMember> : MemberedOperationBuilder<TBuilder, TOperation, TBuffer, AsyncAdapter<TBuffer, TMember>, TMember>
        where TBuilder : AsyncOperationBuilder<TBuilder, TOperation, TBuffer, TMember>, new()
        where TOperation : AsyncOperation<TBuffer, TMember>, new()
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
    {
        protected virtual TStage StageAt<TStage>(int index)
            where TStage : AsyncStage<TBuffer, TMember>, new()
        {
            var joinIndex = index - 1;
            var adapter = Adapters.ElementAtOrDefault(joinIndex, true);
            if (adapter?.Stage is not TStage stage)
            {
                stage = new();
                adapter = new AsyncAdapter<TBuffer, TStage, TMember>(stage);
                Adapters.Insert(index, adapter);
            }

            return stage;
        }

        #region Notifications

        protected virtual TBuilder InsertNotification(int index, NotificationAsyncStage<TBuffer, TMember>.OnNotificationAsync notification)
        {
            var notificationAsyncStage = new NotificationAsyncStage<TBuffer, TMember>(notification);
            var adapter = new AsyncAdapter<TBuffer, NotificationAsyncStage<TBuffer, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(NotificationAsyncStage<TBuffer, TMember>.OnNotificationAsync notification)
        {
            return InsertNotification(0, notification);
        }

        public TBuilder AppendNotification(NotificationAsyncStage<TBuffer, TMember>.OnNotificationAsync notification)
        {
            return InsertNotification(Adapters.Count, notification);
        }

        protected virtual TBuilder InsertNotification(int index, NotificationAsyncStage<TBuffer, TMember>.OnTokenableNotificationAsync notification)
        {
            var notificationAsyncStage = new NotificationAsyncStage<TBuffer, TMember>(notification);
            var adapter = new AsyncAdapter<TBuffer, NotificationAsyncStage<TBuffer, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(NotificationAsyncStage<TBuffer, TMember>.OnTokenableNotificationAsync notification)
        {
            return InsertNotification(0, notification);
        }

        public TBuilder AppendNotification(NotificationAsyncStage<TBuffer, TMember>.OnTokenableNotificationAsync notification)
        {
            return InsertNotification(Adapters.Count, notification);
        }

        protected virtual TBuilder InsertNotification(int index, NotificationAsyncStage<TBuffer, TMember>.GetContinuousMemberDelegate getter)
        {
            var notificationAsyncStage = new NotificationAsyncStage<TBuffer, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, NotificationAsyncStage<TBuffer, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(NotificationAsyncStage<TBuffer, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(NotificationAsyncStage<TBuffer, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertNotification(int index, NotificationAsyncStage<TBuffer, TMember>.GetCancellableMemberDelegate getter)
        {
            var notificationAsyncStage = new NotificationAsyncStage<TBuffer, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, NotificationAsyncStage<TBuffer, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(NotificationAsyncStage<TBuffer, TMember>.GetCancellableMemberDelegate getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(NotificationAsyncStage<TBuffer, TMember>.GetCancellableMemberDelegate getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        #endregion
    }

    public abstract class AsyncOperationBuilder<TBuilder, TMember> : AsyncOperationBuilder<TBuilder, AsyncOperation<TMember>, AsyncBuffer<TMember>, TMember>
        where TBuilder : AsyncOperationBuilder<TBuilder, TMember>, new()
        where TMember : IOperationMember
    {
    }

    public class AsyncOperationBuilder<TMember> : AsyncOperationBuilder<AsyncOperationBuilder<TMember>, TMember>
        where TMember : IOperationMember
    {
    }

    public class AsyncOperationBuilder : AsyncOperationBuilder<AsyncOperationBuilder, AsyncOperation, AsyncBuffer<IOperationMember>, IOperationMember>
    {
    }
}