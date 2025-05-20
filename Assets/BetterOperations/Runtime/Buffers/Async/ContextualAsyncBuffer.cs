using System.Collections.Generic;
using System.Threading;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Buffers
{
    public class ContextualAsyncBuffer<TContext, TMember> : AsyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public TContext Context { get; set; }

        public ContextualAsyncBuffer(IEnumerable<TMember> members, TContext context, CancellationToken cancellationToken)
            : base(members, cancellationToken)
        {
            Context = context;
        }
    }
}