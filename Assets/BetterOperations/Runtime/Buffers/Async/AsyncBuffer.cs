using System.Collections.Generic;
using System.Threading;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Permissions;

namespace Better.Operations.Runtime.Buffers
{
    public class AsyncBuffer<TMember> : MemberedBuffer<TMember>
        where TMember : IOperationMember
    {
        private CancellationTokenSource _cancellationTokenSource;

        public PermissionFlag PermissionFlag { get; private set; }
        public CancellationToken CancellationToken => _cancellationTokenSource.Token;
        public bool IsCancellationRequested => _cancellationTokenSource.IsCancellationRequested;

        public AsyncBuffer(IEnumerable<TMember> members, CancellationToken cancellationToken) : base(members)
        {
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        }

        public virtual bool SetPermissionFlag(PermissionFlag permissionFlag)
        {
            PermissionFlag = PermissionFlag.MostSignificant(PermissionFlag, permissionFlag);

            var changed = PermissionFlag == permissionFlag;
            return changed;
        }

        public virtual void RequestCancellation(bool force = false)
        {
            _cancellationTokenSource.Cancel();

            if (force)
            {
                var permissionFlag = PermissionFlag.Create(PermissionValues.MaxDeny);
                SetPermissionFlag(permissionFlag);
            }
        }
    }
}