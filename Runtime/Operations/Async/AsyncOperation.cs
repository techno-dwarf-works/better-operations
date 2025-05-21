using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Instructions;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime
{
    public abstract class AsyncOperation<TBuffer, TMember> : MemberedOperation<TBuffer, AsyncAdapter<TBuffer, TMember>, TMember>
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
    {
        protected async Task<TBuffer> ExecuteAsync(TBuffer buffer)
        {
            OnPreExecute(buffer);
            await ExecuteAdaptersAsync(buffer);
            OnPostExecute(buffer);

            return buffer;
        }

        private async Task ExecuteAdaptersAsync(TBuffer buffer)
        {
            foreach (var adapter in Adapters)
            {
                var result = await adapter.TryExecuteAsync(buffer);
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
            ExecuteAsync(buffer).Forget();
        }

        public override void Dispose()
        {
            base.Dispose();

            var executingBuffers = GetAllExecutingBuffers();
            foreach (var executingBuffer in executingBuffers)
            {
                executingBuffer.RequestCancellation(true);
            }
        }
    }

    public class AsyncOperation<TMember> : AsyncOperation<AsyncBuffer<TMember>, TMember>
        where TMember : IOperationMember
    {
        public Task<AsyncBuffer<TMember>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var buffer = new AsyncBuffer<TMember>(Members, cancellationToken);
            return ExecuteAsync(buffer);
        }
    }

    public class AsyncOperation : AsyncOperation<IOperationMember>
    {
    }
}