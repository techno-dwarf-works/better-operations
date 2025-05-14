using System.Collections.Generic;
using System.Linq;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Buffers
{
    public class MemberedBuffer<TMember> : OperationBuffer
        where TMember : IOperationMember
    {
        public TMember[] Members { get; }

        public MemberedBuffer(IEnumerable<TMember> members)
        {
            Members = members.ToArray();
        }
    }
}