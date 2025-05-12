using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;

namespace Better.Operations.Runtime
{
    public abstract class SyncOperation<TBuffer, TAdapter, TMember> : Operation<TBuffer, TAdapter, TMember>
        where TBuffer : SyncOperationBuffer<TMember>
        where TAdapter : SyncBufferAdapter<TBuffer, TMember>
        where TMember : IOperationMember
    {
        protected void Run(TBuffer buffer)
        {
            // IMPL
        }
    }

    public class SyncOperation<TMember> : SyncOperation<SyncOperationBuffer<TMember>, SyncBufferAdapter<SyncOperationBuffer<TMember>, TMember>, TMember>
        where TMember : IOperationMember
    {
        public void Run()
        {
            var buffer = new SyncOperationBuffer<TMember>(Members);
            Run(buffer);
        }
    }
}