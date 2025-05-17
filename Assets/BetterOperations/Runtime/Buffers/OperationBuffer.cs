namespace Better.Operations.Runtime.Buffers
{
    public abstract class OperationBuffer
    {
        public abstract bool IsCancellationRequested { get; }
        public abstract void Cancel();
    }
}