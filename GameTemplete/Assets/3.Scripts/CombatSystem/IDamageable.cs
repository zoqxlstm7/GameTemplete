using UnityEngine;

namespace GameTemplete
{
    public interface IDamageable
    {
        // 현재 살아있는 상태인지 여부
        bool IsDead { get; }

        /// <summary>
        /// 데미지 처리를 하는 함수
        /// 타격 이펙트 프리팹 파라미터를 활용하여
        /// 스킬 또는 공격마다의 타격 이펙트를 구현해 줄 수도 있다.
        /// </summary>
        void TakeDamage(CombatData data, GameObject hitEffectPrefabs);
    }
}
