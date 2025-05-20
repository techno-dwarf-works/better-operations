using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Adapters
{
    public abstract class MemberedAdapter<TBuffer, TMember> : BufferAdapter<TBuffer>
        where TBuffer : MemberedBuffer<TMember>
        where TMember : IOperationMember
    {
    }
}