using System.Collections.Generic;
using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;

namespace Better.Operations.Runtime.Builders
{
    public abstract class OperationBuilder<TOperation, TBuffer, TAdapter, TMember>
        where TOperation : Operation<TBuffer, TAdapter, TMember>, new()
        where TBuffer : OperationBuffer<TMember>
        where TAdapter : BufferStageAdapter<TBuffer, TMember>
        where TMember : IOperationMember
    {
        private List<TAdapter> _adapters;
        protected IReadOnlyCollection<BufferStageAdapter<TBuffer, TMember>> Adapters => _adapters;

        protected OperationBuilder()
        {
            _adapters = new();
        }

        protected void InsertAdapter(int index, TAdapter adapter)
        {
            _adapters.Insert(index, adapter);
        }

        protected void PrependAdapter(TAdapter adapter)
        {
            var index = 0;
            InsertAdapter(index, adapter);
        }

        protected void AppendAdapter(TAdapter adapter)
        {
            var index = _adapters.Count;
            InsertAdapter(index, adapter);
        }

        public TOperation Build()
        {
            OnPreBuild();

            var adaptersArray = _adapters.ToArray();
            var operation = new TOperation();
            operation.SetupAdapters(adaptersArray);

            // TODO: Lock mutable

            OnBuilt(operation);
            return operation;
        }

        protected virtual void OnPreBuild()
        {
        }

        protected virtual void OnBuilt(TOperation operation)
        {
        }
    }

    public abstract class OperationBuilder<TOperation, TAdapter, TMember> : OperationBuilder<TOperation, OperationBuffer<TMember>, TAdapter, TMember>
        where TOperation : Operation<OperationBuffer<TMember>, TAdapter, TMember>, new()
        where TAdapter : BufferStageAdapter<OperationBuffer<TMember>, TMember>
        where TMember : IOperationMember
    {
    }
}