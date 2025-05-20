using Better.Commons.Runtime.Extensions;
using Better.Operations.Runtime.Adapters;
using Better.Operations.Runtime.Buffers;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Stages;

namespace Better.Operations.Runtime.Builders
{
    public abstract class ValueAsyncOperationBuilder<TBuilder, TOperation, TBuffer, TValue, TMember> : AsyncOperationBuilder<TBuilder, TOperation, TBuffer, TMember>
        where TBuilder : ValueAsyncOperationBuilder<TBuilder, TOperation, TBuffer, TValue, TMember>, new()
        where TOperation : ValueAsyncOperation<TBuffer, TValue, TMember>, new()
        where TBuffer : ValueAsyncBuffer<TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        #region Notifications

        protected virtual TBuilder InsertNotification(int index, ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnNotificationAsync notification)
        {
            var stage = new ValueNotificationAsyncStage<TBuffer, TValue, TMember>(notification);
            var adapter = new AsyncAdapter<TBuffer, ValueNotificationAsyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnNotificationAsync notification)
        {
            return InsertNotification(0, notification);
        }

        public TBuilder AppendNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnNotificationAsync notification)
        {
            return InsertNotification(Adapters.Count, notification);
        }

        protected virtual TBuilder InsertNotification(int index, ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnTokenableNotificationAsync notification)
        {
            var stage = new ValueNotificationAsyncStage<TBuffer, TValue, TMember>(notification);
            var adapter = new AsyncAdapter<TBuffer, ValueNotificationAsyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnTokenableNotificationAsync notification)
        {
            return InsertNotification(0, notification);
        }

        public TBuilder AppendNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.OnTokenableNotificationAsync notification)
        {
            return InsertNotification(Adapters.Count, notification);
        }

        protected virtual TBuilder InsertNotification(int index, ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetContinuousMemberDelegate getter)
        {
            var stage = new ValueNotificationAsyncStage<TBuffer, TValue, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, ValueNotificationAsyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertNotification(int index, ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetCancellableMemberDelegate getter)
        {
            var stage = new ValueNotificationAsyncStage<TBuffer, TValue, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, ValueNotificationAsyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetCancellableMemberDelegate getter)
        {
            return InsertNotification(0, getter);
        }

        public TBuilder AppendNotification(ValueNotificationAsyncStage<TBuffer, TValue, TMember>.GetCancellableMemberDelegate getter)
        {
            return InsertNotification(Adapters.Count, getter);
        }

        #endregion

        #region Fallbacks

        protected virtual TBuilder InsertFallback(int index, ValueFallbackAsyncStage<TBuffer, TValue, TMember>.OnFallbackAsync fallback)
        {
            var stage = new ValueFallbackAsyncStage<TBuffer, TValue, TMember>(fallback);
            var adapter = new AsyncAdapter<TBuffer, ValueFallbackAsyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(ValueFallbackAsyncStage<TBuffer, TValue, TMember>.OnFallbackAsync fallback)
        {
            return InsertFallback(0, fallback);
        }

        public TBuilder AppendFallback(ValueFallbackAsyncStage<TBuffer, TValue, TMember>.OnFallbackAsync fallback)
        {
            return InsertFallback(Adapters.Count, fallback);
        }

        protected virtual TBuilder InsertFallback(int index, ValueFallbackAsyncStage<TBuffer, TValue, TMember>.OnTokenableFallbackAsync fallback)
        {
            var stage = new ValueFallbackAsyncStage<TBuffer, TValue, TMember>(fallback);
            var adapter = new AsyncAdapter<TBuffer, ValueFallbackAsyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(ValueFallbackAsyncStage<TBuffer, TValue, TMember>.OnTokenableFallbackAsync fallback)
        {
            return InsertFallback(0, fallback);
        }

        public TBuilder AppendFallback(ValueFallbackAsyncStage<TBuffer, TValue, TMember>.OnTokenableFallbackAsync fallback)
        {
            return InsertFallback(Adapters.Count, fallback);
        }

        protected virtual TBuilder InsertFallback(int index, ValueFallbackAsyncStage<TBuffer, TValue, TMember>.GetContinuousMemberDelegate getter)
        {
            var stage = new ValueFallbackAsyncStage<TBuffer, TValue, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, ValueFallbackAsyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(ValueFallbackAsyncStage<TBuffer, TValue, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertFallback(0, getter);
        }

        public TBuilder AppendFallback(ValueFallbackAsyncStage<TBuffer, TValue, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertFallback(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertFallback(int index, ValueFallbackAsyncStage<TBuffer, TValue, TMember>.GetCancellableMemberDelegate getter)
        {
            var stage = new ValueFallbackAsyncStage<TBuffer, TValue, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, ValueFallbackAsyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependFallback(ValueFallbackAsyncStage<TBuffer, TValue, TMember>.GetCancellableMemberDelegate getter)
        {
            return InsertFallback(0, getter);
        }

        public TBuilder AppendFallback(ValueFallbackAsyncStage<TBuffer, TValue, TMember>.GetCancellableMemberDelegate getter)
        {
            return InsertFallback(Adapters.Count, getter);
        }

        #endregion

        #region Permissings

        protected virtual TBuilder InsertPermissing(int index, ValuePermissingAsyncStage<TBuffer, TValue, TMember>.GetPermissingFlagAsync getter)
        {
            var stage = new ValuePermissingAsyncStage<TBuffer, TValue, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, ValuePermissingAsyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependPermissing(ValuePermissingAsyncStage<TBuffer, TValue, TMember>.GetPermissingFlagAsync getter)
        {
            return InsertPermissing(0, getter);
        }

        public TBuilder AppendPermissing(ValuePermissingAsyncStage<TBuffer, TValue, TMember>.GetPermissingFlagAsync getter)
        {
            return InsertPermissing(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertPermissing(int index, ValuePermissingAsyncStage<TBuffer, TValue, TMember>.GetPermissingFlagTokenableAsync getter)
        {
            var stage = new ValuePermissingAsyncStage<TBuffer, TValue, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, ValuePermissingAsyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependPermissing(ValuePermissingAsyncStage<TBuffer, TValue, TMember>.GetPermissingFlagTokenableAsync getter)
        {
            return InsertPermissing(0, getter);
        }

        public TBuilder AppendPermissing(ValuePermissingAsyncStage<TBuffer, TValue, TMember>.GetPermissingFlagTokenableAsync getter)
        {
            return InsertPermissing(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertPermissing(int index, ValuePermissingAsyncStage<TBuffer, TValue, TMember>.GetContinuousMemberDelegate getter)
        {
            var stage = new ValuePermissingAsyncStage<TBuffer, TValue, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, ValuePermissingAsyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependPermissing(ValuePermissingAsyncStage<TBuffer, TValue, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertPermissing(0, getter);
        }

        public TBuilder AppendPermissing(ValuePermissingAsyncStage<TBuffer, TValue, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertPermissing(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertPermissing(int index, ValuePermissingAsyncStage<TBuffer, TValue, TMember>.GetCancellableMemberDelegate getter)
        {
            var stage = new ValuePermissingAsyncStage<TBuffer, TValue, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, ValuePermissingAsyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependPermissing(ValuePermissingAsyncStage<TBuffer, TValue, TMember>.GetCancellableMemberDelegate getter)
        {
            return InsertPermissing(0, getter);
        }

        public TBuilder AppendPermissing(ValuePermissingAsyncStage<TBuffer, TValue, TMember>.GetCancellableMemberDelegate getter)
        {
            return InsertPermissing(Adapters.Count, getter);
        }

        #endregion

        #region Modifies

        protected virtual TBuilder InsertModify(int index, ValueModifyAsyncStage<TBuffer, TValue, TMember>.OnModifyAsync notification)
        {
            var stage = new ValueModifyAsyncStage<TBuffer, TValue, TMember>(notification);
            var adapter = new AsyncAdapter<TBuffer, ValueModifyAsyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependModify(ValueModifyAsyncStage<TBuffer, TValue, TMember>.OnModifyAsync notification)
        {
            return InsertModify(0, notification);
        }

        public TBuilder AppendModify(ValueModifyAsyncStage<TBuffer, TValue, TMember>.OnModifyAsync notification)
        {
            return InsertModify(Adapters.Count, notification);
        }

        protected virtual TBuilder InsertModify(int index, ValueModifyAsyncStage<TBuffer, TValue, TMember>.OnTokenableModifyAsync notification)
        {
            var stage = new ValueModifyAsyncStage<TBuffer, TValue, TMember>(notification);
            var adapter = new AsyncAdapter<TBuffer, ValueModifyAsyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependModify(ValueModifyAsyncStage<TBuffer, TValue, TMember>.OnTokenableModifyAsync notification)
        {
            return InsertModify(0, notification);
        }

        public TBuilder AppendModify(ValueModifyAsyncStage<TBuffer, TValue, TMember>.OnTokenableModifyAsync notification)
        {
            return InsertModify(Adapters.Count, notification);
        }

        protected virtual TBuilder InsertModify(int index, ValueModifyAsyncStage<TBuffer, TValue, TMember>.GetContinuousMemberDelegate getter)
        {
            var stage = new ValueModifyAsyncStage<TBuffer, TValue, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, ValueModifyAsyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependModify(ValueModifyAsyncStage<TBuffer, TValue, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertModify(0, getter);
        }

        public TBuilder AppendModify(ValueModifyAsyncStage<TBuffer, TValue, TMember>.GetContinuousMemberDelegate getter)
        {
            return InsertModify(Adapters.Count, getter);
        }

        protected virtual TBuilder InsertModify(int index, ValueModifyAsyncStage<TBuffer, TValue, TMember>.GetCancellableMemberDelegate getter)
        {
            var stage = new ValueModifyAsyncStage<TBuffer, TValue, TMember>(getter);
            var adapter = new AsyncAdapter<TBuffer, ValueModifyAsyncStage<TBuffer, TValue, TMember>, TMember>(stage);
            Adapters.Insert(index, adapter);

            return (TBuilder)this;
        }

        public TBuilder PrependModify(ValueModifyAsyncStage<TBuffer, TValue, TMember>.GetCancellableMemberDelegate getter)
        {
            return InsertModify(0, getter);
        }

        public TBuilder AppendModify(ValueModifyAsyncStage<TBuffer, TValue, TMember>.GetCancellableMemberDelegate getter)
        {
            return InsertModify(Adapters.Count, getter);
        }

        #endregion
    }

    public abstract class ValueAsyncOperationBuilder<TBuilder, TValue, TMember> : ValueAsyncOperationBuilder<TBuilder, ValueAsyncOperation<TValue, TMember>, ValueAsyncBuffer<TValue, TMember>, TValue, TMember>
        where TBuilder : ValueAsyncOperationBuilder<TBuilder, TValue, TMember>, new()
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    public class ValueAsyncOperationBuilder<TValue, TMember> : ValueAsyncOperationBuilder<ValueAsyncOperationBuilder<TValue, TMember>, TValue, TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
    }

    public class ValueAsyncOperationBuilder<TValue> : ValueAsyncOperationBuilder<ValueAsyncOperationBuilder<TValue>, ValueAsyncOperation<TValue>, ValueAsyncBuffer<TValue, IOperationMember>, TValue, IOperationMember>
        where TValue : struct
    {
    }
}