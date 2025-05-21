using Better.Operations.Runtime.Permissions;
using Tests.Operations;

namespace Tests
{
    public class UncapacitableModifier : Modifier, IIncapacitingVitalityModifier
    {
        public PermissionFlag GetIncapacitatingPermission()
        {
            var permissionFlag = PermissionFlag.Create(PermissionValues.MaxDeny);
            return permissionFlag;
        }

        public void OnIncapacitatingCompleted()
        {
        }

        public void OnIncapacitatingFailed(PermissionFlag permission)
        {
        }
    }
}