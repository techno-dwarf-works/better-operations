using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Permissions;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ContextualPermissingAsyncStage<TBuffer, TContext, TMember, TContinuousDelegate, TCancellableDelegate> : PermissingAsyncStage<TBuffer, TMember, TContinuousDelegate, TCancellableDelegate>
        where TBuffer : ContextualAsyncBuffer<TContext, TMember>
        where TMember : IOperationMember
        where TContinuousDelegate : Delegate
        where TCancellableDelegate : Delegate
    {
        protected ContextualPermissingAsyncStage(TContinuousDelegate continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        protected ContextualPermissingAsyncStage(TCancellableDelegate cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        protected ContextualPermissingAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        protected ContextualPermissingAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }
    }

    public class ContextualPermissingAsyncStage<TBuffer, TContext, TMember> : ContextualPermissingAsyncStage<TBuffer, TContext, TMember, ContextualPermissingAsyncStage<TBuffer, TContext, TMember>.GetPermissingFlagAsync, ContextualPermissingAsyncStage<TBuffer, TContext, TMember>.GetTokenablePermissingFlagAsync>
        where TBuffer : ContextualAsyncBuffer<TContext, TMember>
        where TMember : IOperationMember
    {
        public delegate Task<PermissionFlag> GetPermissingFlagAsync(TContext context);

        public delegate Task<PermissionFlag> GetTokenablePermissingFlagAsync(TContext context, CancellationToken cancellationToken);

        public ContextualPermissingAsyncStage(GetPermissingFlagAsync continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        public ContextualPermissingAsyncStage(GetTokenablePermissingFlagAsync cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        public ContextualPermissingAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        public ContextualPermissingAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }

        protected override Task<PermissionFlag> GetPermissionFlagByAsync(TBuffer buffer, GetPermissingFlagAsync subDelegate)
        {
            return subDelegate.Invoke(buffer.Context);
        }

        protected override Task<PermissionFlag> GetPermissionFlagByAsync(TBuffer buffer, GetTokenablePermissingFlagAsync subDelegate)
        {
            return subDelegate.Invoke(buffer.Context, buffer.CancellationToken);
        }
    }
}