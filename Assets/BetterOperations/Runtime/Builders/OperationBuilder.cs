using System;
using System.Collections.Generic;
using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using UnityEngine;

namespace Better.Operations.Runtime.Builders
{
    public abstract class OperationBuilder<TBuilder, TOperation, TBuffer, TAdapter>
        where TBuilder : OperationBuilder<TBuilder, TOperation, TBuffer, TAdapter>, new()
        where TOperation : Operation<TBuffer, TAdapter>, new()
        where TBuffer : OperationBuffer
        where TAdapter : BufferStageAdapter<TBuffer>
    {
        protected List<TAdapter> Adapters { get; private set; }
        public bool IsMutable { get; private set; }

        protected OperationBuilder()
        {
            Adapters = new();
            IsMutable = true;
        }

        public TOperation Build()
        {
            if (!ValidateMutable(true, false))
            {
                var immutableMessage = "Cannot build, already immutable";
                throw new InvalidOperationException(immutableMessage);
            }

            OnPreBuild();

            var operation = new TOperation();
            var adaptersArray = Adapters.ToArray();
            operation.SetupAdapters(adaptersArray);

            OnBuilt(operation);
            return operation;
        }

        protected virtual void OnPreBuild()
        {
        }

        protected virtual void OnBuilt(TOperation operation)
        {
            Adapters.Clear();
            IsMutable = false;
        }

        protected bool ValidateMutable(bool targetState, bool logError = true)
        {
            var isValid = IsMutable == targetState;
            if (!isValid && logError)
            {
                var reason = targetState ? "must be mutable" : "must be immutable";
                var message = "Not valid, " + reason;
                Debug.LogError(message);
            }

            return isValid;
        }

        public static TBuilder Create()
        {
            return new TBuilder();
        }
    }

    public abstract class OperationBuilder<TBuilder, TOperation, TAdapter> : OperationBuilder<TBuilder, TOperation, OperationBuffer, TAdapter>
        where TBuilder : OperationBuilder<TBuilder, TOperation, TAdapter>, new()
        where TOperation : Operation<OperationBuffer, TAdapter>, new()
        where TAdapter : BufferStageAdapter<OperationBuffer>
    {
    }
}