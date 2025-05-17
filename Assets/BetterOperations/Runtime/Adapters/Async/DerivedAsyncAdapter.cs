using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Adapters
{
    public class DerivedAsyncAdapter<TBuffer, TMember> : AsyncAdapter<TBuffer, AsyncStage<TBuffer, TMember>, TMember>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public DerivedAsyncAdapter(AsyncStage<TBuffer, TMember> stage) : base(stage)
        {
        }

        public override Task<TBuffer> RunAsync(TBuffer buffer)
        {
            return RelativeStage.ExecuteAsync(buffer);
        }
    }
}