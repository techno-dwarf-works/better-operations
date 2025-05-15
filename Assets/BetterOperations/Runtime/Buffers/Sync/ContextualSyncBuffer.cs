using System.Collections.Generic;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Buffers
{
    public class ContextualSyncBuffer<TContext, TMember> : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public TContext Context { get; }

        public ContextualSyncBuffer(IEnumerable<TMember> members, TContext context) : base(members)
        {
            Context = context;
        }
    }
}