using System.Collections.Generic;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Buffers
{
    public class SyncBuffer<TMember> : MemberedBuffer<TMember>
        where TMember : IOperationMember
    {
        public bool IsCancellationRequested { get; private set; }

        public SyncBuffer(IEnumerable<TMember> members) : base(members)
        {
        }

        public virtual void Cancel()
        {
            IsCancellationRequested = true;
        }
    }
}