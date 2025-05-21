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
        protected override AsyncAdapter<TBuffer, TMember> CreateContractlessAdapter(ContractlessStage<TBuffer> stage)
        {
            var adapter = new ContractlessAsyncAdapter<TBuffer, ContractlessStage<TBuffer>, TMember>(stage);
            return adapter;
        }

        #region Cancellation Catchs

        protected virtual TBuilder InsertCancellationCatch(int index)
        {
            var stage = new CatchCancellationAsyncStage<TBuffer, TMember>();
            var adapter = new AsyncAdapter<TBuffer, CatchCancellationAsyncStage<TBuffer, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependCancellationCatch()
        {
            return InsertCancellationCatch(0);
        }

        public TBuilder AppendCancellationCatch()
        {
            return InsertCancellationCatch(Adapters.Count);
        }

        #endregion

        #region Notifications

        protected virtual TBuilder InsertNotification(int index, NotificationAsyncStage<TBuffer, TMember>.OnNotificationAsync notification)
        {
            var stage = new NotificationAsyncStage<TBuffer, TMember>(notification);
            var adapter = new AsyncAdapter<TBuffer, NotificationAsyncStage<TBuffer, TMember>, TMember>(stage);
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
            var stage = new NotificationAsyncStage<TBuffer, TMember>(notification);
            var adapter = new AsyncAdapter<TBuffer, NotificationAsyncStage<TBuffer, TMember>, TMember>(stage);
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
            var stage = new NotificationAsyncStage<TBuffer, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, NotificationAsyncStage<TBuffer, TMember>, TMember>(stage);
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
            var stage = new NotificationAsyncStage<TBuffer, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, NotificationAsyncStage<TBuffer, TMember>, TMember>(stage);
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

        protected virtual TBuilder InsertFallback(int index, FallbackAsyncStage<TBuffer, TMember>.OnFallbackAsync fallback)
        {
            var stage = new FallbackAsyncStage<TBuffer, TMember>(fallback);
            var adapter = new AsyncAdapter<TBuffer, FallbackAsyncStage<TBuffer, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(FallbackAsyncStage<TBuffer, TMember>.OnFallbackAsync fallback)
        {
            return InsertFallback(0, fallback);
        }

        public TBuilder AppendFallback(FallbackAsyncStage<TBuffer, TMember>.OnFallbackAsync fallback)
        {
            return InsertFallback(Adapters.Count, fallback);
        }

        protected virtual TBuilder InsertFallback(int index, FallbackAsyncStage<TBuffer, TMember>.OnTokenableFallbackAsync fallback)
        {
            var stage = new FallbackAsyncStage<TBuffer, TMember>(fallback);
            var adapter = new AsyncAdapter<TBuffer, FallbackAsyncStage<TBuffer, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(FallbackAsyncStage<TBuffer, TMember>.OnTokenableFallbackAsync fallback)
        {
            return InsertFallback(0, fallback);
        }

        public TBuilder AppendFallback(FallbackAsyncStage<TBuffer, TMember>.OnTokenableFallbackAsync fallback)
        {
            return InsertFallback(Adapters.Count, fallback);
        }

        protected virtual TBuilder InsertFallback(int index, FallbackAsyncStage<TBuffer, TMember>.GetContinuousMemberDelegate getter)
        {
            var stage = new FallbackAsyncStage<TBuffer, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, FallbackAsyncStage<TBuffer, TMember>, TMember>(stage);
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
            var stage = new FallbackAsyncStage<TBuffer, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, FallbackAsyncStage<TBuffer, TMember>, TMember>(stage);
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
        
        #region Permissings

        protected virtual TBuilder InsertPermissing(int index, PermissingAsyncStage<TBuffer, TMember>.GetPermissingFlagAsync getter)
        {
            var stage = new PermissingAsyncStage<TBuffer, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, PermissingAsyncStage<TBuffer, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependPermissing(PermissingAsyncStage<TBuffer, TMember>.GetPermissingFlagAsync getter)
        {
            return InsertPermissing(0, getter);
        }

        public TBuilder AppendPermissing(PermissingAsyncStage<TBuffer, TMember>.GetPermissingFlagAsync getter)
        {
            return InsertPermissing(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertPermissing(int index, PermissingAsyncStage<TBuffer, TMember>.GetPermissingFlagTokenableAsync getter)
        {
            var stage = new PermissingAsyncStage<TBuffer, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, PermissingAsyncStage<TBuffer, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependPermissing(PermissingAsyncStage<TBuffer, TMember>.GetPermissingFlagTokenableAsync getter)
        {
            return InsertPermissing(0, getter);
        }

        public TBuilder AppendPermissing(PermissingAsyncStage<TBuffer, TMember>.GetPermissingFlagTokenableAsync getter)
        {
            return InsertPermissing(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertPermissing(int index, PermissingAsyncStage<TBuffer, TMember>.GetContinuousMemberDelegate getter)
        {
            var stage = new PermissingAsyncStage<TBuffer, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, PermissingAsyncStage<TBuffer, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependPermissing(PermissingAsyncStage<TBuffer, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertPermissing(0, getter);
        }

        public TBuilder AppendPermissing(PermissingAsyncStage<TBuffer, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertPermissing(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertPermissing(int index, PermissingAsyncStage<TBuffer, TMember>.GetCancellableMemberDelegate getter)
        {
            var stage = new PermissingAsyncStage<TBuffer, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, PermissingAsyncStage<TBuffer, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependPermissing(PermissingAsyncStage<TBuffer, TMember>.GetCancellableMemberDelegate getter)
        {
            return InsertPermissing(0, getter);
        }

        public TBuilder AppendPermissing(PermissingAsyncStage<TBuffer, TMember>.GetCancellableMemberDelegate getter)
        {
            return InsertPermissing(Adapters.Count, getter);
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