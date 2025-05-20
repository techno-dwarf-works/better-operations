using System;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ValueNotificationSyncStage<TBuffer, TValue, TMember, TDelegate> : NotificationSyncStage<TBuffer, TMember, TDelegate>
        where TBuffer : ValueSyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
        where TDelegate : Delegate
    {
        protected ValueNotificationSyncStage(TDelegate subDelegate) : base(subDelegate)
        {
        }

        protected ValueNotificationSyncStage(GetDelegate delegateGetter) : base(delegateGetter)
        {
        }
    }

    public class ValueNotificationSyncStage<TBuffer, TValue, TMember> : ValueNotificationSyncStage<TBuffer, TValue, TMember, ValueNotificationSyncStage<TBuffer, TValue, TMember>.OnNotification>
        where TBuffer : ValueSyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        public delegate void OnNotification(TValue sourceValue, TValue modifiedValue);

        public ValueNotificationSyncStage(OnNotification subDelegate) : base(subDelegate)
        {
        }

        public ValueNotificationSyncStage(GetDelegate delegateGetter) : base(delegateGetter)
        {
        }

        protected override void Execute(TBuffer buffer, OnNotification subDelegate)
        {
            subDelegate.Invoke(buffer.SourceValue, buffer.ModifiedValue);
        }
    }
}