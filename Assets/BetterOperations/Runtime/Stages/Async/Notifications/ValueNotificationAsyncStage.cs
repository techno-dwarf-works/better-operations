using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ValueNotificationAsyncStage<TBuffer, TValue, TMember, TContinuousDelegate, TCancellableDelegate> : NotificationAsyncStage<TBuffer, TMember, TContinuousDelegate, TCancellableDelegate>
        where TBuffer : ValueAsyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
        where TContinuousDelegate : Delegate
        where TCancellableDelegate : Delegate
    {
        protected ValueNotificationAsyncStage(TContinuousDelegate continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        protected ValueNotificationAsyncStage(TCancellableDelegate cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        protected ValueNotificationAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        protected ValueNotificationAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }
    }

    public class ValueNotificationAsyncStage<TBuffer, TValue, TMember> : ValueNotificationAsyncStage<TBuffer, TValue, TMember, ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnNotification, ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnTokenableNotification>
        where TBuffer : ValueAsyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        public delegate Task OnNotification(TValue sourceValue, TValue modifiedValue);

        public delegate Task OnTokenableNotification(TValue sourceValue, TValue modifiedValue, CancellationToken cancellationToken);

        public ValueNotificationAsyncStage(OnNotification continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        public ValueNotificationAsyncStage(OnTokenableNotification cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        public ValueNotificationAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        public ValueNotificationAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }

        protected override Task ExecuteSubDelegateAsync(TBuffer buffer, OnNotification subDelegate)
        {
            return subDelegate.Invoke(buffer.SourceValue, buffer.ModifiedValue);
        }

        protected override Task ExecuteSubDelegateAsync(TBuffer buffer, OnTokenableNotification subDelegate)
        {
            return subDelegate.Invoke(buffer.SourceValue, buffer.ModifiedValue, buffer.CancellationToken);
        }
    }
}