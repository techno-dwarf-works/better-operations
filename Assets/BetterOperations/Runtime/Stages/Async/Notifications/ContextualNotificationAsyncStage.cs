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

    public class ContextualNotificationAsyncStage<TBuffer, TContext, TMember> : ContextualNotificationAsyncStage<TBuffer, TContext, TMember, ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.OnNotificationAsync, ContextualNotificationAsyncStage<TBuffer, TContext, TMember>.OnTokenableNotificationAsync>
        where TBuffer : ContextualAsyncBuffer<TContext, TMember>
        where TMember : IOperationMember
    {
        public delegate Task OnNotificationAsync(TContext context);

        public delegate Task OnTokenableNotificationAsync(TContext context, CancellationToken cancellationToken);

        public ContextualNotificationAsyncStage(OnNotificationAsync continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        public ContextualNotificationAsyncStage(OnTokenableNotificationAsync cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        public ContextualNotificationAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        public ContextualNotificationAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }

        protected override Task ExecuteSubDelegateAsync(TBuffer buffer, OnNotificationAsync subDelegate)
        {
            return subDelegate.Invoke(buffer.Context);
        }

        protected override Task ExecuteSubDelegateAsync(TBuffer buffer, OnTokenableNotificationAsync subDelegate)
        {
            return subDelegate.Invoke(buffer.Context, buffer.CancellationToken);
        }
    }
}