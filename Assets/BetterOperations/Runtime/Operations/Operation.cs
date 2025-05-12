using System.Collections.Generic;
using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;

namespace Better.Operations.Runtime
{
    public abstract class Operation<TBuffer, TAdapter, TMember>
        where TBuffer : OperationBuffer<TMember>
        where TAdapter : BufferStageAdapter<TBuffer, TMember>
        where TMember : IOperationMember
    {
        private TAdapter[] _adapters;
        private HashSet<TMember> _members;
        protected IReadOnlyCollection<TMember> Members => _members;

        protected Operation()
        {
            _members = new();
        }

        internal void SetupAdapters(TAdapter[] adapters)
        {
            _adapters = adapters;
        }
    }

    public abstract class Operation<TAdapter, TMember> : Operation<OperationBuffer<TMember>, TAdapter, TMember>
        where TAdapter : BufferStageAdapter<OperationBuffer<TMember>, TMember>
        where TMember : IOperationMember
    {
    }
}