using System;
using Better.Operations.Runtime.Buffers;

namespace Better.Operations.Runtime.Stages
{
    public class NotifySyncOperationStage<TMember> : SyncOperationStage<TMember>
        where TMember : IOperationMember
    {
        private Action _action;

        public void Register(Action action)
        {
            _action = action;
        }

        public override void Run(OperationBuffer<TMember> buffer)
        {
            _action.Invoke();
        }
    }
}