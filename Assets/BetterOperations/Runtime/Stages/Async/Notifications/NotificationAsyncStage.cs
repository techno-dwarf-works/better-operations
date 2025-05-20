using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public abstract class NotificationAsyncStage<TBuffer, TMember, TContinuousDelegate, TCancellableDelegate> : DelegateAsyncStage<TBuffer, TMember, TContinuousDelegate, TCancellableDelegate>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
        where TContinuousDelegate : Delegate
        where TCancellableDelegate : Delegate
    {
        protected NotificationAsyncStage(TContinuousDelegate continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        protected NotificationAsyncStage(TCancellableDelegate cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        protected NotificationAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        protected NotificationAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }
    }

    public class NotificationAsyncStage<TBuffer, TMember> : NotificationAsyncStage<TBuffer, TMember, NotificationAsyncStage<TBuffer, TMember>.OnNotificationAsync, NotificationAsyncStage<TBuffer, TMember>.OnTokenableNotificationAsync>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public delegate Task OnNotificationAsync();

        public delegate Task OnTokenableNotificationAsync(CancellationToken cancellationToken);

        public NotificationAsyncStage(OnNotificationAsync continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        public NotificationAsyncStage(OnTokenableNotificationAsync cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        public NotificationAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        public NotificationAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }

        protected override Task ExecuteSubDelegateAsync(TBuffer buffer, OnNotificationAsync subDelegate)
        {
            return subDelegate.Invoke();
        }

        protected override Task ExecuteSubDelegateAsync(TBuffer buffer, OnTokenableNotificationAsync subDelegate)
        {
            return subDelegate.Invoke(buffer.CancellationToken);
        }
    }
}