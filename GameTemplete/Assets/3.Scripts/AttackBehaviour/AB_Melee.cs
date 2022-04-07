using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplete
{
    public class AB_Melee : AttackBehaviour
    {
        #region Variables
        public ManualCollision manualCollision = null;
        #endregion Variables

        #region Main Methods
        public override void ExecuteAttack(GameObject target = null, Transform fireTransform = null)
        {
            Collider[] colliders = manualCollision.CheckOverlapBox(targetMask);
            if (colliders != null)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    CombatData combatData = new CombatData()
                    {
                        attacker = gameObject,
                        damage = Damage,
                        critical = 0.0f,
                        criticalDamage = 0.0f,
                    };

                    CombatManager.TakeDamage(colliders[i].gameObject, combatData);
                }
            }

            ResetCoolTime();
        }
        #endregion Main Methods
    }
}
