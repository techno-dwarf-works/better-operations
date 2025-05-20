using System;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Permissions;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ValueFallbackSyncStage<TBuffer, TValue, TMember, TDelegate> : FallbackSyncStage<TBuffer, TMember, TDelegate>
        where TBuffer : ValueSyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
        where TDelegate : Delegate
    {
        protected ValueFallbackSyncStage(TDelegate subDelegate) : base(subDelegate)
        {
        }

        protected ValueFallbackSyncStage(GetDelegate delegateGetter) : base(delegateGetter)
        {
        }
    }

    public class ValueFallbackSyncStage<TBuffer, TValue, TMember> : ValueFallbackSyncStage<TBuffer, TValue, TMember, ValueFallbackSyncStage<TBuffer, TValue, TMember>.OnFallback>
        where TBuffer : ValueSyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        public delegate void OnFallback(PermissionFlag operationPermission, TValue sourceValue, TValue modifiedValue);

        public ValueFallbackSyncStage(OnFallback subDelegate) : base(subDelegate)
        {
        }

        public ValueFallbackSyncStage(GetDelegate delegateGetter) : base(delegateGetter)
        {
        }

        protected override void Execute(TBuffer buffer, OnFallback subDelegate)
        {
            subDelegate.Invoke(buffer.PermissionFlag, buffer.SourceValue, buffer.ModifiedValue);
        }
    }
}