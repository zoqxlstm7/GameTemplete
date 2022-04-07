using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplete
{
    public class IdleState : State<EnemyAgent>
    {
        #region Variables
        private int MoveHash = 0;

        private Animator animator = null;
        #endregion Variables

        public override void OnInit()
        {
            animator = owner.GetComponent<Animator>();

            MoveHash = Animator.StringToHash(AnimatorKeys.Move);
        }

        public override void OnEnter()
        {
            animator.SetBool(MoveHash, false);
        }

        public override void OnUpdate(float deltaTime)
        {
            Transform target = owner.SearchEnemy();

            if (target != null)
            {
                if (owner.IsAvailableAttack)
                {
                    owner.FaceToTarget(target);
                    stateMachine.ChangeState<AttackState>();
                }
                else
                {
                    stateMachine.ChangeState<MoveState>();
                }
            }
        }
    }
}
