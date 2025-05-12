using Better.Operations.Runtime.Buffers;

namespace Better.Operations.Runtime.Stages
{
    public abstract class SyncOperationStage<TBuffer, TMember> : OperationStage<TBuffer, TMember>
        where TBuffer : SyncOperationBuffer<TMember>
        where TMember : IOperationMember
    {
        public abstract void Run(OperationBuffer<TMember> buffer);
    }

    public abstract class SyncOperationStage<TMember> : SyncOperationStage<SyncOperationBuffer<TMember>, TMember>
        where TMember : IOperationMember
    {
    }
}