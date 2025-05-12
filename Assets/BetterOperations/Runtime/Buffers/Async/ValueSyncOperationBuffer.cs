using System.Collections.Generic;

namespace Better.Operations.Runtime.Buffers
{
    public class ValueAsyncOperationBuffer<TValue, TMember> : AsyncOperationBuffer<TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        public TValue SourceValue { get; }
        public TValue ModifiedValue { get; set; }

        public ValueAsyncOperationBuffer(IEnumerable<TMember> members, TValue sourceValue) : base(members)
        {
            SourceValue = sourceValue;
            ModifiedValue = sourceValue;
        }
    }
}