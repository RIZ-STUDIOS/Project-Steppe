using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectSteppe
{
    public class CurrencyIncomingDummy : MonoBehaviour
    {
        public UnityEvent OnUpdateText;

        public void UpdateText()
        {
            OnUpdateText.Invoke();
        }
    }
}
