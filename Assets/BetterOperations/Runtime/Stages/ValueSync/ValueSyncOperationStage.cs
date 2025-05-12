using Better.Operations.Runtime.Buffers;

namespace Better.Operations.Runtime.Stages
{
    public abstract class ValueSyncOperationStage<TBuffer, TValue, TMember> : SyncOperationStage<TBuffer, TMember>
        where TBuffer : ValueSyncOperationBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    // public abstract class ValueSyncOperationStage<TValue, TMember> : ValueSyncOperationStage<ValueSyncOperationBuffer<TValue, TMember>, TValue, TMember>
    //     where TValue : struct
    //     where TMember : IOperationMember
    // {
    // }
}