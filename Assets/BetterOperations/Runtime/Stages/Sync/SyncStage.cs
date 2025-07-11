﻿using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Instructions;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public abstract class SyncStage<TBuffer, TMember> : MemberedStage<TBuffer, TMember>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public virtual ExecuteInstruction ExecuteInstruction => ExecuteInstruction.Mandatory;

        public abstract bool TryExecute(TBuffer buffer);
    }
}