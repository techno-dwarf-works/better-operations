using System.Collections.Generic;
using System.Text;
using Better.Commons.Runtime.Extensions;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Buffers
{
    public class ContextualSyncBuffer<TContext, TMember> : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public TContext Context { get; set; }

        public ContextualSyncBuffer(IEnumerable<TMember> members, TContext context) : base(members)
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