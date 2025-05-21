using System;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ValueModifySyncStage<TBuffer, TValue, TMember, TDelegate> : ModifySyncStage<TBuffer, TMember, TDelegate>
        where TBuffer : ValueSyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
        where TDelegate : Delegate
    {
        protected ValueModifySyncStage(TDelegate subDelegate) : base(subDelegate)
        {
        }

        protected ValueModifySyncStage(GetMemberDelegate delegateGetter) : base(delegateGetter)
        {
        }
    }

    public class ValueModifySyncStage<TBuffer, TValue, TMember> : ValueModifySyncStage<TBuffer, TValue, TMember, ValueModifySyncStage<TBuffer, TValue, TMember>.OnModify>
        where TBuffer : ValueSyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        public delegate TValue OnModify(TValue source, TValue modified);

        public ValueModifySyncStage(OnModify subDelegate) : base(subDelegate)
        {
        }

        public ValueModifySyncStage(GetMemberDelegate delegateGetter) : base(delegateGetter)
        {
        }

        protected override void Execute(TBuffer buffer, OnModify subDelegate)
        {
            var modified = subDelegate.Invoke(buffer.SourceValue, buffer.ModifiedValue);
            buffer.ModifiedValue = modified;
        }
    }
}