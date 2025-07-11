﻿using System.Collections.Generic;
using System.Text;
using Better.Commons.Runtime.Extensions;
using Better.Operations.Runtime.Members;

namespace Better.Operations.Runtime.Buffers
{
    public class ValueSyncBuffer<TValue, TMember> : SyncBuffer<TMember>
        where TValue : struct
        where TMember : IOperationMember
    {
        public TValue SourceValue { get; }
        public TValue ModifiedValue { get; set; }

        public ValueSyncBuffer(IEnumerable<TMember> members, TValue source) : base(members)
        {
            SourceValue = source;
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

        public override void CollectInfo(ref StringBuilder stringBuilder)
        {
            base.CollectInfo(ref stringBuilder);

            stringBuilder.AppendFieldLine(nameof(SourceValue), SourceValue);
            stringBuilder.AppendFieldLine(nameof(ModifiedValue), ModifiedValue);
        }
    }
}