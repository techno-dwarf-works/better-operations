using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Extensions;
using Better.Operations.Runtime.Instructions;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Permissions;

namespace Better.Operations.Runtime.Stages
{
    public abstract class PermissingAsyncStage<TBuffer, TMember, TContinuousDelegate, TCancellableDelegate> : DelegateAsyncStage<TBuffer, TMember, TContinuousDelegate, TCancellableDelegate>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
        where TContinuousDelegate : Delegate
        where TCancellableDelegate : Delegate
    {
        public override ExecuteInstruction ExecuteInstruction => ExecuteInstruction.Optional;

        protected PermissingAsyncStage(TContinuousDelegate continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        protected PermissingAsyncStage(TCancellableDelegate cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        protected PermissingAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        protected PermissingAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }

        protected override bool IsAvailable(PermissionFlag permissionFlag)
        {
            var isMaxDeny = permissionFlag.IsMaxDeny();
            return !isMaxDeny;
        }

        protected sealed override async Task ExecuteSubDelegateAsync(TBuffer buffer, TContinuousDelegate subDelegate)
        {
            var permissionFlag = await GetPermissionFlagByAsync(buffer, subDelegate);
            buffer.SetPermissionFlag(permissionFlag);
        }

        protected sealed override async Task ExecuteSubDelegateAsync(TBuffer buffer, TCancellableDelegate subDelegate)
        {
            var permissionFlag = await GetPermissionFlagByAsync(buffer, subDelegate);
            buffer.SetPermissionFlag(permissionFlag);
        }

        protected abstract Task<PermissionFlag> GetPermissionFlagByAsync(TBuffer buffer, TContinuousDelegate subDelegate);
        protected abstract Task<PermissionFlag> GetPermissionFlagByAsync(TBuffer buffer, TCancellableDelegate subDelegate);
    }

    public class PermissingAsyncStage<TBuffer, TMember> : PermissingAsyncStage<TBuffer, TMember, PermissingAsyncStage<TBuffer, TMember>.GetPermissingFlagAsync, PermissingAsyncStage<TBuffer, TMember>.GetPermissingFlagTokenableAsync>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public delegate Task<PermissionFlag> GetPermissingFlagAsync();

        public delegate Task<PermissionFlag> GetPermissingFlagTokenableAsync(CancellationToken cancellationToken);

        public PermissingAsyncStage(GetPermissingFlagAsync continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        public PermissingAsyncStage(GetPermissingFlagTokenableAsync cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        public PermissingAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        public PermissingAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }

        protected override Task<PermissionFlag> GetPermissionFlagByAsync(TBuffer buffer, GetPermissingFlagAsync subDelegate)
        {
            return subDelegate.Invoke();
        }

        protected override Task<PermissionFlag> GetPermissionFlagByAsync(TBuffer buffer, GetPermissingFlagTokenableAsync subDelegate)
        {
            return subDelegate.Invoke(buffer.CancellationToken);
        }
    }
}