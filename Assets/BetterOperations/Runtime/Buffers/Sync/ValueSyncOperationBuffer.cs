using System.Collections.Generic;

namespace Better.Operations.Runtime.Buffers
{
    public class ValueSyncOperationBuffer<TValue, TMember> : SyncOperationBuffer<TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        public TValue SourceValue { get; }
        public TValue ModifiedValue { get; set; }

        public ValueSyncOperationBuffer(IEnumerable<TMember> members, TValue sourceValue) : base(members)
        {
            SourceValue = sourceValue;
            ModifiedValue = sourceValue;
        }
    }
}