using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ValueNotificationAsyncStage<TBuffer, TValue, TMember, TContinuousDelegate, TCancellableDelegate> : NotificationAsyncStage<TBuffer, TMember, TContinuousDelegate, TCancellableDelegate>
        where TBuffer : ValueAsyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
        where TContinuousDelegate : Delegate
        where TCancellableDelegate : Delegate
    {
        protected ValueNotificationAsyncStage(TContinuousDelegate continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        protected ValueNotificationAsyncStage(TCancellableDelegate cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        protected ValueNotificationAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        protected ValueNotificationAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }
    }

    public class ValueNotificationAsyncStage<TBuffer, TValue, TMember> : ValueNotificationAsyncStage<TBuffer, TValue, TMember, ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnNotificationAsync, ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnTokenableNotificationAsync>
        where TBuffer : ValueAsyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        public delegate Task OnNotificationAsync(TValue sourceValue, TValue modifiedValue);

        public delegate Task OnTokenableNotificationAsync(TValue sourceValue, TValue modifiedValue, CancellationToken cancellationToken);

        public ValueNotificationAsyncStage(OnNotificationAsync continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        public ValueNotificationAsyncStage(OnTokenableNotificationAsync cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        public ValueNotificationAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        public ValueNotificationAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }

        protected override Task ExecuteSubDelegateAsync(TBuffer buffer, OnNotificationAsync subDelegate)
        {
            return subDelegate.Invoke(buffer.SourceValue, buffer.ModifiedValue);
        }

        protected override Task ExecuteSubDelegateAsync(TBuffer buffer, OnTokenableNotificationAsync subDelegate)
        {
            return subDelegate.Invoke(buffer.SourceValue, buffer.ModifiedValue, buffer.CancellationToken);
        }
    }
}