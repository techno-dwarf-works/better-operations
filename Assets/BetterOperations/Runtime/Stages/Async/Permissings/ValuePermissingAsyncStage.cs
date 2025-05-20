using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Permissions;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ValuePermissingAsyncStage<TBuffer, TValue, TMember, TContinuousDelegate, TCancellableDelegate> : PermissingAsyncStage<TBuffer, TMember, TContinuousDelegate, TCancellableDelegate>
        where TBuffer : ValueAsyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
        where TContinuousDelegate : Delegate
        where TCancellableDelegate : Delegate
    {
        protected ValuePermissingAsyncStage(TContinuousDelegate continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        protected ValuePermissingAsyncStage(TCancellableDelegate cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        protected ValuePermissingAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        protected ValuePermissingAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }
    }

    public class ValuePermissingAsyncStage<TBuffer, TValue, TMember> : ValuePermissingAsyncStage<TBuffer, TValue, TMember, ValuePermissingAsyncStage<TBuffer, TValue, TMember>.GetPermissingFlagAsync, ValuePermissingAsyncStage<TBuffer, TValue, TMember>.GetTokenablePermissingFlagAsync>
        where TBuffer : ValueAsyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        public delegate Task<PermissionFlag> GetPermissingFlagAsync(TValue sourceValue, TValue modifiedValue);

        public delegate Task<PermissionFlag> GetTokenablePermissingFlagAsync(TValue sourceValue, TValue modifiedValue, CancellationToken cancellationToken);

        public ValuePermissingAsyncStage(GetPermissingFlagAsync continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        public ValuePermissingAsyncStage(GetTokenablePermissingFlagAsync cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        public ValuePermissingAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        public ValuePermissingAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }

        protected override Task<PermissionFlag> GetPermissionFlagByAsync(TBuffer buffer, GetPermissingFlagAsync subDelegate)
        {
            return subDelegate.Invoke(buffer.SourceValue, buffer.ModifiedValue);
        }

        protected override Task<PermissionFlag> GetPermissionFlagByAsync(TBuffer buffer, GetTokenablePermissingFlagAsync subDelegate)
        {
            return subDelegate.Invoke(buffer.SourceValue, buffer.ModifiedValue, buffer.CancellationToken);
        }
    }
}