using System;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public class CallbackSyncStage<TBuffer, TMember> : SyncStage<TBuffer, TMember>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
        /// //////////////////////
        /// //////////////////////
        /// //////////////////////
        /// //////////////////////
        /// //////////////////////
        /// //////////////////////
        /// //////////////////////
        /// //////////////////////
        /// //////////////////////
        /// //////////////////////
        /// //////////////////////
        public void Register(Action action)
        {
        }

        public override TBuffer Execute(TBuffer buffer)
        {
            return buffer;
        }
    }
}