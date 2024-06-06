using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Currencies
{
    [System.Serializable]
    public class GameCurrency
    {
        public CurrencyType type;

        [SerializeField]
        private int _amount;
        public int Amount
        {
            get {  return _amount; }
            set
            {
                _amount = value;
                if (value < 0 && !canBeNegative) value = 0;
            }
        }

        private bool canBeNegative = false;

        public GameCurrency(CurrencyType type, int amount = 0, bool canBeNegative = false)
        {
            this.type = type;
            _amount = amount;
            this.canBeNegative = false;
        }
    }

    public enum CurrencyType
    {
        Experience,
        Blessing
    }
}
