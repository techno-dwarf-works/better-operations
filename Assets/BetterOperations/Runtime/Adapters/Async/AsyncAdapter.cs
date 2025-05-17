using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Adapters
{
    public abstract class AsyncAdapter<TBuffer, TMember> : MemberedAdapter<TBuffer, TMember>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
    {
        public abstract Task RunAsync(TBuffer buffer);
    }

    public abstract class AsyncAdapter<TBuffer, TStage, TMember> : AsyncAdapter<TBuffer, TMember>
        where TBuffer : AsyncBuffer<TMember>
        where TStage : AsyncStage<TBuffer, TMember>
        where TMember : IOperationMember
    {
        public override OperationStage<TBuffer> Stage => RelativeStage;
        public TStage RelativeStage { get; }

        protected AsyncAdapter(TStage stage)
        {
            RelativeStage = stage;
        }
    }
}