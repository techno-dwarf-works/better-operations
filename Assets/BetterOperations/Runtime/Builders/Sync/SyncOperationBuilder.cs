﻿using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Builders
{
    public abstract class SyncOperationBuilder<TBuilder, TOperation, TBuffer, TMember> : MemberedOperationBuilder<TBuilder, TOperation, TBuffer, SyncAdapter<TBuffer, TMember>, TMember>
        where TBuilder : SyncOperationBuilder<TBuilder, TOperation, TBuffer, TMember>, new()
        where TOperation : SyncOperation<TBuffer, TMember>, new()
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
        protected override SyncAdapter<TBuffer, TMember> CreateContractlessAdapter(ContractlessStage<TBuffer> stage)
        {
            var adapter = new ContractlessSyncAdapter<TBuffer, ContractlessStage<TBuffer>, TMember>(stage);
            return adapter;
        }

        #region Notifications

        protected virtual TBuilder InsertNotification(int index, NotificationSyncStage<TBuffer, TMember>.OnNotification notification)
        {
            var stage = new NotificationSyncStage<TBuffer, TMember>(notification);
            var adapter = new SyncAdapter<TBuffer, NotificationSyncStage<TBuffer, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(NotificationSyncStage<TBuffer, TMember>.OnNotification notification)
        {
            return InsertNotification(0, notification);
        }

        public TBuilder AppendNotification(NotificationSyncStage<TBuffer, TMember>.OnNotification notification)
        {
            return InsertNotification(Adapters.Count, notification);
        }

        protected virtual TBuilder InsertNotification(int index, NotificationSyncStage<TBuffer, TMember>.GetMemberDelegate getter)
        {
            var stage = new NotificationSyncStage<TBuffer, TMember>(getter);
            var adapter = new SyncAdapter<TBuffer, NotificationSyncStage<TBuffer, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(NotificationSyncStage<TBuffer, TMember>.GetMemberDelegate getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(NotificationSyncStage<TBuffer, TMember>.GetMemberDelegate getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        #endregion

        #region Fallbacks

        protected virtual TBuilder InsertFallback(int index, FallbackSyncStage<TBuffer, TMember>.OnFallback fallback)
        {
            var stage = new FallbackSyncStage<TBuffer, TMember>(fallback);
            var adapter = new SyncAdapter<TBuffer, FallbackSyncStage<TBuffer, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(FallbackSyncStage<TBuffer, TMember>.OnFallback fallback)
        {
            return InsertFallback(0, fallback);
        }

        public TBuilder AppendFallback(FallbackSyncStage<TBuffer, TMember>.OnFallback fallback)
        {
            return InsertFallback(Adapters.Count, fallback);
        }

        protected virtual TBuilder InsertFallback(int index, FallbackSyncStage<TBuffer, TMember>.GetMemberDelegate getter)
        {
            var stage = new FallbackSyncStage<TBuffer, TMember>(getter);
            var adapter = new SyncAdapter<TBuffer, FallbackSyncStage<TBuffer, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(FallbackSyncStage<TBuffer, TMember>.GetMemberDelegate getter)
        {
            return InsertFallback(0, getter);
        }

        public TBuilder AppendFallback(FallbackSyncStage<TBuffer, TMember>.GetMemberDelegate getter)
        {
            return InsertFallback(Adapters.Count, getter);
        }

        #endregion

        #region Permissings

        protected virtual TBuilder InsertPermissing(int index, PermissingSyncStage<TBuffer, TMember>.GetPermissionFlag getter)
        {
            var stage = new PermissingSyncStage<TBuffer, TMember>(getter);
            var adapter = new SyncAdapter<TBuffer, PermissingSyncStage<TBuffer, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependPermissing(PermissingSyncStage<TBuffer, TMember>.GetPermissionFlag getter)
        {
            return InsertPermissing(0, getter);
        }

        public TBuilder AppendPermissing(PermissingSyncStage<TBuffer, TMember>.GetPermissionFlag getter)
        {
            return InsertPermissing(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertPermissing(int index, PermissingSyncStage<TBuffer, TMember>.GetMemberDelegate getter)
        {
            var stage = new PermissingSyncStage<TBuffer, TMember>(getter);
            var adapter = new SyncAdapter<TBuffer, PermissingSyncStage<TBuffer, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependPermissing(PermissingSyncStage<TBuffer, TMember>.GetMemberDelegate getter)
        {
            return InsertPermissing(0, getter);
        }

        public TBuilder AppendPermissing(PermissingSyncStage<TBuffer, TMember>.GetMemberDelegate getter)
        {
            return InsertPermissing(Adapters.Count, getter);
        }

        #endregion
    }

    public abstract class SyncOperationBuilder<TBuilder, TMember> : SyncOperationBuilder<TBuilder, SyncOperation<TMember>, SyncBuffer<TMember>, TMember>
        where TBuilder : SyncOperationBuilder<TBuilder, TMember>, new()
        where TMember : IOperationMember
    {
    }

    public class SyncOperationBuilder<TMember> : SyncOperationBuilder<SyncOperationBuilder<TMember>, TMember>
        where TMember : IOperationMember
    {
    }

    public class SyncOperationBuilder : SyncOperationBuilder<SyncOperationBuilder, SyncOperation, SyncBuffer<IOperationMember>, IOperationMember>
    {
    }
}