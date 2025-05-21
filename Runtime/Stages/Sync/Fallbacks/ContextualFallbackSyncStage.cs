using System;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Permissions;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ContextualFallbackSyncStage<TBuffer, TContext, TMember, TDelegate> : FallbackSyncStage<TBuffer, TMember, TDelegate>
        where TBuffer : ContextualSyncBuffer<TContext, TMember>
        where TMember : IOperationMember
        where TDelegate : Delegate
    {
        protected ContextualFallbackSyncStage(TDelegate subDelegate) : base(subDelegate)
        {
        }

        protected ContextualFallbackSyncStage(GetMemberDelegate delegateGetter) : base(delegateGetter)
        {
        }
    }

    public class ContextualFallbackSyncStage<TBuffer, TContext, TMember> : ContextualFallbackSyncStage<TBuffer, TContext, TMember, ContextualFallbackSyncStage<TBuffer, TContext, TMember>.OnFallback>
        where TBuffer : ContextualSyncBuffer<TContext, TMember>
        where TMember : IOperationMember
    {
        public delegate void OnFallback(PermissionFlag operationPermission, TContext context);

        public ContextualFallbackSyncStage(OnFallback subDelegate) : base(subDelegate)
        {
        }

        public ContextualFallbackSyncStage(GetMemberDelegate delegateGetter) : base(delegateGetter)
        {
        }

        protected override void Execute(TBuffer buffer, OnFallback subDelegate)
        {
            subDelegate.Invoke(buffer.PermissionFlag, buffer.Context);
        }
    }
}