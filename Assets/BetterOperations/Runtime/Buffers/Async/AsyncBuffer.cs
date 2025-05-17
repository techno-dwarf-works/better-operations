using System.Collections.Generic;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Buffers
{
    public class AsyncBuffer<TMember> : MemberedBuffer<TMember>
        where TMember : IOperationMember
    {
        public AsyncBuffer(IEnumerable<TMember> members) : base(members)
        {
        }
    }
}