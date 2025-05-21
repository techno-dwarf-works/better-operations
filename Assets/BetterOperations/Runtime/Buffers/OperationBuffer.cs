using System;
using System.Text;
using Better.Commons.Runtime.Extensions;

namespace Better.Operations.Runtime.Buffers
{
    public abstract class OperationBuffer
    {
        public virtual void CollectInfo(ref StringBuilder stringBuilder)
        {
            var typeName = GetType().Name;
            stringBuilder.AppendFieldLine(nameof(Type), typeName);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            CollectInfo(ref builder);

            return builder.ToString();
        }
    }
}