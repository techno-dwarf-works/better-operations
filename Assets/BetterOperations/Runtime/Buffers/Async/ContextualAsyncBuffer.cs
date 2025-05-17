using System.Collections.Generic;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Buffers
{
    public class ContextualAsyncBuffer<TContext, TMember> : AsyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public TContext Context { get; }

        public ContextualAsyncBuffer(IEnumerable<TMember> members, TContext context) : base(members)
        {
            Context = context;
        }
    }
}