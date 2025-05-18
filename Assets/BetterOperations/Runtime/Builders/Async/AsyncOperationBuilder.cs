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
        protected virtual TBuilder InsertNotification(int index, NotificationAsyncStage<TBuffer, TMember>.OnNotificationAsync notification)
        {
            var joinIndex = index - 1;
            var adapter = Adapters.ElementAtOrDefault(joinIndex, true);
            if (adapter?.Stage is not NotificationAsyncStage<TBuffer, TMember> notificationAsyncStage)
            {
                notificationAsyncStage = new();
                adapter = new DerivedAsyncAdapter<TBuffer, TMember>(notificationAsyncStage);
                Adapters.Insert(index, adapter);
            }

            notificationAsyncStage.Register(notification);
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

        protected virtual TBuilder InsertNotification(int index, NotificationAsyncStage<TBuffer, TMember>.GetNotificationBy getter)
        {
            var joinIndex = index - 1;
            var adapter = Adapters.ElementAtOrDefault(joinIndex, true);
            if (adapter?.Stage is not NotificationAsyncStage<TBuffer, TMember> notificationAsyncStage)
            {
                notificationAsyncStage = new();
                adapter = new DerivedAsyncAdapter<TBuffer, TMember>(notificationAsyncStage);
                Adapters.Insert(index, adapter);
            }

            notificationAsyncStage.Register(getter);
            return (TBuilder)this;
        }

        public TBuilder PrependNotification(NotificationAsyncStage<TBuffer, TMember>.GetNotificationBy getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(NotificationAsyncStage<TBuffer, TMember>.GetNotificationBy getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertNotification(int index, NotificationAsyncStage<TBuffer, TMember>.GetTokenableNotificationBy getter)
        {
            var joinIndex = index - 1;
            var adapter = Adapters.ElementAtOrDefault(joinIndex, true);
            if (adapter?.Stage is not NotificationAsyncStage<TBuffer, TMember> notificationAsyncStage)
            {
                notificationAsyncStage = new();
                adapter = new DerivedAsyncAdapter<TBuffer, TMember>(notificationAsyncStage);
                Adapters.Insert(index, adapter);
            }

            notificationAsyncStage.Register(getter);
            return (TBuilder)this;
        }

        public TBuilder PrependNotification(NotificationAsyncStage<TBuffer, TMember>.GetTokenableNotificationBy getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(NotificationAsyncStage<TBuffer, TMember>.GetTokenableNotificationBy getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertNotification(int index, NotificationAsyncStage<TBuffer, TMember>.OnTokenableNotificationAsync getter)
        {
            var joinIndex = index - 1;
            var adapter = Adapters.ElementAtOrDefault(joinIndex, true);
            if (adapter?.Stage is not NotificationAsyncStage<TBuffer, TMember> notificationAsyncStage)
            {
                notificationAsyncStage = new();
                adapter = new DerivedAsyncAdapter<TBuffer, TMember>(notificationAsyncStage);
                Adapters.Insert(index, adapter);
            }

            notificationAsyncStage.Register(getter);
            return (TBuilder)this;
        }

        public TBuilder PrependNotification(NotificationAsyncStage<TBuffer, TMember>.OnTokenableNotificationAsync getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(NotificationAsyncStage<TBuffer, TMember>.OnTokenableNotificationAsync getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertNotification(int index, NotificationAsyncStage<TBuffer, TMember>.GetNotification getter)
        {
            var joinIndex = index - 1;
            var adapter = Adapters.ElementAtOrDefault(joinIndex, true);
            if (adapter?.Stage is not NotificationAsyncStage<TBuffer, TMember> notificationAsyncStage)
            {
                notificationAsyncStage = new();
                adapter = new DerivedAsyncAdapter<TBuffer, TMember>(notificationAsyncStage);
                Adapters.Insert(index, adapter);
            }

            notificationAsyncStage.Register(getter);
            return (TBuilder)this;
        }

        public TBuilder PrependNotification(NotificationAsyncStage<TBuffer, TMember>.GetNotification getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(NotificationAsyncStage<TBuffer, TMember>.GetNotification getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertNotification(int index, NotificationAsyncStage<TBuffer, TMember>.GetTokenableNotification getter)
        {
            var joinIndex = index - 1;
            var adapter = Adapters.ElementAtOrDefault(joinIndex, true);
            if (adapter?.Stage is not NotificationAsyncStage<TBuffer, TMember> notificationAsyncStage)
            {
                notificationAsyncStage = new();
                adapter = new DerivedAsyncAdapter<TBuffer, TMember>(notificationAsyncStage);
                Adapters.Insert(index, adapter);
            }

            notificationAsyncStage.Register(getter);
            return (TBuilder)this;
        }

        public TBuilder PrependNotification(NotificationAsyncStage<TBuffer, TMember>.GetTokenableNotification getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(NotificationAsyncStage<TBuffer, TMember>.GetTokenableNotification getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }
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