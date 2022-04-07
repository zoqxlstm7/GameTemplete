using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplete
{
    public class CombatManager : MonoBehaviour
    {
        #region Variables
        // 타겟 리스트
        private static List<GameObject> targetList = new List<GameObject>();
        #endregion Variables

        #region Main Methods
        public static void TakeDamage(GameObject target, CombatData data)
        {
            if (target != null)
            {
                target.GetComponent<IDamageable>()?.TakeDamage(data, null);
            }
        }

        /// <summary>
        /// 주변범위 적 검색
        /// </summary>
        public static GameObject[] GetAroundEnemyArr(Vector3 position, float radius, LayerMask targetMask)
        {
            Collider[] aroundTargets = Physics.OverlapSphere(position, radius, targetMask);
            targetList.Clear();

            for (int i = 0; i < aroundTargets.Length; i++)
            {
                if (!IsEnemyAttackable(aroundTargets[i].transform))
                    continue;

                targetList.Add(aroundTargets[i].gameObject);
            }

            return targetList.ToArray();
        }

        /// <summary>
        /// 특정각도 범위의 적 검색
        /// </summary>
        public static GameObject[] GetAroundEnemyArrWithAngle(Transform owner, float radius, LayerMask targetMask, float angle)
        {
            GameObject[] aroundTargets = GetAroundEnemyArr(owner.position, radius, targetMask);
            targetList.Clear();

            for (int i = 0; i < aroundTargets.Length; i++)
            {
                Vector3 dir = (aroundTargets[i].transform.position - owner.position).normalized;
                if (Vector3.Dot(owner.forward, dir) > Mathf.Cos(angle * Mathf.Deg2Rad))
                {
                    targetList.Add(aroundTargets[i]);
                }
            }

            return targetList.ToArray();
        }

        /// <summary>
        /// 직선 범위의 적 검색
        /// </summary>
        public static GameObject[] GetLinearEnemyArr(Transform owner, float distance, float radius, LayerMask targetMask)
        {
            Vector3 targetPos = owner.position + owner.forward * distance;
            Collider[] aroundTargets = Physics.OverlapCapsule(owner.position, targetPos, radius, targetMask);
            targetList.Clear();

            for (int i = 0; i < aroundTargets.Length; i++)
            {
                if (!IsEnemyAttackable(aroundTargets[i].transform))
                    continue;
                targetList.Add(aroundTargets[i].gameObject);
            }

            return targetList.ToArray();
        }

        /// <summary>
        /// 공격가능한 적인지 반환
        /// </summary>
        public static bool IsEnemyAttackable(Transform target)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            if (damageable == null || damageable.IsDead)
                return false;

            return true;
        }
        #endregion Main Methods

        #region Helper Methods
        public static float GetHitDamageWithDef(StatsManager statsManager, float damage)
        {
            float defensiveValue = statsManager.GetValue(StatAttribute.DefensivePower);
            return (1 - defensiveValue / (defensiveValue + 1000)) * damage;
        }
        #endregion Helper Methods
    }
}
