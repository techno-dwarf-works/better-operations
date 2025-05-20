using System.Collections.Generic;
using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime
{
    public abstract class MemberedOperation<TBuffer, TAdapter, TMember> : Operation<TBuffer, TAdapter>, IOperationMemberRegistry<TMember>
        where TBuffer : MemberedBuffer<TMember>
        where TAdapter : MemberedAdapter<TBuffer, TMember>
        where TMember : IOperationMember
    {
        private HashSet<TMember> _members;

        protected IReadOnlyCollection<TMember> Members => _members;
        public int MembersCount => _members.Count;

        protected MemberedOperation()
        {
            _members = new();
        }

        #region IOperationMemberRegistry

        public bool Register(TMember member)
        {
            var registered = _members.Add(member);
            return registered;
        }

        public bool IsRegistered(TMember member)
        {
            var registered = _members.Contains(member);
            return registered;
        }

        public bool Unregister(TMember member)
        {
            var unregistered = _members.Remove(member);
            return unregistered;
        }

        #endregion
    }

    public abstract class MemberedOperation<TAdapter, TMember> : MemberedOperation<MemberedBuffer<TMember>, TAdapter, TMember>
        where TAdapter : MemberedAdapter<MemberedBuffer<TMember>, TMember>
        where TMember : IOperationMember
    {
    }
}