using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Builders
{
    public abstract class AsyncOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TMember> : MemberedOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TMember>
        where TBuilder : AsyncOperationBuilder<TBuilder, TOperation, TBuffer, TAdapter, TMember>, new()
        where TOperation : AsyncOperation<TBuffer, TAdapter, TMember>, new()
        where TBuffer : AsyncBuffer<TMember>
        where TAdapter : AsyncAdapter<TBuffer, TMember>
        where TMember : IOperationMember
    {
        // protected TBuilder InsertCallback(int index, Action action)
        // {
        //     var joinIndex = index - 1;
        //     var adapter = Adapters.ElementAtOrDefault(joinIndex, true);
        //     if (adapter?.Stage is not CallbackAsyncStage<TBuffer, TMember> callbackAsyncStage)
        //     {
        //         callbackAsyncStage = new();
        //         adapter = new DerivedAsyncAdapter<TBuffer, TMember>(callbackAsyncStage);
        //         Adapters.Insert(index, adapter);
        //     }
        //
        //     callbackAsyncStage.Register(action);
        //     return (TBuilder)this;
        // }
    }

    public abstract class AsyncOperationBuilder<TBuilder, TOperation, TBuffer, TMember> : MemberedOperationBuilder<TBuilder, TOperation, TBuffer, AsyncAdapter<TBuffer, TMember>, TMember>
        where TBuilder : AsyncOperationBuilder<TBuilder, TOperation, TBuffer, TMember>, new()
        where TOperation : AsyncOperation<TBuffer, AsyncAdapter<TBuffer, TMember>, TMember>, new()
        where TBuffer : AsyncBuffer<TMember>
        where TMember : IOperationMember
    {
    }

    public class AsyncOperationBuilder<TBuilder, TMember> : AsyncOperationBuilder<TBuilder, AsyncOperation<TMember>, AsyncBuffer<TMember>, TMember>
        where TBuilder : AsyncOperationBuilder<TBuilder, TMember>, new()
        where TMember : IOperationMember
    {
    }

    public class AsyncOperationBuilder<TMember> : AsyncOperationBuilder<AsyncOperationBuilder<TMember>, TMember>
        where TMember : IOperationMember
    {
    }
}