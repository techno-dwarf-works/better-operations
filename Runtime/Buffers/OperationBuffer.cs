using System.Text;

namespace Better.Operations.Runtime.Buffers
{
    public abstract class OperationBuffer
    {
        public virtual void CollectInfo(ref StringBuilder stringBuilder)
        {
            var typeName = GetType().Name;
            stringBuilder.AppendLine(typeName);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            CollectInfo(ref builder);

            return builder.ToString();
        }
    }
}