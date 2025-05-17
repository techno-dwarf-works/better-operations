using System;

namespace Better.Operations.Runtime.Instructions
{
    [Flags]
    public enum ExecuteInstruction
    {
        // TODO: Naming

        WhenAllGood = 1,
        WhenUnpermissed = 2,
        WhenCancelled = 4,
        Any = WhenAllGood | WhenUnpermissed | WhenCancelled,
    }
}