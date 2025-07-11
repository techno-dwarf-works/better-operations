﻿using System;
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

        protected ValueNotificationSyncStage(GetMemberDelegate delegateGetter) : base(delegateGetter)
        {
        }
    }

    public class ValueNotificationSyncStage<TBuffer, TValue, TMember> : ValueNotificationSyncStage<TBuffer, TValue, TMember, ValueNotificationSyncStage<TBuffer, TValue, TMember>.OnNotification>
        where TBuffer : ValueSyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        public delegate void OnNotification(TValue source, TValue modified);

        public ValueNotificationSyncStage(OnNotification subDelegate) : base(subDelegate)
        {
        }

        public ValueNotificationSyncStage(GetMemberDelegate delegateGetter) : base(delegateGetter)
        {
        }

        protected override void Execute(TBuffer buffer, OnNotification subDelegate)
        {
            subDelegate.Invoke(buffer.SourceValue, buffer.ModifiedValue);
        }
    }
}