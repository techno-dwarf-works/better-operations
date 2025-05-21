using Better.Operations.Runtime.Permissions;
using Tests.Operations;

namespace Tests
{
    public class ClampDamageModifier : Modifier, IApplyingVitalityModifier
    {
        public VitalityTransactionInfo ModifyApplying(VitalityTransactionInfo source, VitalityTransactionInfo modified)
        {
            if (modified.IsDecreasing)
            {
                modified = new VitalityTransactionInfo(-1, modified.Type);
            }

            return modified;
        }

        public PermissionFlag GetApplyingPermission(VitalityTransactionInfo source, VitalityTransactionInfo modified)
        {
            return default;
        }

        public void OnApplyingCompleted(VitalityTransactionInfo source, VitalityTransactionInfo modified)
        {
        }

        public void OnApplyingFailed(PermissionFlag permission, VitalityTransactionInfo source, VitalityTransactionInfo modified)
        {
        }
    }
}