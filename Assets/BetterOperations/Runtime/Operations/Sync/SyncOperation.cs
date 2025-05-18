using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime
{
    public abstract class SyncOperation<TBuffer, TMember> : MemberedOperation<TBuffer, SyncAdapter<TBuffer, TMember>, TMember>
        where TBuffer : SyncBuffer<TMember>
        where TMember : IOperationMember
    {
        protected TBuffer Execute(TBuffer buffer)
        {
            OnPreExecute(buffer);

            for (int i = 0; i < Adapters.Length; i++)
            {
                Adapters[i].Run(buffer); // TODO: Buffer
                // TODO: Add cancel/stop handle
            }

            OnPostExecute(buffer);
            return buffer;
        }

        protected override void ExecuteNext(TBuffer buffer)
        {
            Execute(buffer);
        }
    }

    public class SyncOperation<TMember> : SyncOperation<SyncBuffer<TMember>, TMember>
        where TMember : IOperationMember
    {
        public SyncBuffer<TMember> Execute()
        {
            var buffer = new SyncBuffer<TMember>(Members);
            return Execute(buffer);
        }
    }

    public class SyncOperation : SyncOperation<IOperationMember>
    {
    }
}