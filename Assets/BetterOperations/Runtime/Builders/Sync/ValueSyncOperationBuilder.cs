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
            var stage = new ValueNotificationSyncStage<TBuffer, TValue, TMember>(notification);
            var adapter = new SyncAdapter<TBuffer, ValueNotificationSyncStage<TBuffer, TValue, TMember>, TMember>(stage);
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
            var stage = new ValueNotificationSyncStage<TBuffer, TValue, TMember>(getter);
            var adapter = new SyncAdapter<TBuffer, ValueNotificationSyncStage<TBuffer, TValue, TMember>, TMember>(stage);
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

        #region Fallbacks

        protected virtual TBuilder InsertFallback(int index, ValueFallbackSyncStage<TBuffer, TValue, TMember>.OnFallback fallback)
        {
            var stage = new ValueFallbackSyncStage<TBuffer, TValue, TMember>(fallback);
            var adapter = new SyncAdapter<TBuffer, ValueFallbackSyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(ValueFallbackSyncStage<TBuffer, TValue, TMember>.OnFallback fallback)
        {
            return InsertFallback(0, fallback);
        }

        public TBuilder AppendFallback(ValueFallbackSyncStage<TBuffer, TValue, TMember>.OnFallback fallback)
        {
            return InsertFallback(Adapters.Count, fallback);
        }

        protected virtual TBuilder InsertFallback(int index, ValueFallbackSyncStage<TBuffer, TValue, TMember>.GetDelegate getter)
        {
            var stage = new ValueFallbackSyncStage<TBuffer, TValue, TMember>(getter);
            var adapter = new SyncAdapter<TBuffer, ValueFallbackSyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(ValueFallbackSyncStage<TBuffer, TValue, TMember>.GetDelegate getter)
        {
            return InsertFallback(0, getter);
        }

        public TBuilder AppendFallback(ValueFallbackSyncStage<TBuffer, TValue, TMember>.GetDelegate getter)
        {
            return InsertFallback(Adapters.Count, getter);
        }

        #endregion

        #region Permissings

        protected virtual TBuilder InsertPermissing(int index, ValuePermissingSyncStage<TBuffer, TValue, TMember>.GetPermissionFlag getter)
        {
            var stage = new ValuePermissingSyncStage<TBuffer, TValue, TMember>(getter);
            var adapter = new SyncAdapter<TBuffer, ValuePermissingSyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependPermissing(ValuePermissingSyncStage<TBuffer, TValue, TMember>.GetPermissionFlag getter)
        {
            return InsertPermissing(0, getter);
        }

        public TBuilder AppendPermissing(ValuePermissingSyncStage<TBuffer, TValue, TMember>.GetPermissionFlag getter)
        {
            return InsertPermissing(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertPermissing(int index, ValuePermissingSyncStage<TBuffer, TValue, TMember>.GetDelegate getter)
        {
            var stage = new ValuePermissingSyncStage<TBuffer, TValue, TMember>(getter);
            var adapter = new SyncAdapter<TBuffer, ValuePermissingSyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependPermissing(ValuePermissingSyncStage<TBuffer, TValue, TMember>.GetDelegate getter)
        {
            return InsertPermissing(0, getter);
        }

        public TBuilder AppendPermissing(ValuePermissingSyncStage<TBuffer, TValue, TMember>.GetDelegate getter)
        {
            return InsertPermissing(Adapters.Count, getter);
        }

        #endregion

        #region Modifies

        protected virtual TBuilder InsertModify(int index, ValueModifySyncStage<TBuffer, TValue, TMember>.OnModify modify)
        {
            var stage = new ValueModifySyncStage<TBuffer, TValue, TMember>(modify);
            var adapter = new SyncAdapter<TBuffer, ValueModifySyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependModify(ValueModifySyncStage<TBuffer, TValue, TMember>.OnModify modify)
        {
            return InsertModify(0, modify);
        }

        public TBuilder AppendModify(ValueModifySyncStage<TBuffer, TValue, TMember>.OnModify modify)
        {
            return InsertModify(Adapters.Count, modify);
        }

        protected virtual TBuilder InsertModify(int index, ValueModifySyncStage<TBuffer, TValue, TMember>.GetDelegate getter)
        {
            var stage = new ValueModifySyncStage<TBuffer, TValue, TMember>(getter);
            var adapter = new SyncAdapter<TBuffer, ValueModifySyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependModify(ValueModifySyncStage<TBuffer, TValue, TMember>.GetDelegate getter)
        {
            return InsertModify(0, getter);
        }

        public TBuilder AppendModify(ValueModifySyncStage<TBuffer, TValue, TMember>.GetDelegate getter)
        {
            return InsertModify(Adapters.Count, getter);
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