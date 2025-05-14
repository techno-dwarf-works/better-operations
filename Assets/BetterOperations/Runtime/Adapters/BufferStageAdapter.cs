using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Adapters
{
    public abstract class BufferStageAdapter<TBuffer>
        where TBuffer : OperationBuffer
    {
        public abstract OperationStage<TBuffer> Stage { get; }
    }
}