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
        protected void Execute(TBuffer buffer)
        {
            OnPreExecute(buffer);

            for (int i = 0; i < Adapters.Length; i++)
            {
                Adapters[i].Run(buffer);
            }

            OnPostExecute(buffer);
        }

        protected override void ExecuteNext(TBuffer buffer)
        {
            Execute(buffer);
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
        public void Execute()
        {
            var buffer = new SyncBuffer<TMember>(Members);
            Execute(buffer);
        }
    }
}