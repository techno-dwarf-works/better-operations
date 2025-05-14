using System;
using UnityEngine;

namespace Tests
{
    public class TesterBehaviour : MonoBehaviour
    {
        [SerializeField] private VitalityHandler _vitalityHandler;
        [SerializeField] private float _damageValue;

        private void Awake()
        {
            _vitalityHandler.Initialize();
            _vitalityHandler.CurrentPointsChanged += OnCurrentPointsChanged;
            _vitalityHandler.CurrentPointsDecreased += OnCurrentPointsDecreased;
        }

        private void OnCurrentPointsChanged(VitalityHandler vitalityHandler)
        {
            var message = $"Tester.{nameof(OnCurrentPointsChanged)}: {vitalityHandler.CurrentPoints}";
            Debug.Log(message);
        }

        private void OnCurrentPointsDecreased(VitalityHandler vitalityHandler)
        {
            var message = $"Tester.{nameof(OnCurrentPointsDecreased)}: {vitalityHandler.CurrentPoints}";
            Debug.Log(message);
        }

        [ContextMenu(nameof(ApplyDamage))]
        private void ApplyDamage()
        {
            _vitalityHandler.DecreaseCurrentPoints(_damageValue);
        }

        // [ContextMenu(nameof(ApplyHeal))]
        // private void ApplyHeal()
        // {
        //     _vitalityHandler.Increase
        // }

        private void OnDestroy()
        {
            _vitalityHandler.CurrentPointsChanged -= OnCurrentPointsChanged;
            _vitalityHandler.CurrentPointsDecreased -= OnCurrentPointsDecreased;
        }
    }
}