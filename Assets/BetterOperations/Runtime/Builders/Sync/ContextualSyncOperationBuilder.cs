using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Builders
{
    public abstract class ContextualSyncOperationBuilder<TBuilder, TOperation, TBuffer, TContext, TMember> : SyncOperationBuilder<TBuilder, TOperation, TBuffer, TMember>
        where TBuilder : ContextualSyncOperationBuilder<TBuilder, TOperation, TBuffer, TContext, TMember>, new()
        where TOperation : ContextualSyncOperation<TBuffer, TContext, TMember>, new()
        where TBuffer : ContextualSyncBuffer<TContext, TMember>
        where TMember : IOperationMember
    {
        #region Notifications

        protected virtual TBuilder InsertNotification(int index, ContextualNotificationSyncStage<TBuffer, TContext, TMember>.OnNotification notification)
        {
            var stage = new ContextualNotificationSyncStage<TBuffer, TContext, TMember>(notification);
            var adapter = new SyncAdapter<TBuffer, ContextualNotificationSyncStage<TBuffer, TContext, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ContextualNotificationSyncStage<TBuffer, TContext, TMember>.OnNotification notification)
        {
            return InsertNotification(0, notification);
        }

        public TBuilder AppendNotification(ContextualNotificationSyncStage<TBuffer, TContext, TMember>.OnNotification notification)
        {
            return InsertNotification(Adapters.Count, notification);
        }

        protected virtual TBuilder InsertNotification(int index, ContextualNotificationSyncStage<TBuffer, TContext, TMember>.GetDelegate getter)
        {
            var stage = new ContextualNotificationSyncStage<TBuffer, TContext, TMember>(getter);
            var adapter = new SyncAdapter<TBuffer, ContextualNotificationSyncStage<TBuffer, TContext, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ContextualNotificationSyncStage<TBuffer, TContext, TMember>.GetDelegate getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(ContextualNotificationSyncStage<TBuffer, TContext, TMember>.GetDelegate getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        #endregion

        #region Fallbacks

        protected virtual TBuilder InsertFallback(int index, ContextualFallbackSyncStage<TBuffer, TContext, TMember>.OnFallback fallback)
        {
            var stage = new ContextualFallbackSyncStage<TBuffer, TContext, TMember>(fallback);
            var adapter = new SyncAdapter<TBuffer, ContextualFallbackSyncStage<TBuffer, TContext, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(ContextualFallbackSyncStage<TBuffer, TContext, TMember>.OnFallback fallback)
        {
            return InsertFallback(0, fallback);
        }

        public TBuilder AppendFallback(ContextualFallbackSyncStage<TBuffer, TContext, TMember>.OnFallback fallback)
        {
            return InsertFallback(Adapters.Count, fallback);
        }

        protected virtual TBuilder InsertFallback(int index, ContextualFallbackSyncStage<TBuffer, TContext, TMember>.GetDelegate getter)
        {
            var stage = new ContextualFallbackSyncStage<TBuffer, TContext, TMember>(getter);
            var adapter = new SyncAdapter<TBuffer, ContextualFallbackSyncStage<TBuffer, TContext, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(ContextualFallbackSyncStage<TBuffer, TContext, TMember>.GetDelegate getter)
        {
            return InsertFallback(0, getter);
        }

        public TBuilder AppendFallback(ContextualFallbackSyncStage<TBuffer, TContext, TMember>.GetDelegate getter)
        {
            return InsertFallback(Adapters.Count, getter);
        }

        #endregion

        #region Permissings

        protected virtual TBuilder InsertPermissing(int index, ContextualPermissingSyncStage<TBuffer, TContext, TMember>.GetPermissionFlag getter)
        {
            var stage = new ContextualPermissingSyncStage<TBuffer, TContext, TMember>(getter);
            var adapter = new SyncAdapter<TBuffer, ContextualPermissingSyncStage<TBuffer, TContext, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependPermissing(ContextualPermissingSyncStage<TBuffer, TContext, TMember>.GetPermissionFlag getter)
        {
            return InsertPermissing(0, getter);
        }

        public TBuilder AppendPermissing(ContextualPermissingSyncStage<TBuffer, TContext, TMember>.GetPermissionFlag getter)
        {
            return InsertPermissing(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertPermissing(int index, ContextualPermissingSyncStage<TBuffer, TContext, TMember>.GetDelegate getter)
        {
            var stage = new ContextualPermissingSyncStage<TBuffer, TContext, TMember>(getter);
            var adapter = new SyncAdapter<TBuffer, ContextualPermissingSyncStage<TBuffer, TContext, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependPermissing(ContextualPermissingSyncStage<TBuffer, TContext, TMember>.GetDelegate getter)
        {
            return InsertPermissing(0, getter);
        }

        public TBuilder AppendPermissing(ContextualPermissingSyncStage<TBuffer, TContext, TMember>.GetDelegate getter)
        {
            return InsertPermissing(Adapters.Count, getter);
        }

        #endregion

        #region Modifies

        protected virtual TBuilder InsertModify(int index, ContextualModifySyncStage<TBuffer, TContext, TMember>.OnModify modify)
        {
            var stage = new ContextualModifySyncStage<TBuffer, TContext, TMember>(modify);
            var adapter = new SyncAdapter<TBuffer, ContextualModifySyncStage<TBuffer, TContext, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependModify(ContextualModifySyncStage<TBuffer, TContext, TMember>.OnModify modify)
        {
            return InsertModify(0, modify);
        }

        public TBuilder AppendModify(ContextualModifySyncStage<TBuffer, TContext, TMember>.OnModify modify)
        {
            return InsertModify(Adapters.Count, modify);
        }

        protected virtual TBuilder InsertModify(int index, ContextualModifySyncStage<TBuffer, TContext, TMember>.GetDelegate getter)
        {
            var stage = new ContextualModifySyncStage<TBuffer, TContext, TMember>(getter);
            var adapter = new SyncAdapter<TBuffer, ContextualModifySyncStage<TBuffer, TContext, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependModify(ContextualModifySyncStage<TBuffer, TContext, TMember>.GetDelegate getter)
        {
            return InsertModify(0, getter);
        }

        public TBuilder AppendModify(ContextualModifySyncStage<TBuffer, TContext, TMember>.GetDelegate getter)
        {
            return InsertModify(Adapters.Count, getter);
        }

        #endregion
    }

    public abstract class ContextualSyncOperationBuilder<TBuilder, TContext, TMember> : ContextualSyncOperationBuilder<TBuilder, ContextualSyncOperation<TContext, TMember>, ContextualSyncBuffer<TContext, TMember>, TContext, TMember>
        where TBuilder : ContextualSyncOperationBuilder<TBuilder, TContext, TMember>, new()
        where TMember : IOperationMember
    {
    }

    public class ContextualSyncOperationBuilder<TContext, TMember> : ContextualSyncOperationBuilder<ContextualSyncOperationBuilder<TContext, TMember>, TContext, TMember>
        where TMember : IOperationMember
    {
    }

    public class ContextualSyncOperationBuilder<TContext> : ContextualSyncOperationBuilder<ContextualSyncOperationBuilder<TContext>, ContextualSyncOperation<TContext>, ContextualSyncBuffer<TContext, IOperationMember>, TContext, IOperationMember>
    {
    }
}