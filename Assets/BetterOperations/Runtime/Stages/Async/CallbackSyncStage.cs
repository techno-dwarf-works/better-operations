using System;
using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public class CallbackAsyncStage<TBuffer, TMember> : AsyncStage<TBuffer, TMember>
        where TBuffer : AsyncBuffer<TMember>
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

        public override Task RunAsync(TBuffer buffer)
        {
            return default;
        }
    }
}