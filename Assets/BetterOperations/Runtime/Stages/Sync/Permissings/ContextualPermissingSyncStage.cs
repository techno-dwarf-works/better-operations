using System;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Permissions;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ContextualPermissingSyncStage<TBuffer, TContext, TMember, TDelegate> : PermissingSyncStage<TBuffer, TMember, TDelegate>
        where TBuffer : ContextualSyncBuffer<TContext, TMember>
        where TMember : IOperationMember
        where TDelegate : Delegate
    {
        protected ContextualPermissingSyncStage(TDelegate subDelegate) : base(subDelegate)
        {
        }

        protected ContextualPermissingSyncStage(GetDelegate delegateGetter) : base(delegateGetter)
        {
        }
    }

    public class ContextualPermissingSyncStage<TBuffer, TContext, TMember> : ContextualPermissingSyncStage<TBuffer, TContext, TMember, ContextualPermissingSyncStage<TBuffer, TContext, TMember>.GetPermissionFlag>
        where TBuffer : ContextualSyncBuffer<TContext, TMember>
        where TMember : IOperationMember
    {
        public delegate PermissionFlag GetPermissionFlag(TContext context);

        public ContextualPermissingSyncStage(GetPermissionFlag subDelegate) : base(subDelegate)
        {
        }

        public ContextualPermissingSyncStage(GetDelegate delegateGetter) : base(delegateGetter)
        {
        }

        protected override PermissionFlag GetPermissionFlagBy(TBuffer buffer, GetPermissionFlag subDelegate)
        {
            return subDelegate.Invoke(buffer.Context);
        }
    }
}