using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ContextualModifyAsyncStage<TBuffer, TContext, TMember, TContinuousDelegate, TCancellableDelegate> : ModifyAsyncStage<TBuffer, TMember, TContinuousDelegate, TCancellableDelegate>
        where TBuffer : ContextualAsyncBuffer<TContext, TMember>
        where TMember : IOperationMember
        where TContinuousDelegate : Delegate
        where TCancellableDelegate : Delegate
    {
        protected ContextualModifyAsyncStage(TContinuousDelegate continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        protected ContextualModifyAsyncStage(TCancellableDelegate cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        protected ContextualModifyAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        protected ContextualModifyAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }
    }

    public class ContextualModifyAsyncStage<TBuffer, TContext, TMember> : ContextualModifyAsyncStage<TBuffer, TContext, TMember, ContextualModifyAsyncStage<TBuffer, TContext, TMember>.OnModifyAsync, ContextualModifyAsyncStage<TBuffer, TContext, TMember>.OnTokenableModifyAsync>
        where TBuffer : ContextualAsyncBuffer<TContext, TMember>
        where TMember : IOperationMember
    {
        public delegate Task<TContext> OnModifyAsync(TContext context);

        public delegate Task<TContext> OnTokenableModifyAsync(TContext context, CancellationToken cancellationToken);

        public ContextualModifyAsyncStage(OnModifyAsync continuousSubDelegate) : base(continuousSubDelegate)
        {
        }

        public ContextualModifyAsyncStage(OnTokenableModifyAsync cancellableSubDelegate) : base(cancellableSubDelegate)
        {
        }

        public ContextualModifyAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter) : base(continuousDelegateGetter)
        {
        }

        public ContextualModifyAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter) : base(cancellableDelegateGetter)
        {
        }

        protected override async Task ExecuteSubDelegateAsync(TBuffer buffer, OnModifyAsync subDelegate)
        {
            var modified = await subDelegate.Invoke(buffer.Context);
            buffer.Context = modified;
        }

        protected override async Task ExecuteSubDelegateAsync(TBuffer buffer, OnTokenableModifyAsync subDelegate)
        {
            var modified = await subDelegate.Invoke(buffer.Context, buffer.CancellationToken);
            buffer.Context = modified;
        }
    }
}