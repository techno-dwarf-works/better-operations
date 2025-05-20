using System;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public abstract class DelegateSyncStage<TBuffer, TMember, TDelegate> : PermissionableSyncStage<TBuffer, TMember>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
        where TDelegate : Delegate
    {
        protected enum ExecuteDelegateMode
        {
            SubDelegate,
            MemberDelegate,
        }

        private TDelegate _subDelegate;
        private GetDelegate _delegateGetter;

        protected ExecuteDelegateMode ExecuteMode { get; }

        public delegate TDelegate GetDelegate(TMember member);

        private DelegateSyncStage(ExecuteDelegateMode executeMode)
        {
            ExecuteMode = executeMode;
        }

        protected DelegateSyncStage(TDelegate subDelegate) : this(ExecuteDelegateMode.SubDelegate)
        {
            _subDelegate = subDelegate;
        }

        protected DelegateSyncStage(GetDelegate delegateGetter) : this(ExecuteDelegateMode.MemberDelegate)
        {
            _delegateGetter = delegateGetter;
        }

        protected override void Execute(TBuffer buffer)
        {
            if (ExecuteMode == ExecuteDelegateMode.SubDelegate)
            {
                Execute(buffer, _subDelegate);
                return;
            }

            foreach (var member in buffer.Members)
            {
                var subDelegate = _delegateGetter.Invoke(member);
                Execute(buffer, subDelegate);
            }
        }

        protected abstract void Execute(TBuffer buffer, TDelegate subDelegate);
    }
}