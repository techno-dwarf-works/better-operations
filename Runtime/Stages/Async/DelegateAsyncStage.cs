using System;
using System.Linq;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public abstract class DelegateAsyncStage<TBuffer, TMember> : PermissionableAsyncStage<TBuffer, TMember>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
    {
        protected enum ExecuteDelegateMode
        {
            SubDelegate,
            MemberDelegate,
        }

        protected ExecuteDelegateMode ExecuteMode { get; }

        protected DelegateAsyncStage(ExecuteDelegateMode executeMode)
        {
            ExecuteMode = executeMode;
        }

        protected sealed override Task ExecuteAsync(TBuffer buffer)
        {
            if (ExecuteMode == ExecuteDelegateMode.SubDelegate)
            {
                var subDelegateTask = ExecuteSubDelegateAsync(buffer);
                return subDelegateTask;
            }

            var memberSubTasks = buffer.Members.Select(member => ExecuteMemberDelegateAsync(buffer, member)).WhenAll();
            return memberSubTasks;
        }

        protected abstract Task ExecuteSubDelegateAsync(TBuffer buffer);
        protected abstract Task ExecuteMemberDelegateAsync(TBuffer buffer, TMember member);
    }

    public abstract class DelegateAsyncStage<TBuffer, TMember, TDelegate> : DelegateAsyncStage<TBuffer, TMember>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
        where TDelegate : Delegate
    {
        private TDelegate _subDelegate;
        private GetMemberDelegate _memberDelegateGetter;

        public delegate TDelegate GetMemberDelegate(TMember member);

        protected DelegateAsyncStage(TDelegate subDelegate) : base(ExecuteDelegateMode.SubDelegate)
        {
            _subDelegate = subDelegate;
        }

        protected DelegateAsyncStage(GetMemberDelegate memberDelegateGetter) : base(ExecuteDelegateMode.MemberDelegate)
        {
            _memberDelegateGetter = memberDelegateGetter;
        }

        protected sealed override Task ExecuteSubDelegateAsync(TBuffer buffer)
        {
            return ExecuteSubDelegateAsync(buffer, _subDelegate);
        }

        protected abstract Task ExecuteSubDelegateAsync(TBuffer buffer, TDelegate subDelegate);

        protected sealed override Task ExecuteMemberDelegateAsync(TBuffer buffer, TMember member)
        {
            var memberSubDelegate = _memberDelegateGetter.Invoke(member);
            return ExecuteSubDelegateAsync(buffer, memberSubDelegate);
        }
    }

    public abstract class DelegateAsyncStage<TBuffer, TMember, TContinuousDelegate, TCancellableDelegate> : DelegateAsyncStage<TBuffer, TMember>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
        where TContinuousDelegate : Delegate
        where TCancellableDelegate : Delegate
    {
        protected enum BreakingDelegateMode
        {
            Continuous,
            Cancellable,
        }

        protected BreakingDelegateMode BreakingMode { get; }

        private TContinuousDelegate _continuousSubDelegate;
        private TCancellableDelegate _cancellableSubDelegate;
        private GetContinuousMemberDelegate _continuousDelegateGetter;
        private GetCancellableMemberDelegate _cancellableDelegateGetter;

        public delegate TContinuousDelegate GetContinuousMemberDelegate(TMember member);

        public delegate TCancellableDelegate GetCancellableMemberDelegate(TMember member);

        private DelegateAsyncStage(ExecuteDelegateMode executeMode, BreakingDelegateMode breakingMode)
            : base(executeMode)
        {
            BreakingMode = breakingMode;
        }

        protected DelegateAsyncStage(TContinuousDelegate continuousSubDelegate)
            : this(ExecuteDelegateMode.SubDelegate, BreakingDelegateMode.Continuous)
        {
            _continuousSubDelegate = continuousSubDelegate;
        }

        protected DelegateAsyncStage(TCancellableDelegate cancellableSubDelegate)
            : this(ExecuteDelegateMode.SubDelegate, BreakingDelegateMode.Cancellable)
        {
            _cancellableSubDelegate = cancellableSubDelegate;
        }

        protected DelegateAsyncStage(GetContinuousMemberDelegate continuousDelegateGetter)
            : this(ExecuteDelegateMode.MemberDelegate, BreakingDelegateMode.Continuous)
        {
            _continuousDelegateGetter = continuousDelegateGetter;
        }

        protected DelegateAsyncStage(GetCancellableMemberDelegate cancellableDelegateGetter)
            : this(ExecuteDelegateMode.MemberDelegate, BreakingDelegateMode.Cancellable)
        {
            _cancellableDelegateGetter = cancellableDelegateGetter;
        }

        protected sealed override Task ExecuteSubDelegateAsync(TBuffer buffer)
        {
            if (BreakingMode == BreakingDelegateMode.Continuous)
            {
                var continuousTask = ExecuteSubDelegateAsync(buffer, _continuousSubDelegate);
                return continuousTask;
            }

            var cancellableTask = ExecuteSubDelegateAsync(buffer, _cancellableSubDelegate);
            return cancellableTask;
        }

        protected abstract Task ExecuteSubDelegateAsync(TBuffer buffer, TContinuousDelegate subDelegate);
        protected abstract Task ExecuteSubDelegateAsync(TBuffer buffer, TCancellableDelegate subDelegate);

        protected sealed override Task ExecuteMemberDelegateAsync(TBuffer buffer, TMember member)
        {
            if (BreakingMode == BreakingDelegateMode.Continuous)
            {
                var continuousSubDelegate = _continuousDelegateGetter.Invoke(member);
                return ExecuteSubDelegateAsync(buffer, continuousSubDelegate);
            }

            var cancellableSubDelegate = _cancellableDelegateGetter.Invoke(member);
            return ExecuteSubDelegateAsync(buffer, cancellableSubDelegate);
        }
    }
}