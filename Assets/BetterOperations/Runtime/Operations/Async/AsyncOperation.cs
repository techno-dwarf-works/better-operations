using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime
{
    public abstract class AsyncOperation<TBuffer, TAdapter, TMember> : MemberedOperation<TBuffer, TAdapter, TMember>
        where TBuffer : AsyncBuffer<TMember>
        where TAdapter : AsyncAdapter<TBuffer, TMember>
        where TMember : IOperationMember
    {
        protected void Execute(TBuffer buffer)
        {
            OnPreExecute(buffer);

            // for (int i = 0; i < Adapters.Length; i++)
            // {
            //     Adapters[i].Run(buffer);
            // }

            OnPostExecute(buffer);
        }

        protected override void ExecuteNext(TBuffer buffer)
        {
            Execute(buffer);
        }
    }

    public abstract class AsyncOperation<TBuffer, TMember> : AsyncOperation<TBuffer, AsyncAdapter<TBuffer, TMember>, TMember>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
    {
    }

    public class AsyncOperation<TMember> : AsyncOperation<AsyncBuffer<TMember>, TMember>
        where TMember : IOperationMember
    {
        public void Execute()
        {
            var buffer = new AsyncBuffer<TMember>(Members);
            Execute(buffer);
        }
    }
}