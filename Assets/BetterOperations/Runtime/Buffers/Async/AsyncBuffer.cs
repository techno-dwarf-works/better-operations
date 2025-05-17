using System.Collections.Generic;
using System.Threading;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Buffers
{
    public class AsyncBuffer<TMember> : MemberedBuffer<TMember>
        where TMember : IOperationMember
    {
        private CancellationTokenSource _cancellationTokenSource;

        public CancellationToken CancellationToken => _cancellationTokenSource.Token;
        public override bool IsCancellationRequested => _cancellationTokenSource.IsCancellationRequested;

        public AsyncBuffer(IEnumerable<TMember> members, CancellationToken cancellationToken)
            : base(members)
        {
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        }

        public override void Cancel()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}