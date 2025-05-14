using System.Collections.Generic;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Buffers
{
    public class SyncBuffer<TMember> : MemberedBuffer<TMember>
        where TMember : IOperationMember
    {
        public SyncBuffer(IEnumerable<TMember> members) : base(members)
        {
        }
    }
}