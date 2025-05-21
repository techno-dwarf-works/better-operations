using System;
using UnityEngine;

namespace Tests
{
    public class TesterBehaviour : MonoBehaviour
    {
        [SerializeField] private VitalityHandler _vitalityHandler;
        [SerializeField] private VitalityTransactionInfo _transactionInfo;

        [Header("FAKE BUTTON")]
        [SerializeField] private bool _apply;
        
        private void Awake()
        {
            _vitalityHandler.Initialize();
            _vitalityHandler.TransactionApplied += OnTransactionApplied;
            _vitalityHandler.TransactionFailed += OnTransactionFailed;
            _vitalityHandler.Incapacitated += OnIncapacitated;
        }

        private void Update()
        {
            if (_apply)
            {
                _apply = false;
                ApplyDamage();
            }
        }

        public void Register(Modifier modifier)
        {
            _vitalityHandler.Register(modifier);
        }

        public void Unregister(Modifier modifier)
        {
            _vitalityHandler.Unregister(modifier);
        }

        private void OnTransactionApplied(VitalityHandler vitalityHandler, VitalityTransactionInfo transactionInfo)
        {
            var message = $"Tester.{nameof(OnTransactionApplied)}; {nameof(vitalityHandler.CurrentPoints)}: {vitalityHandler.CurrentPoints}; {nameof(transactionInfo)} = {transactionInfo}";

            Debug.Log(message);
        }

        private void OnTransactionFailed(VitalityHandler vitalityHandler, VitalityTransactionInfo transactionInfo)
        {
            var message = $"Tester.{nameof(OnTransactionFailed)}; {nameof(vitalityHandler.CurrentPoints)}: {vitalityHandler.CurrentPoints}; {nameof(transactionInfo)} = {transactionInfo}";

            Debug.Log(message);
        }

        private void OnIncapacitated(VitalityHandler vitalityHandler)
        {
            var message = $"Tester.{nameof(OnIncapacitated)}; {nameof(vitalityHandler.CurrentPoints)}: {vitalityHandler.CurrentPoints};";

            Debug.Log(message);
        }

        private void ApplyDamage()
        {
            var startedMessage = $"Tester.{nameof(ApplyDamage)} STARTED; {nameof(_vitalityHandler.CurrentPoints)}: {_vitalityHandler.CurrentPoints}; {nameof(_transactionInfo)} = {_transactionInfo}";
            Debug.Log(startedMessage);

            _vitalityHandler.TryApplyTransaction(_transactionInfo);

            var finishedMessage = $"Tester.{nameof(ApplyDamage)} FINISHED; {nameof(_vitalityHandler.CurrentPoints)}: {_vitalityHandler.CurrentPoints}; {nameof(_transactionInfo)} = {_transactionInfo}";
            Debug.Log(finishedMessage);
        }

        private void OnDestroy()
        {
            _vitalityHandler.TransactionApplied -= OnTransactionApplied;
            _vitalityHandler.TransactionFailed -= OnTransactionFailed;
            _vitalityHandler.Incapacitated -= OnIncapacitated;
        }
    }
}