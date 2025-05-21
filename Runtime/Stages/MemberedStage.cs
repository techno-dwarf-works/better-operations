using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Stages
{
    public abstract class MemberedStage<TBuffer, TMember> : OperationStage<TBuffer>
        where TBuffer : MemberedBuffer<TMember>
        where TMember : IOperationMember
    {
    }
}