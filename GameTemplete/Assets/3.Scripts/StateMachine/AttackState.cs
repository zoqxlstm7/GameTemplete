using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GameTemplete
{
    public class AttackState : State<EnemyAgent>
    {
        #region Variables
        private int AttackTriggerHash = 0;
        private int AttackIndexHash = 0;

        private Animator animator = null;
        private NavMeshAgent agent = null;

        private AttackStateController attackStateController = null;
        private IAttackable attackable = null;
        #endregion Variables

        public override void OnInit()
        {
            animator = owner.GetComponent<Animator>();
            agent = owner.GetComponent<NavMeshAgent>();

            attackStateController = owner.GetComponent<AttackStateController>();
            attackable = owner.GetComponent<IAttackable>();

            AttackTriggerHash = Animator.StringToHash(AnimatorKeys.AttackTrigger);
            AttackIndexHash = Animator.StringToHash(AnimatorKeys.AttackIndex);
        }

        public override void OnEnter()
        {
            agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

            if (attackable == null || attackable.CurrentAttackBehaviour == null)
            {
                stateMachine.ChangeState<IdleState>();
                return;
            }

            animator.SetInteger(AttackIndexHash, attackable.CurrentAttackBehaviour.animationIndex);
            animator.SetTrigger(AttackTriggerHash);
        }

        public override void OnUpdate(float deltaTime)
        {

        }

        public override void OnExit()
        {
        }
    }
}
