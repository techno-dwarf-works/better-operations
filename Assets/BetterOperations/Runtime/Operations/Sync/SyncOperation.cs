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

    public class SyncOperation<TMember> : SyncOperation<SyncBuffer<TMember>, SyncAdapter<SyncBuffer<TMember>, TMember>, TMember>
        where TMember : IOperationMember
    {
        public void Run()
        {
            var buffer = new SyncBuffer<TMember>(Members);
            Run(buffer);
        }
    }
}