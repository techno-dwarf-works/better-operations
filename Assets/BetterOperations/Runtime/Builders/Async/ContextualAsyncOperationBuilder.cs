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

        protected virtual TBuilder InsertNotification(int index, ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.OnNotificationAsync notification)
        {
            var notificationAsyncStage = new ContextualNotificationAsyncStage<TBuffer, TContext, TMember>(notification);
            var adapter = new AsyncAdapter<TBuffer, ContextualNotificationAsyncStage<TBuffer, TContext, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

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

        protected virtual TBuilder InsertNotification(int index, ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.OnTokenableNotificationAsync notification)
        {
            var notificationAsyncStage = new ContextualNotificationAsyncStage<TBuffer, TContext, TMember>(notification);
            var adapter = new AsyncAdapter<TBuffer, ContextualNotificationAsyncStage<TBuffer, TContext, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.OnTokenableNotificationAsync notification)
        {
            return InsertNotification(0, notification);
        }

        public TBuilder AppendNotification(ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.OnTokenableNotificationAsync notification)
        {
            return InsertNotification(Adapters.Count, notification);
        }

        protected virtual TBuilder InsertNotification(int index, ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.GetContinuousMemberDelegate getter)
        {
            var notificationAsyncStage = new ContextualNotificationAsyncStage<TBuffer, TContext, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, ContextualNotificationAsyncStage<TBuffer, TContext, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertNotification(int index, ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.GetCancellableMemberDelegate getter)
        {
            var notificationAsyncStage = new ContextualNotificationAsyncStage<TBuffer, TContext, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, ContextualNotificationAsyncStage<TBuffer, TContext, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.GetCancellableMemberDelegate getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.GetCancellableMemberDelegate getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        #endregion

        #region Fallbacks

        protected virtual TBuilder InsertFallback(int index, ContextualFallbackAsyncStage<TBuffer, TContext, TMember>.OnFallbackAsync notification)
        {
            var notificationAsyncStage = new ContextualFallbackAsyncStage<TBuffer, TContext, TMember>(notification);
            var adapter = new AsyncAdapter<TBuffer, ContextualFallbackAsyncStage<TBuffer, TContext, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(ContextualFallbackAsyncStage<TBuffer, TContext, TMember>.OnFallbackAsync notification)
        {
            return InsertFallback(0, notification);
        }

        public TBuilder AppendFallback(ContextualFallbackAsyncStage<TBuffer, TContext, TMember>.OnFallbackAsync notification)
        {
            return InsertFallback(Adapters.Count, notification);
        }

        protected virtual TBuilder InsertFallback(int index, ContextualFallbackAsyncStage<TBuffer, TContext, TMember>.OnTokenableFallbackAsync notification)
        {
            var notificationAsyncStage = new ContextualFallbackAsyncStage<TBuffer, TContext, TMember>(notification);
            var adapter = new AsyncAdapter<TBuffer, ContextualFallbackAsyncStage<TBuffer, TContext, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(ContextualFallbackAsyncStage<TBuffer, TContext, TMember>.OnTokenableFallbackAsync notification)
        {
            return InsertFallback(0, notification);
        }

        public TBuilder AppendFallback(ContextualFallbackAsyncStage<TBuffer, TContext, TMember>.OnTokenableFallbackAsync notification)
        {
            return InsertFallback(Adapters.Count, notification);
        }

        protected virtual TBuilder InsertFallback(int index, ContextualFallbackAsyncStage<TBuffer, TContext, TMember>.GetContinuousMemberDelegate getter)
        {
            var notificationAsyncStage = new ContextualFallbackAsyncStage<TBuffer, TContext, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, ContextualFallbackAsyncStage<TBuffer, TContext, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(ContextualFallbackAsyncStage<TBuffer, TContext, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertFallback(0, getter);
        }

        public TBuilder AppendFallback(ContextualFallbackAsyncStage<TBuffer, TContext, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertFallback(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertFallback(int index, ContextualFallbackAsyncStage<TBuffer, TContext, TMember>.GetCancellableMemberDelegate getter)
        {
            var notificationAsyncStage = new ContextualFallbackAsyncStage<TBuffer, TContext, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, ContextualFallbackAsyncStage<TBuffer, TContext, TMember>, TMember>(notificationAsyncStage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(ContextualFallbackAsyncStage<TBuffer, TContext, TMember>.GetCancellableMemberDelegate getter)
        {
            return InsertFallback(0, getter);
        }

        public TBuilder AppendFallback(ContextualFallbackAsyncStage<TBuffer, TContext, TMember>.GetCancellableMemberDelegate getter)
        {
            return InsertFallback(Adapters.Count, getter);
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