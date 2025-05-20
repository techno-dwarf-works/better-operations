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
        
        #region Fallbacks

        protected virtual TBuilder InsertFallback(int index, FallbackAsyncStage<TBuffer, TMember>.OnFallbackAsync notification)
        {
            var notificationAsyncStage = new FallbackAsyncStage<TBuffer, TMember>(notification);
            var adapter = new AsyncAdapter<TBuffer, FallbackAsyncStage<TBuffer, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(FallbackAsyncStage<TBuffer, TMember>.OnFallbackAsync notification)
        {
            return InsertFallback(0, notification);
        }

        public TBuilder AppendFallback(FallbackAsyncStage<TBuffer, TMember>.OnFallbackAsync notification)
        {
            return InsertFallback(Adapters.Count, notification);
        }

        protected virtual TBuilder InsertFallback(int index, FallbackAsyncStage<TBuffer, TMember>.OnTokenableFallbackAsync notification)
        {
            var notificationAsyncStage = new FallbackAsyncStage<TBuffer, TMember>(notification);
            var adapter = new AsyncAdapter<TBuffer, FallbackAsyncStage<TBuffer, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(FallbackAsyncStage<TBuffer, TMember>.OnTokenableFallbackAsync notification)
        {
            return InsertFallback(0, notification);
        }

        public TBuilder AppendFallback(FallbackAsyncStage<TBuffer, TMember>.OnTokenableFallbackAsync notification)
        {
            return InsertFallback(Adapters.Count, notification);
        }

        protected virtual TBuilder InsertFallback(int index, FallbackAsyncStage<TBuffer, TMember>.GetContinuousMemberDelegate getter)
        {
            var notificationAsyncStage = new FallbackAsyncStage<TBuffer, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, FallbackAsyncStage<TBuffer, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(FallbackAsyncStage<TBuffer, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertFallback(0, getter);
        }

        public TBuilder AppendFallback(FallbackAsyncStage<TBuffer, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertFallback(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertFallback(int index, FallbackAsyncStage<TBuffer, TMember>.GetCancellableMemberDelegate getter)
        {
            var notificationAsyncStage = new FallbackAsyncStage<TBuffer, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, FallbackAsyncStage<TBuffer, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(FallbackAsyncStage<TBuffer, TMember>.GetCancellableMemberDelegate getter)
        {
            return InsertFallback(0, getter);
        }

        public TBuilder AppendFallback(FallbackAsyncStage<TBuffer, TMember>.GetCancellableMemberDelegate getter)
        {
            return InsertFallback(Adapters.Count, getter);
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