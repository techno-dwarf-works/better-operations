using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
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
        private CancellationTokenSource _aliveTokenSource;

        protected CancellationToken AliveToken => _aliveTokenSource.Token;

        public AsyncOperation()
        {
            _aliveTokenSource = new();
        }

        protected async Task<TBuffer> ExecuteAsync(TBuffer buffer)
        {
            OnPreExecute(buffer);

            // for (int i = 0; i < Adapters.Length; i++)
            // {
            //     Adapters[i].Run(buffer);
            // // TODO: Add cancel/stop handle
            // }

            OnPostExecute(buffer);
            return buffer;
        }

        protected override void ExecuteNext(TBuffer buffer)
        {
            ExecuteAsync(buffer).Forget();
        }

        public override void Dispose()
        {
            base.Dispose();

            _aliveTokenSource.Cancel();
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
        public Task ExecuteAsync(CancellationToken cancellationToken)
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