using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectSteppe
{
    public class CurrencyIncomingDummy : MonoBehaviour
    {
        public UnityEvent OnDispenseWithFlavour;

        public void DispenseWithFlavour()
        {
            OnDispenseWithFlavour.Invoke();
        }
    }
}
