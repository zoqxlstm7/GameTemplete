using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GameTemplete
{
    public class MoveState : State<EnemyAgent>
    {
        #region Variables
        private int MoveHash = 0;

        private Animator animator = null;
        private NavMeshAgent agent = null;
        #endregion Variables

        public override void OnInit()
        {
            animator = owner.GetComponent<Animator>();
            agent = owner.GetComponent<NavMeshAgent>();

            MoveHash = Animator.StringToHash(AnimatorKeys.Move);
        }

        public override void OnEnter()
        {
            agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
            animator.SetBool(MoveHash, true);
        }

        public override void OnUpdate(float deltaTime)
        {
            Transform target = owner.SearchEnemy();

            if (target != null)
            {
                agent?.SetDestination(target.position);
            }

            if (!target || agent.remainingDistance < agent.stoppingDistance || owner.IsAvailableAttack)
            {
                stateMachine.ChangeState<IdleState>();
            }
        }

        public override void OnExit()
        {
            animator.SetBool(MoveHash, false);
            agent?.ResetPath();
        }
    }
}
