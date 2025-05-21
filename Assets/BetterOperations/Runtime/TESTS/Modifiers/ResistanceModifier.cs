using Better.Operations.Runtime.Permissions;
using Tests.Operations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tests
{
    public class ResistanceModifier : Modifier, IApplyingVitalityModifier
    {
        [SerializeField] private VitalityType _vitalityType;

        [Range(0f, 1f)]
        [SerializeField] private float _resistanceFactor;

        public VitalityTransactionInfo ModifyApplying(VitalityTransactionInfo source, VitalityTransactionInfo modified)
        {
            if (modified.Type == _vitalityType)
            {
                var resistanceMode = 1f - _resistanceFactor;
                var resistanedAmount = modified.Amount * resistanceMode;
                modified = new VitalityTransactionInfo(resistanedAmount, modified.Type);
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