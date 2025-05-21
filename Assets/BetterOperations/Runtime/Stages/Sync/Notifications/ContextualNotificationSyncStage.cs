using System;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ContextualNotificationSyncStage<TBuffer, TContext, TMember, TDelegate> : NotificationSyncStage<TBuffer, TMember, TDelegate>
        where TBuffer : ContextualSyncBuffer<TContext, TMember>
        where TMember : IOperationMember
        where TDelegate : Delegate
    {
        protected ContextualNotificationSyncStage(TDelegate subDelegate) : base(subDelegate)
        {
        }

        protected ContextualNotificationSyncStage(GetMemberDelegate delegateGetter) : base(delegateGetter)
        {
        }
    }

    public class ContextualNotificationSyncStage<TBuffer, TContext, TMember> : ContextualNotificationSyncStage<TBuffer, TContext, TMember, ContextualNotificationSyncStage<TBuffer, TContext, TMember>.OnNotification>
        where TBuffer : ContextualSyncBuffer<TContext, TMember>
        where TMember : IOperationMember
    {
        public delegate void OnNotification(TContext context);

        public ContextualNotificationSyncStage(OnNotification subDelegate) : base(subDelegate)
        {
        }

        public ContextualNotificationSyncStage(GetMemberDelegate delegateGetter) : base(delegateGetter)
        {
        }

        protected override void Execute(TBuffer buffer, OnNotification subDelegate)
        {
            subDelegate.Invoke(buffer.Context);
        }
    }
}