using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplete 
{
    public class AttackStateMachineBehaviour : StateMachineBehaviour
    {
        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            // 상태 진입시 상태 진입 델리게이트 이벤트 함수 호출
            animator.GetComponent<AttackStateController>().OnStartOfAttackState();
        }

        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            // 상태 탈출시 상태 탈출 델리게이트 이벤트 함수 호출
            animator.GetComponent<AttackStateController>().OnEndOfAttackState();
        }
    }
}
