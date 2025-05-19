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
        private CancellationTokenSource _aliveTokenSource;

        protected CancellationToken AliveToken => _aliveTokenSource.Token;

        public AsyncOperation()
        {
            _aliveTokenSource = new();
        }

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

            // TODO: Add buffers cancel requesting (force) 
            _aliveTokenSource.Cancel();
        }
    }

    public class AsyncOperation<TMember> : AsyncOperation<AsyncBuffer<TMember>, TMember>
        where TMember : IOperationMember
    {
        public Task<AsyncBuffer<TMember>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var bufferTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, AliveToken);
            var buffer = new AsyncBuffer<TMember>(Members, bufferTokenSource.Token);
            return ExecuteAsync(buffer);
        }
    }

    public class AsyncOperation : AsyncOperation<IOperationMember>
    {
    }
}