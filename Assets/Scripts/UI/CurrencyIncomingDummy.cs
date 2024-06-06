using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectSteppe
{
    public class CurrencyIncomingDummy : MonoBehaviour
    {
        public UnityEvent OnUpdateText;
        public UnityEvent OnDispenseWithFlavour;

        public void UpdateText()
        {
            OnUpdateText.Invoke();
        }

        public void DispenseWithFlavour()
        {
            OnDispenseWithFlavour.Invoke();
        }
    }
}
