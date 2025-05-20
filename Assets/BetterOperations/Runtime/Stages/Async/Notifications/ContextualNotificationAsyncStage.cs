using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ContextualNotificationAsyncStage<TBuffer, TContext, TMember, TContinuousDelegate, TCancellableDelegate> : NotificationAsyncStage<TBuffer, TMember, TContinuousDelegate, TCancellableDelegate>
        where TBuffer : ContextualAsyncBuffer<TContext, TMember>
        where TMember : IOperationMember
        where TContinuousDelegate : Delegate
        where TCancellableDelegate : Delegate
    {
        protected ContextualNotificationAsyncStage(TContinuousDelegate continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        protected ContextualNotificationAsyncStage(TCancellableDelegate cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        protected ContextualNotificationAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        protected ContextualNotificationAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }
    }

    public class ContextualNotificationAsyncStage<TBuffer, TContext, TMember> : ContextualNotificationAsyncStage<TBuffer, TContext, TMember, ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.OnNotification, ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.OnTokenableNotification>
        where TBuffer : ContextualAsyncBuffer<TContext, TMember>
        where TMember : IOperationMember
    {
        public delegate Task OnNotification(TContext context);

        public delegate Task OnTokenableNotification(TContext context, CancellationToken cancellationToken);

        public ContextualNotificationAsyncStage(OnNotification continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        public ContextualNotificationAsyncStage(OnTokenableNotification cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        public ContextualNotificationAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        public ContextualNotificationAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }

        protected override Task ExecuteSubDelegateAsync(TBuffer buffer, OnNotification subDelegate)
        {
            return subDelegate.Invoke(buffer.Context);
        }

        protected override Task ExecuteSubDelegateAsync(TBuffer buffer, OnTokenableNotification subDelegate)
        {
            return subDelegate.Invoke(buffer.Context, buffer.CancellationToken);
        }
    }
}