using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime
{
    public abstract class SyncOperation<TBuffer, TAdapter, TMember> : MemberedOperation<TBuffer, TAdapter, TMember>
        where TBuffer : SyncBuffer<TMember>
        where TAdapter : SyncAdapter<TBuffer, TMember>
        where TMember : IOperationMember
    {
        protected void Run(TBuffer buffer)
        {
            // IMPL
        }
    }

    public abstract class SyncOperation<TBuffer, TMember> : SyncOperation<TBuffer, SyncAdapter<TBuffer, TMember>, TMember>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
    }

    public class SyncOperation<TMember> : SyncOperation<SyncBuffer<TMember>, TMember>
        where TMember : IOperationMember
    {
        public void Run()
        {
            var buffer = new SyncBuffer<TMember>(Members);
            Run(buffer);
        }
    }
}