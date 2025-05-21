using UnityEngine;

namespace Tests
{
    public abstract class Modifier : MonoBehaviour, IModifier
    {
        private void OnEnable()
        {
            FindObjectOfType<TesterBehaviour>().Register(this);
        }

        private void OnDisable()
        {
            FindObjectOfType<TesterBehaviour>().Unregister(this);
        }
    }
}