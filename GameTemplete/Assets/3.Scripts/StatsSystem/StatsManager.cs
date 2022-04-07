using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplete
{
    public class StatsManager : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private Stat[] stats = null;

        public int level = 1;

        public float health = 0.0f;
        public float mana = 0.0f;

        public event System.Action OnChangedStats = null;
        #endregion Variables

        #region Property
        public float HealthPercentage
        {
            get
            {
                float health = this.health;
                float maxHealth = GetValue(StatAttribute.Health);

                return (maxHealth > 0 ? (health / maxHealth) : 0);
            }
        }

        public float ManaPercentage
        {
            get
            {
                float mana = this.mana;
                float maxMana = GetValue(StatAttribute.Mana);

                return (maxMana > 0 ? (mana / maxMana) : 0);
            }
        }
        #endregion Property

        #region Unity Methods
        private void OnEnable() => InitializeStats();
        #endregion Unity Methods

        #region Main Methods
        protected virtual void InitializeStats()
        {
            SetBaseValue(StatAttribute.Health, GetBaseValue(StatAttribute.Health));
            SetBaseValue(StatAttribute.Mana, GetBaseValue(StatAttribute.Mana));

            SetBaseValue(StatAttribute.AttackPower, GetBaseValue(StatAttribute.AttackPower));
            SetBaseValue(StatAttribute.DefensivePower, GetBaseValue(StatAttribute.DefensivePower));
            SetBaseValue(StatAttribute.Critical, GetBaseValue(StatAttribute.Critical));
            SetBaseValue(StatAttribute.CriticalDamage, GetBaseValue(StatAttribute.CriticalDamage));

            SetBaseValue(StatAttribute.MoveSpeed, GetBaseValue(StatAttribute.MoveSpeed));
            SetBaseValue(StatAttribute.AttackSpeed, GetBaseValue(StatAttribute.AttackSpeed));

            ResetValue();
        }

        public void ResetValue()
        {
            health = GetValue(StatAttribute.Health);
            mana = GetValue(StatAttribute.Mana);

            OnChangedStats?.Invoke();
        }

        public void SetBaseValue(StatAttribute type, float value)
        {
            foreach (Stat stat in stats)
            {
                if (stat.type == type)
                {
                    stat.value.baseValue = value;
                    return;
                }
            }
        }

        public float GetBaseValue(StatAttribute type)
        {
            foreach (Stat stat in stats)
            {
                if (stat.type == type)
                {
                    return stat.value.baseValue;
                }
            }

            return -1;
        }

        public void AddModifierValue(StatAttribute type, float value)
        {
            foreach (Stat stat in stats)
            {
                if (stat.type == type)
                {
                    stat.value.AddModifier(value);
                    return;
                }
            }
        }

        public void RemoveModifiedValue(StatAttribute type, float value)
        {
            foreach (Stat stat in stats)
            {
                if (stat.type == type)
                {
                    stat.value.RemoveModifier(value);
                }
            }
        }

        public float GetValue(StatAttribute type)
        {
            foreach (Stat stat in stats)
            {
                if (stat.type == type)
                {
                    return stat.value.GetValue();
                }
            }

            return -1;
        }

        public float AddHealth(float value)
        {
            // 사망시 리턴
            if (health <= 0)
                return 0;

            health += value;

            if (health < 0)
            {
                health = 0;
            }
            else if (health > GetValue(StatAttribute.Health))
            {
                health = GetValue(StatAttribute.Health);
            }

            OnChangedStats?.Invoke();

            return health;
        }

        public float AddMana(float value)
        {
            mana += value;

            if (mana < 0)
            {
                mana = 0;
            }
            else if (mana > GetValue(StatAttribute.Mana))
            {
                mana = GetValue(StatAttribute.Mana);
            }

            OnChangedStats?.Invoke();

            return mana;
        }
        #endregion Main Methods
    }
}
