using System;

namespace Better.Operations.Runtime.Instructions
{
    [Flags]
    public enum ExecuteInstruction
    {
        Proceed = 1,
        PermissionDenied = 2,
        Cancelled = 4,
        Always = Proceed | PermissionDenied | Cancelled,
    }
}