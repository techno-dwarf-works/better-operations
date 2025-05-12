using Better.Operations.Runtime.Buffers;

namespace Better.Operations.Runtime.Stages
{
    public abstract class OperationStage<TBuffer, TMember>
        where TBuffer : OperationBuffer<TMember>
        where TMember : IOperationMember
    {
    }
}