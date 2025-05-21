using Better.Operations.Runtime.Permissions;

namespace Tests.Operations
{
    public interface IApplyingVitalityModifier : IModifier
    {
        VitalityTransactionInfo ModifyApplying(VitalityTransactionInfo source, VitalityTransactionInfo modified);
        PermissionFlag GetApplyingPermission(VitalityTransactionInfo source, VitalityTransactionInfo modified);
        void OnApplyingCompleted(VitalityTransactionInfo source, VitalityTransactionInfo modified);
        void OnApplyingFailed(PermissionFlag permission, VitalityTransactionInfo source, VitalityTransactionInfo modified);
    }
}