using System;
using UnityEngine;

namespace Tests
{
    [Serializable]
    public struct VitalityTransactionInfo
    {
        [SerializeField] private float _amount;
        [SerializeField] private VitalityType _type;

        public float Amount => _amount;
        public VitalityType Type => _type;

        public bool IsIncreasing => Amount > 0f;
        public bool IsDecreasing => Amount < 0f;
        public bool IsValid => Amount != 0f;

        public VitalityTransactionInfo(float amount, VitalityType type)
        {
            _amount = amount;
            _type = type;
        }

        public override string ToString()
        {
            var info = $"{nameof(Amount)}: {Amount}, {nameof(Type)}: {Type}";
            return info;
        }
    }
}