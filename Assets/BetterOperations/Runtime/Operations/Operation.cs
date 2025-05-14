using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;

namespace Better.Operations.Runtime
{
    public abstract class Operation<TBuffer, TAdapter>
        where TBuffer : OperationBuffer
        where TAdapter : BufferStageAdapter<TBuffer>
    {
        private TAdapter[] _adapters;

        internal void SetupAdapters(TAdapter[] adapters)
        {
            _adapters = adapters;
        }
    }
}