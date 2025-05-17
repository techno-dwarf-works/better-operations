using System.Collections.Generic;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Buffers
{
    public class SyncBuffer<TMember> : MemberedBuffer<TMember>
        where TMember : IOperationMember
    {
        private bool _isCancellationRequested;
        public override bool IsCancellationRequested => _isCancellationRequested;

        public SyncBuffer(IEnumerable<TMember> members) : base(members)
        {
        }

        public override void Cancel()
        {
            _isCancellationRequested = true;
        }
    }
}