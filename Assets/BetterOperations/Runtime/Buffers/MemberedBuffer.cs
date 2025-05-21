using System.Collections.Generic;
using System.Linq;
using System.Text;
using Better.Commons.Runtime.Extensions;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Buffers
{
    public abstract class MemberedBuffer<TMember> : OperationBuffer
        where TMember : IOperationMember
    {
        public TMember[] Members { get; }

        public MemberedBuffer(IEnumerable<TMember> members)
        {
            Members = members.ToArray();
        }

        public override void CollectInfo(ref StringBuilder stringBuilder)
        {
            base.CollectInfo(ref stringBuilder);

            stringBuilder.AppendFieldLine(nameof(Members), Members.Length);
        }
    }
}