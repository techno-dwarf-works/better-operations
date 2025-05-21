using System;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Extensions;
using Better.Operations.Runtime.Instructions;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Permissions;

namespace Better.Operations.Runtime.Stages
{
    public abstract class FallbackSyncStage<TBuffer, TMember, TDelegate> : DelegateSyncStage<TBuffer, TMember, TDelegate>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
        where TDelegate : Delegate
    {
        public override ExecuteInstruction ExecuteInstruction => ExecuteInstruction.Optional;

        protected FallbackSyncStage(TDelegate subDelegate) : base(subDelegate)
        {
        }

        protected FallbackSyncStage(GetMemberDelegate delegateGetter) : base(delegateGetter)
        {
        }

        protected override bool IsAvailable(PermissionFlag permissionFlag)
        {
            var isDeny = permissionFlag.IsDeny();
            return isDeny;
        }
    }

    public class FallbackSyncStage<TBuffer, TMember> : FallbackSyncStage<TBuffer, TMember, FallbackSyncStage<TBuffer, TMember>.OnFallback>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public delegate void OnFallback(PermissionFlag operationPermission);

        public FallbackSyncStage(OnFallback subDelegate) : base(subDelegate)
        {
        }

        public FallbackSyncStage(GetMemberDelegate delegateGetter) : base(delegateGetter)
        {
        }

        protected override void Execute(TBuffer buffer, OnFallback subDelegate)
        {
            subDelegate.Invoke(buffer.PermissionFlag);
        }
    }
}