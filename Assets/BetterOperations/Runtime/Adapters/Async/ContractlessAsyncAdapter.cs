﻿using System.Threading.Tasks;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Instructions;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Adapters
{
    public abstract class ContractlessAsyncAdapter<TBuffer, TMember> : AsyncAdapter<TBuffer, TMember>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
    {
    }

    public class ContractlessAsyncAdapter<TBuffer, TStage, TMember> : ContractlessAsyncAdapter<TBuffer, TMember>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
        where TStage : ContractlessStage<TBuffer>
    {
        public override ExecuteInstruction ExecuteInstruction => ExecuteInstruction.Mandatory;
        public TStage Stage { get; }

        public ContractlessAsyncAdapter(TStage stage)
        {
            Stage = stage;
        }

        public override Task<bool> TryExecuteAsync(TBuffer buffer)
        {
            Stage.Execute(buffer);
            return Task.FromResult(true);
        }
    }
}