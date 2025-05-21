using System;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ModifySyncStage<TBuffer, TMember, TDelegate> : DelegateSyncStage<TBuffer, TMember, TDelegate>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
        where TDelegate : Delegate
    {
        protected ModifySyncStage(TDelegate subDelegate) : base(subDelegate)
        {
        }

        protected ModifySyncStage(GetMemberDelegate delegateGetter) : base(delegateGetter)
        {
        }
    }
}