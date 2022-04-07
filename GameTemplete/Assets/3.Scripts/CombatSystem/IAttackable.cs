using UnityEngine;

namespace GameTemplete
{
    public interface IAttackable
    {
        // 공격 행동 중 우선순위나 쿨타임에 의해 선택된 공격 행동
        AttackBehaviour CurrentAttackBehaviour { get; }

        /// <summary>
        /// 애니메이터에서 공격 트리거가 발동할 때
        /// attackIndex를 통해 공격 애니메이션을 결정
        /// </summary>
        void OnExecuteAttack(int attackIndex);
    }
}
