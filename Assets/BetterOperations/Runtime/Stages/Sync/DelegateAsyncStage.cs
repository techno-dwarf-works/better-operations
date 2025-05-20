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
        protected enum ExecuteMode
        {
            SubDelegate,
            DelegateGetter,
        }

        private TDelegate _subDelegate;
        private GetDelegate _delegateGetter;

        protected ExecuteMode Mode { get; }

        public delegate TDelegate GetDelegate(TMember member);

        private DelegateSyncStage(ExecuteMode mode)
        {
            Mode = mode;
        }

        protected DelegateSyncStage(TDelegate subDelegate) : this(ExecuteMode.SubDelegate)
        {
            _subDelegate = subDelegate;
        }

        protected DelegateSyncStage(GetDelegate delegateGetter) : this(ExecuteMode.DelegateGetter)
        {
            _delegateGetter = delegateGetter;
        }

        protected override void Execute(TBuffer buffer)
        {
            if (Mode == ExecuteMode.SubDelegate)
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