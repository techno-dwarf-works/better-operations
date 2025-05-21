using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Instructions;
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
            ExecuteAdapters(buffer);
            OnPostExecute(buffer);

            return buffer;
        }

        private void ExecuteAdapters(TBuffer buffer)
        {
            foreach (var adapter in Adapters)
            {
                var result = adapter.TryExecute(buffer);
                if (result)
                {
                    continue;
                }

                if (adapter.ExecuteInstruction == ExecuteInstruction.Mandatory)
                {
                    break;
                }
            }
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