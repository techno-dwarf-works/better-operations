using System.Collections.Generic;
using System.Text;
using System.Threading;
using Better.Commons.Runtime.Extensions;
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

        public override void CollectInfo(ref StringBuilder stringBuilder)
        {
            base.CollectInfo(ref stringBuilder);

            stringBuilder.AppendFieldLine(nameof(Context), Context);
        }
    }
}