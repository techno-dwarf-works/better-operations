using System;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public abstract class NotificationSyncStage<TBuffer, TMember, TDelegate> : DelegateSyncStage<TBuffer, TMember, TDelegate>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
        where TDelegate : Delegate
    {
        protected NotificationSyncStage(TDelegate subDelegate) : base(subDelegate)
        {
        }

        protected NotificationSyncStage(GetDelegate delegateGetter) : base(delegateGetter)
        {
        }
    }

    public class NotificationSyncStage<TBuffer, TMember> : NotificationSyncStage<TBuffer, TMember, NotificationSyncStage<TBuffer, TMember>.OnNotification>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public delegate void OnNotification();

        public NotificationSyncStage(OnNotification subDelegate) : base(subDelegate)
        {
        }

        public NotificationSyncStage(GetDelegate delegateGetter) : base(delegateGetter)
        {
        }

        protected override void Execute(TBuffer buffer, OnNotification subDelegate)
        {
            subDelegate.Invoke();
        }
    }
}