using System;
using UnityEngine;

namespace Tests
{
    [Serializable]
    public class VitalityHandler
    {
        public const float MinPoints = 0f;
        public const float MinChangingAmount = float.Epsilon;

        public event Action<VitalityHandler> CurrentPointsDecreased;
        public event Action<VitalityHandler> CurrentPointsChanged;

        [Min(MinPoints)]
        [SerializeField] float m_MaxPoints;

        public float CurrentPoints { get; private set; }
        public float MaxPoints => m_MaxPoints;
        public bool IsCapable => CurrentPoints > MinPoints;
        public bool IsFilledToMax => Mathf.Approximately(CurrentPoints, MaxPoints);

        public void Initialize()
        {
            CurrentPoints = MaxPoints;
        }

        // TODO: Operation
        bool ChangeCurrentPointsOperation(float value)
        {
            OnChangeCurrentPoints(value); // TODO: Operation Self-Notify
            // TODO: Operation Mod-Notify
            OnCurrentPointsChanged(value); // TODO: Operation Self-Notify

            return true; // TODO: Operation Result
        }

        // TODO: Operation
        bool AllowDecreasePointsOperation(float amount)
        {
            // TODO: Operation Mod-Allowed
            var result = IsAllowedDecreasePoints(amount); // TODO: Operation Self-Allowed

            return result; // TODO: Operation Result
        }

        // TODO: Operation
        bool DecreasePointsOperation(float amount)
        {
            if (!AllowDecreasePointsOperation(amount))
            {
                // TODO: Operation SubOperation Result
                return false;
            }

            // TODO: Operation Mod-Modify

            if (!AllowDecreasePointsOperation(amount))
            {
                // TODO: Operation SubOperation Result
                return false;
            }

            // TODO: Operation Sub-Operation with DataTransformation
            var points = CurrentPoints - amount;
            ChangeCurrentPointsOperation(points);

            // TODO: Operation Mod-Notify
            OnCurrentPointsDecreased(amount); // TODO: Operation Self-Notify

            return true; // TODO: Operation Result
        }
        
        public bool CanDecreaseCurrentPoints()
        {
            return AllowDecreasePointsOperation(MinChangingAmount);
        }

        public bool DecreaseCurrentPoints(float amount)
        {
            return DecreasePointsOperation(amount);
        }

        bool IsAllowedDecreasePoints(float amount)
        {
            var isAllowed = IsCapable && amount >= MinChangingAmount;
            return isAllowed;
        }

        void OnCurrentPointsDecreased(float amount)
        {
            CurrentPointsIncreased?.Invoke(this);

            xxxxxxx
            if (!IsCapable)
            {
                Incapacitate();
            }
        }

        void OnChangeCurrentPoints(float value)
        {
            value = Math.Clamp(value, MinPoints, MaxPoints);
            if (Mathf.Approximately(CurrentPoints, value))
            {
                return;
            }

            CurrentPoints = value;
        }

        void OnCurrentPointsChanged(float value)
        {
            CurrentPointsChanged?.Invoke(this);
        }
    }
}