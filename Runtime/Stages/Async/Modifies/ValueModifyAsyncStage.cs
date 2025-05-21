using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ValueModifyAsyncStage<TBuffer, TValue, TMember, TContinuousDelegate, TCancellableDelegate> : ModifyAsyncStage<TBuffer, TMember, TContinuousDelegate, TCancellableDelegate>
        where TBuffer : ValueAsyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
        where TContinuousDelegate : Delegate
        where TCancellableDelegate : Delegate
    {
        protected ValueModifyAsyncStage(TContinuousDelegate continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        protected ValueModifyAsyncStage(TCancellableDelegate cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        protected ValueModifyAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        protected ValueModifyAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }
    }

    public class ValueModifyAsyncStage<TBuffer, TValue, TMember> : ValueModifyAsyncStage<TBuffer, TValue, TMember, ValueModifyAsyncStage<TBuffer, TValue, TMember>.OnModifyAsync, ValueModifyAsyncStage<TBuffer, TValue, TMember>.OnTokenableModifyAsync>
        where TBuffer : ValueAsyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        public delegate Task<TValue> OnModifyAsync(TValue source, TValue modified);

        public delegate Task<TValue> OnTokenableModifyAsync(TValue source, TValue modified, CancellationToken cancellationToken);

        public ValueModifyAsyncStage(OnModifyAsync continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        public ValueModifyAsyncStage(OnTokenableModifyAsync cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        public ValueModifyAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        public ValueModifyAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }

        protected override async Task ExecuteSubDelegateAsync(TBuffer buffer, OnModifyAsync subDelegate)
        {
            var modified = await subDelegate.Invoke(buffer.SourceValue, buffer.ModifiedValue);
            buffer.ModifiedValue = modified;
        }

        protected override async Task ExecuteSubDelegateAsync(TBuffer buffer, OnTokenableModifyAsync subDelegate)
        {
            var modified = await subDelegate.Invoke(buffer.SourceValue, buffer.ModifiedValue, buffer.CancellationToken);
            buffer.ModifiedValue = modified;
        }
    }
}