using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Permissions;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ValueFallbackAsyncStage<TBuffer, TValue, TMember, TContinuousDelegate, TCancellableDelegate> : FallbackAsyncStage<TBuffer, TMember, TContinuousDelegate, TCancellableDelegate>
        where TBuffer : ValueAsyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
        where TContinuousDelegate : Delegate
        where TCancellableDelegate : Delegate
    {
        protected ValueFallbackAsyncStage(TContinuousDelegate continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        protected ValueFallbackAsyncStage(TCancellableDelegate cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        protected ValueFallbackAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        protected ValueFallbackAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }
    }

    public class ValueFallbackAsyncStage<TBuffer, TValue, TMember> : ValueFallbackAsyncStage<TBuffer, TValue, TMember, ValueFallbackAsyncStage<TBuffer, TValue, TMember>.OnFallbackAsync, ValueFallbackAsyncStage<TBuffer, TValue, TMember>.OnTokenableFallbackAsync>
        where TBuffer : ValueAsyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        public delegate Task OnFallbackAsync(PermissionFlag operationPermission, TValue sourceValue, TValue modifiedValue);

        public delegate Task OnTokenableFallbackAsync(PermissionFlag operationPermission, TValue sourceValue, TValue modifiedValue, CancellationToken cancellationToken);

        public ValueFallbackAsyncStage(OnFallbackAsync continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        public ValueFallbackAsyncStage(OnTokenableFallbackAsync cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        public ValueFallbackAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        public ValueFallbackAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }

        protected override Task ExecuteSubDelegateAsync(TBuffer buffer, OnFallbackAsync subDelegate)
        {
            return subDelegate.Invoke(buffer.PermissionFlag, buffer.SourceValue, buffer.ModifiedValue);
        }

        protected override Task ExecuteSubDelegateAsync(TBuffer buffer, OnTokenableFallbackAsync subDelegate)
        {
            return subDelegate.Invoke(buffer.PermissionFlag, buffer.SourceValue, buffer.ModifiedValue, buffer.CancellationToken);
        }
    }
}