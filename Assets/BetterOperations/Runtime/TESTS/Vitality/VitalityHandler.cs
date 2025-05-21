using System;
using Better.Operations.Runtime;
using Better.Operations.Runtime.Builders;
using Better.Operations.Runtime.Extensions;
using Better.Operations.Runtime.Permissions;
using Tests.Operations;
using UnityEngine;

namespace Tests
{
    [Serializable]
    public class VitalityHandler
    {
        public const float MinPoints = 0f;

        public event Action<VitalityHandler, VitalityTransactionInfo> TransactionApplied;
        public event Action<VitalityHandler, VitalityTransactionInfo> TransactionFailed;

        [SerializeField] private float _currentPoints;

        [Min(MinPoints)]
        [SerializeField] private float _maxPoints;

        private ValueSyncOperation<VitalityTransactionInfo, IApplyingVitalityModifier> _applyingOperation;

        public float CurrentPoints => _currentPoints;
        public float MaxPoints => _maxPoints;
        public bool IsCapable => CurrentPoints > MinPoints;
        public bool IsFilledToMax => Mathf.Approximately(CurrentPoints, MaxPoints);

        #region Initialization

        public void Initialize()
        {
            _currentPoints = MaxPoints;
            InitializeApplyingOperation();
        }

        private void InitializeApplyingOperation()
        {
            _applyingOperation = ValueSyncOperationBuilder<VitalityTransactionInfo, IApplyingVitalityModifier>.Create()
                .AppendModify(member => member.ModifyApplying)
                .AppendPermissing(GetApplyingPermission)
                .AppendPermissing(member => member.GetApplyingPermission)
                .AppendFallback(member => member.OnApplyingFailed)
                .AppendFallback(OnFailedTransaction)
                .AppendNotification(OnApplyTransaction)
                .AppendNotification(member => member.OnApplyingCompleted)
                .AppendNotification(OnAppliedTransaction)
                .Build();
        }

        #endregion

        #region Modifiers

        public void Register(Modifier modifier)
        {
            var message = $"Vitality.{nameof(Register)}; {nameof(modifier)}: {modifier.name};";
            Debug.Log(message);

            _applyingOperation.TryRegister(modifier);
        }

        public void Unregister(Modifier modifier)
        {
            var message = $"Vitality.{nameof(Unregister)}; {nameof(modifier)}: {modifier.name};";
            Debug.Log(message);
            
            _applyingOperation.TryUnregister(modifier);
        }

        #endregion

        public bool ApplyTransaction(VitalityTransactionInfo transactionInfo)
        {
            var applyingResult = _applyingOperation.Execute(transactionInfo);
            return applyingResult.PermissionFlag.IsAllow();
        }

        private PermissionFlag GetApplyingPermission(VitalityTransactionInfo source, VitalityTransactionInfo modified)
        {
            if (!modified.IsValid)
            {
                return PermissionFlag.Create(PermissionValues.MinDeny);
            }

            if (modified.IsIncreasing && IsFilledToMax)
            {
                return PermissionFlag.Create(PermissionValues.MinDeny);
            }

            if (modified.IsDecreasing && !IsCapable)
            {
                return PermissionFlag.Create(PermissionValues.MinDeny);
            }

            return default;
        }

        private void OnApplyTransaction(VitalityTransactionInfo source, VitalityTransactionInfo modified)
        {
            var modifiedPoints = CurrentPoints + modified.Amount;
            modifiedPoints = Math.Clamp(modifiedPoints, MinPoints, MaxPoints);

            _currentPoints = modifiedPoints;
        }

        private void OnAppliedTransaction(VitalityTransactionInfo source, VitalityTransactionInfo modified)
        {
            TransactionApplied?.Invoke(this, modified);
        }

        private void OnFailedTransaction(PermissionFlag permission, VitalityTransactionInfo source, VitalityTransactionInfo modified)
        {
            TransactionFailed?.Invoke(this, modified);
        }
    }
}