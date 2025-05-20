using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Permissions;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ContextualFallbackAsyncStage<TBuffer, TContext, TMember, TContinuousDelegate, TCancellableDelegate> : FallbackAsyncStage<TBuffer, TMember, TContinuousDelegate, TCancellableDelegate>
        where TBuffer : ContextualAsyncBuffer<TContext, TMember>
        where TMember : IOperationMember
        where TContinuousDelegate : Delegate
        where TCancellableDelegate : Delegate
    {
        protected ContextualFallbackAsyncStage(TContinuousDelegate continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        protected ContextualFallbackAsyncStage(TCancellableDelegate cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        protected ContextualFallbackAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        protected ContextualFallbackAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }
    }

    public class ContextualFallbackAsyncStage<TBuffer, TContext, TMember> : ContextualFallbackAsyncStage<TBuffer, TContext, TMember, ContextualFallbackAsyncStage<TBuffer, TContext, TMember>.OnFallbackAsync, ContextualFallbackAsyncStage<TBuffer, TContext, TMember>.OnTokenableFallbackAsync>
        where TBuffer : ContextualAsyncBuffer<TContext, TMember>
        where TMember : IOperationMember
    {
        public delegate Task OnFallbackAsync(PermissionFlag operationPermission, TContext context);

        public delegate Task OnTokenableFallbackAsync(PermissionFlag operationPermission, TContext context, CancellationToken cancellationToken);

        public ContextualFallbackAsyncStage(OnFallbackAsync continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        public ContextualFallbackAsyncStage(OnTokenableFallbackAsync cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        public ContextualFallbackAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        public ContextualFallbackAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }

        protected override Task ExecuteSubDelegateAsync(TBuffer buffer, OnFallbackAsync subDelegate)
        {
            return subDelegate.Invoke(buffer.PermissionFlag, buffer.Context);
        }

        protected override Task ExecuteSubDelegateAsync(TBuffer buffer, OnTokenableFallbackAsync subDelegate)
        {
            return subDelegate.Invoke(buffer.PermissionFlag, buffer.Context, buffer.CancellationToken);
        }
    }
}