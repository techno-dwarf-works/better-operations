using System.Collections.Generic;

namespace Better.Operations.Runtime.Buffers
{
    public class SyncOperationBuffer<TMember> : OperationBuffer<TMember>
        where TMember : IOperationMember
    {
        public SyncOperationBuffer(IEnumerable<TMember> members) : base(members)
        {
        }
    }
}