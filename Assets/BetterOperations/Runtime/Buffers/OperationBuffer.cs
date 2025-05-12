using System.Collections.Generic;
using System.Linq;

namespace Better.Operations.Runtime.Buffers
{
    public class OperationBuffer<TMember>
        where TMember : IOperationMember
    {
        public TMember[] Members { get; }

        public OperationBuffer(IEnumerable<TMember> members)
        {
            Members = members.ToArray();
        }
    }
}