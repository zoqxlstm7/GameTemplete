using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplete
{
    [System.Serializable]
    public class ModifierFloat
    {
        #region Variables
        public float baseValue;
        public float finalValue;

        public List<float> modifiers = new List<float>();
        #endregion Variables

        #region Main Methods
        public ModifierFloat(float baseValue)
        {
            this.baseValue = baseValue;
        }

        public float GetValue()
        {
            UpdateValue();

            return finalValue;
        }

        public void AddModifier(float modifier)
        {
            if (modifier == 0)
                return;

            modifiers.Add(modifier);
            UpdateValue();
        }

        public void RemoveModifier(float modifier)
        {
            if (modifier == 0)
                return;

            modifiers.Remove(modifier);
            UpdateValue();
        }

        void UpdateValue()
        {
            finalValue = baseValue;
            modifiers.ForEach(x => finalValue += x);
        }
        #endregion Main Methods
    }
}
