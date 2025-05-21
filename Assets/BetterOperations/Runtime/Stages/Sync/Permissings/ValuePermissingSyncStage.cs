using System;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Permissions;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ValuePermissingSyncStage<TBuffer, TValue, TMember, TDelegate> : PermissingSyncStage<TBuffer, TMember, TDelegate>
        where TBuffer : ValueSyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
        where TDelegate : Delegate
    {
        protected ValuePermissingSyncStage(TDelegate subDelegate) : base(subDelegate)
        {
        }

        protected ValuePermissingSyncStage(GetDelegate delegateGetter) : base(delegateGetter)
        {
        }
    }

    public class ValuePermissingSyncStage<TBuffer, TValue, TMember> : ValuePermissingSyncStage<TBuffer, TValue, TMember, ValuePermissingSyncStage<TBuffer, TValue, TMember>.GetPermissionFlag>
        where TBuffer : ValueSyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        public delegate PermissionFlag GetPermissionFlag(TValue source, TValue modified);

        public ValuePermissingSyncStage(GetPermissionFlag subDelegate) : base(subDelegate)
        {
        }

        public ValuePermissingSyncStage(GetDelegate delegateGetter) : base(delegateGetter)
        {
        }

        protected override PermissionFlag GetPermissionFlagBy(TBuffer buffer, GetPermissionFlag subDelegate)
        {
            return subDelegate.Invoke(buffer.SourceValue, buffer.ModifiedValue);
        }
    }
}