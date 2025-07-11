﻿using System.Collections.Generic;
using System.Text;
using Better.Commons.Runtime.Extensions;
using Better.Operations.Runtime.Members;
using Better.Operations.Runtime.Permissions;

namespace Better.Operations.Runtime.Buffers
{
    public class SyncBuffer<TMember> : MemberedBuffer<TMember>
        where TMember : IOperationMember
    {
        public PermissionFlag PermissionFlag { get; private set; }

        public SyncBuffer(IEnumerable<TMember> members) : base(members)
        {
        }

        public virtual bool SetPermissionFlag(PermissionFlag permissionFlag)
        {
            PermissionFlag = PermissionFlag.MostSignificant(PermissionFlag, permissionFlag);

            var changed = PermissionFlag == permissionFlag;
            return changed;
        }

        public override void CollectInfo(ref StringBuilder stringBuilder)
        {
            base.CollectInfo(ref stringBuilder);

            stringBuilder.AppendFieldLine(nameof(PermissionFlag), PermissionFlag);
        }
    }
}