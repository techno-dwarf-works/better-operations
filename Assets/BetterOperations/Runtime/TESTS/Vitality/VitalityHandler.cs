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
        public event Action<VitalityHandler> Incapacitated;

        [SerializeField] private float _currentPoints;

        [Min(MinPoints)]
        [SerializeField] private float _maxPoints;

        private ValueSyncOperation<VitalityTransactionInfo, IApplyingVitalityModifier> _applyingOperation;
        private SyncOperation<IIncapacitingVitalityModifier> _incapacitateOperation;

        public float CurrentPoints => _currentPoints;
        public float MaxPoints => _maxPoints;
        public bool IsCapable => CurrentPoints > MinPoints;
        public bool IsFilledToMax => Mathf.Approximately(CurrentPoints, MaxPoints);

        #region Initialization

        public void Initialize()
        {
            _currentPoints = MaxPoints;
            InitializeApplyingOperation();
            InitializeIncapacitatingOperation();
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

        private void InitializeIncapacitatingOperation()
        {
            _incapacitateOperation = SyncOperationBuilder<IIncapacitingVitalityModifier>.Create()
                .AppendPermissing(GetIncapacitatingPermission)
                .AppendPermissing(member => member.GetIncapacitatingPermission)
                .AppendFallback(member => member.OnIncapacitatingFailed)
                .AppendNotification(OnIncapacitate)
                .AppendNotification(member => member.OnIncapacitatingCompleted)
                .AppendNotification(OnIncapacitated)
                .Build();
        }

        #endregion

        #region Modifiers

        public void Register(Modifier modifier)
        {
            var message = $"Vitality.{nameof(Register)}; {nameof(modifier)}: {modifier.name};";
            Debug.Log(message);

            _applyingOperation.TryRegister(modifier);
            _incapacitateOperation.TryRegister(modifier);
        }

        public void Unregister(Modifier modifier)
        {
            var message = $"Vitality.{nameof(Unregister)}; {nameof(modifier)}: {modifier.name};";
            Debug.Log(message);

            _applyingOperation.TryUnregister(modifier);
            _incapacitateOperation.TryUnregister(modifier);
        }

        #endregion

        public bool TryApplyTransaction(VitalityTransactionInfo transactionInfo)
        {
            var applyingResult = _applyingOperation.Execute(transactionInfo);
            return applyingResult.PermissionFlag.IsAllow();
        }

        public bool TryIncapacitate()
        {
            var incapacitateResult = _incapacitateOperation.Execute();
            return incapacitateResult.PermissionFlag.IsAllow();
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
            TryIncapacitate();
        }

        private void OnFailedTransaction(PermissionFlag permission, VitalityTransactionInfo source, VitalityTransactionInfo modified)
        {
            TransactionFailed?.Invoke(this, modified);
        }

        private PermissionFlag GetIncapacitatingPermission()
        {
            if (IsCapable)
            {
                var permissionFlag = PermissionFlag.Create(PermissionValues.MaxDeny);
                return permissionFlag;
            }

            return default;
        }

        private void OnIncapacitate()
        {
            _currentPoints = MinPoints;
        }

        private void OnIncapacitated()
        {
            Incapacitated?.Invoke(this);
        }
    }
}