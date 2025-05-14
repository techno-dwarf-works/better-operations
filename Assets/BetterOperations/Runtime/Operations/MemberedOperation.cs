using System.Collections.Generic;
using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime
{
    public abstract class MemberedOperation<TBuffer, TAdapter, TMember> : Operation<TBuffer, TAdapter>
        where TBuffer : MemberedBuffer<TMember>
        where TAdapter : MemberedAdapter<TBuffer, TMember>
        where TMember : IOperationMember
    {
        private TAdapter[] _adapters;
        private HashSet<TMember> _members;
        protected IReadOnlyCollection<TMember> Members => _members;

        protected MemberedOperation()
        {
            _members = new();
        }

        internal void SetupAdapters(TAdapter[] adapters)
        {
            _adapters = adapters;
        }
    }

    public abstract class MemberedOperation<TAdapter, TMember> : MemberedOperation<MemberedBuffer<TMember>, TAdapter, TMember>
        where TAdapter : MemberedAdapter<MemberedBuffer<TMember>, TMember>
        where TMember : IOperationMember
    {
    }
}