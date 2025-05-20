using Better.Operations.Runtime.Buffers;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ContractlessStage<TBuffer> : OperationStage<TBuffer>
        where TBuffer : OperationBuffer
    {
        public abstract void Execute(TBuffer buffer);
    }
}