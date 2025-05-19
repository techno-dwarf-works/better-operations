using System;
using System.Collections.Generic;
using Better.Commons.Runtime.Utility;
using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;

namespace Better.Operations.Runtime
{
    public abstract class Operation<TBuffer, TAdapter> : IDisposable
        where TBuffer : OperationBuffer
        where TAdapter : BufferStageAdapter<TBuffer>
    {
        private List<TBuffer> _executingBuffers;
        private Queue<TBuffer> _scheduledBuffers;

        public int ExecutingCount => _executingBuffers.Count;
        public int QueueCount => _scheduledBuffers.Count;

        protected TAdapter[] Adapters { get; private set; }

        protected Operation()
        {
            _executingBuffers = new();
            _scheduledBuffers = new();
        }

        internal void SetupAdapters(TAdapter[] adapters)
        {
            Adapters = adapters;
        }

        protected void Schedule(TBuffer buffer)
        {
            if (_scheduledBuffers.Contains(buffer))
            {
                var message = $"{nameof(buffer)}({buffer}) already scheduled";
                DebugUtility.LogException<ArgumentException>(message);
                return;
            }

            _scheduledBuffers.Enqueue(buffer);
            OnScheduled(buffer);
        }

        protected virtual void OnScheduled(TBuffer buffer)
        {
            TryExecuteNext();
        }

        protected virtual void OnPreExecute(TBuffer buffer)
        {
            if (_executingBuffers.Contains(buffer))
            {
                var message = $"Unexpected state, {nameof(buffer)}({buffer}) already executed";
                DebugUtility.LogException<InvalidOperationException>(message);
                return;
            }

            _executingBuffers.Add(buffer);
        }

        protected virtual void OnPostExecute(TBuffer buffer)
        {
            var removed = _executingBuffers.Remove(buffer);
            if (!removed)
            {
                var message = $"Unexpected state, {nameof(buffer)}({buffer}) is not executed";
                DebugUtility.LogException<InvalidOperationException>(message);
                return;
            }

            TryExecuteNext();
        }

        protected bool TryExecuteNext()
        {
            if (ExecutingCount == 0 && QueueCount > 0)
            {
                var buffer = _scheduledBuffers.Dequeue();
                ExecuteNext(buffer);

                return true;
            }

            return false;
        }

        protected abstract void ExecuteNext(TBuffer buffer);

        protected IEnumerable<TBuffer> GetAllExecutingBuffers()
        {
            var buffers = _executingBuffers.ToArray();
            return buffers;
        }

        public virtual void Dispose()
        {
            _scheduledBuffers.Clear();
        }
    }
}