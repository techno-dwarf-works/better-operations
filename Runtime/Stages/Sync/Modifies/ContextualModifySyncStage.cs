using System;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ContextualModifySyncStage<TBuffer, TContext, TMember, TDelegate> : ModifySyncStage<TBuffer, TMember, TDelegate>
        where TBuffer : ContextualSyncBuffer<TContext, TMember>
        where TMember : IOperationMember
        where TDelegate : Delegate
    {
        protected ContextualModifySyncStage(TDelegate subDelegate) : base(subDelegate)
        {
        }

        protected ContextualModifySyncStage(GetMemberDelegate delegateGetter) : base(delegateGetter)
        {
        }
    }

    public class ContextualModifySyncStage<TBuffer, TContext, TMember> : ContextualModifySyncStage<TBuffer, TContext, TMember, ContextualModifySyncStage<TBuffer, TContext, TMember>.OnModify>
        where TBuffer : ContextualSyncBuffer<TContext, TMember>
        where TMember : IOperationMember
    {
        public delegate TContext OnModify(TContext context);

        public ContextualModifySyncStage(OnModify subDelegate) : base(subDelegate)
        {
        }

        public ContextualModifySyncStage(GetMemberDelegate delegateGetter) : base(delegateGetter)
        {
        }

        protected override void Execute(TBuffer buffer, OnModify subDelegate)
        {
            var modified = subDelegate.Invoke(buffer.Context);
            buffer.Context = modified;
        }
    }
}