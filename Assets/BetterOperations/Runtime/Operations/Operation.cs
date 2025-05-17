using System;
using System.Collections.Generic;
using Better.Commons.Runtime.Utility;
using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;

namespace Better.Operations.Runtime
{
    public abstract class Operation<TBuffer, TAdapter>
        where TBuffer : OperationBuffer
        where TAdapter : BufferStageAdapter<TBuffer>
    {
        // TODO: Cancel
        
        private Queue<TBuffer> _buffersQueue;

        public int ExecutingCount { get; private set; }
        public int QueueCount => _buffersQueue.Count;
        protected TAdapter[] Adapters { get; private set; }

        protected Operation()
        {
            _buffersQueue = new();
        }

        internal void SetupAdapters(TAdapter[] adapters)
        {
            Adapters = adapters;
        }

        protected void Schedule(TBuffer buffer)
        {
            if (_buffersQueue.Contains(buffer))
            {
                var message = $"{nameof(buffer)}({buffer}) already scheduled";
                DebugUtility.LogException<ArgumentException>(message);
                return;
            }

            _buffersQueue.Enqueue(buffer);
            OnScheduled(buffer);
        }

        protected virtual void OnScheduled(TBuffer buffer)
        {
            TryExecuteNext();
        }

        protected virtual void OnPreExecute(TBuffer buffer)
        {
            ExecutingCount++;
        }

        protected virtual void OnPostExecute(TBuffer buffer)
        {
            if (ExecutingCount <= 0)
            {
                var message = $"Unexpected state, {nameof(ExecutingCount)}({ExecutingCount}) has unavailable value";
                DebugUtility.LogException<InvalidOperationException>(message);
                return;
            }

            ExecutingCount--;
            TryExecuteNext();
        }

        protected bool TryExecuteNext()
        {
            if (ExecutingCount == 0 && QueueCount > 0)
            {
                var buffer = _buffersQueue.Dequeue();
                ExecuteNext(buffer);

                return true;
            }

            return false;
        }

        protected abstract void ExecuteNext(TBuffer buffer);
    }
}