using System;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Extensions;
using Better.Operations.Runtime.Instructions;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Permissions;

namespace Better.Operations.Runtime.Stages
{
    public abstract class PermissingSyncStage<TBuffer, TMember, TDelegate> : DelegateSyncStage<TBuffer, TMember, TDelegate>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
        where TDelegate : Delegate
    {
        public override ExecuteInstruction ExecuteInstruction => ExecuteInstruction.Optional;

        protected PermissingSyncStage(TDelegate subDelegate) : base(subDelegate)
        {
        }

        protected PermissingSyncStage(GetMemberDelegate delegateGetter) : base(delegateGetter)
        {
        }

        protected override bool IsAvailable(PermissionFlag permissionFlag)
        {
            var isMaxDeny = permissionFlag.IsMaxDeny();
            return !isMaxDeny;
        }

        protected sealed override void Execute(TBuffer buffer, TDelegate subDelegate)
        {
            var permissionFlag = GetPermissionFlagBy(buffer, subDelegate);
            buffer.SetPermissionFlag(permissionFlag);
        }

        protected abstract PermissionFlag GetPermissionFlagBy(TBuffer buffer, TDelegate subDelegate);
    }

    public class PermissingSyncStage<TBuffer, TMember> : PermissingSyncStage<TBuffer, TMember, PermissingSyncStage<TBuffer, TMember>.GetPermissionFlag>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public delegate PermissionFlag GetPermissionFlag();

        public PermissingSyncStage(GetPermissionFlag subDelegate) : base(subDelegate)
        {
        }

        public PermissingSyncStage(GetMemberDelegate delegateGetter) : base(delegateGetter)
        {
        }

        protected override PermissionFlag GetPermissionFlagBy(TBuffer buffer, GetPermissionFlag subDelegate)
        {
            return subDelegate.Invoke();
        }
    }
}