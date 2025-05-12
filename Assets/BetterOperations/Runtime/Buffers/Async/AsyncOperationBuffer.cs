using System.Collections.Generic;

namespace Better.Operations.Runtime.Buffers
{
    public class AsyncOperationBuffer<TMember> : OperationBuffer<TMember>
        where TMember : IOperationMember
    {
        public AsyncOperationBuffer(IEnumerable<TMember> members) : base(members)
        {
        }
    }
}