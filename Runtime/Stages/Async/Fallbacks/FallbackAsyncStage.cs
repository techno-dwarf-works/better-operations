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
    public abstract class FallbackAsyncStage<TBuffer, TMember, TContinuousDelegate, TCancellableDelegate> : DelegateAsyncStage<TBuffer, TMember, TContinuousDelegate, TCancellableDelegate>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
        where TContinuousDelegate : Delegate
        where TCancellableDelegate : Delegate
    {
        public override ExecuteInstruction ExecuteInstruction => ExecuteInstruction.Optional;

        protected FallbackAsyncStage(TContinuousDelegate continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        protected FallbackAsyncStage(TCancellableDelegate cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        protected FallbackAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        protected FallbackAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }

        protected override bool IsAvailable(PermissionFlag permissionFlag)
        {
            var isDeny = permissionFlag.IsDeny();
            return isDeny;
        }
    }

    public class FallbackAsyncStage<TBuffer, TMember> : FallbackAsyncStage<TBuffer, TMember, FallbackAsyncStage<TBuffer, TMember>.OnFallbackAsync, FallbackAsyncStage<TBuffer, TMember>.OnTokenableFallbackAsync>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public delegate Task OnFallbackAsync(PermissionFlag operationPermission);

        public delegate Task OnTokenableFallbackAsync(PermissionFlag operationPermission, CancellationToken cancellationToken);

        public FallbackAsyncStage(OnFallbackAsync continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        public FallbackAsyncStage(OnTokenableFallbackAsync cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        public FallbackAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        public FallbackAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }

        protected override Task ExecuteSubDelegateAsync(TBuffer buffer, OnFallbackAsync subDelegate)
        {
            return subDelegate.Invoke(buffer.PermissionFlag);
        }

        protected override Task ExecuteSubDelegateAsync(TBuffer buffer, OnTokenableFallbackAsync subDelegate)
        {
            return subDelegate.Invoke(buffer.PermissionFlag, buffer.CancellationToken);
        }
    }
}