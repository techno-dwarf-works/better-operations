using Better.Operations.Runtime.Buffers;

namespace Better.Operations.Runtime.Adapters
{
    public abstract class BufferStageAdapter<TBuffer, TMember>
        where TBuffer : OperationBuffer<TMember>
        where TMember : IOperationMember
    {
    }
}