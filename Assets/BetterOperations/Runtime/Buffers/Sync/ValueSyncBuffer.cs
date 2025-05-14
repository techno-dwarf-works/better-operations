using System.Collections.Generic;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Buffers
{
    public class ValueSyncBuffer<TValue, TMember> : SyncBuffer<TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        public TValue SourceValue { get; }
        public TValue ModifiedValue { get; set; }

        public ValueSyncBuffer(IEnumerable<TMember> members, TValue sourceValue) : base(members)
        {
            SourceValue = sourceValue;
            ResetModifiedValue();
        }

        public void ResetModifiedValue()
        {
            ModifiedValue = SourceValue;
            OnModifiedValueWasReset();
        }

        protected virtual void OnModifiedValueWasReset()
        {
        }
    }
}