using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Instructions;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Adapters
{
    public abstract class ContractlessSyncAdapter<TBuffer, TMember> : SyncAdapter<TBuffer, TMember>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
    }

    public class ContractlessSyncAdapter<TBuffer, TStage, TMember> : ContractlessSyncAdapter<TBuffer, TMember>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
        where TStage : ContractlessStage<TBuffer>
    {
        public override ExecuteInstruction ExecuteInstruction => ExecuteInstruction.Mandatory;
        public override OperationStage<TBuffer> Stage => RelativeStage;
        public TStage RelativeStage { get; }

        public ContractlessSyncAdapter(TStage stage)
        {
            RelativeStage = stage;
        }

        public override bool TryExecute(TBuffer buffer)
        {
            RelativeStage.Execute(buffer);
            return true;
        }
    }
}