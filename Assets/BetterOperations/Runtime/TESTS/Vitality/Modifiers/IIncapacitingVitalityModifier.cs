using Better.Operations.Runtime.Permissions;

namespace Tests.Operations
{
    public interface IIncapacitingVitalityModifier : IModifier
    {
        PermissionFlag GetIncapacitatingPermission();
        void OnIncapacitatingCompleted();
        void OnIncapacitatingFailed(PermissionFlag permission);
    }
}