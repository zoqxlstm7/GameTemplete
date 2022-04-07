using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GameTemplete
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerAgent : Actor
    {
        #region Variables
        private NavMeshAgent agent = null;
        #endregion Variables

        #region Main Methods
        protected override void InitializeActor()
        {
            base.InitializeActor();

            agent = GetComponent<NavMeshAgent>();

            agent.updatePosition = true;
            agent.updateRotation = true;
        }

        protected override void UpdateActor()
        {
            if (IsDead)
                return;
            if (IsInAttackState)
                return;
            
            base.UpdateActor();

            MovementManagement();
        }

        void MovementManagement()
        {
            if (target != null)
            {
                agent.SetDestination(target.position);

                if ((agent.pathPending || (agent.remainingDistance > agent.stoppingDistance)) && !IsAvailableAttack)
                {
                    agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

                    animator.SetBool(MoveHash, true);
                }
                else
                {
                    animator.SetBool(MoveHash, false);
                    agent.ResetPath();

                    AttackTarget();
                }
            }
            else
            {
                animator.SetBool(MoveHash, false);

                SearchEnemy();
            }
        }

        void AttackTarget()
        {
            if (target != null)
            {
                IDamageable damageable = target.GetComponent<IDamageable>();
                if (damageable == null || damageable.IsDead)
                {
                    RemoveTarget();
                    return;
                }
            }

            if (IsInAttackState)
                return;
            if (CurrentAttackBehaviour == null)
                return;

            FaceToTarget(target);

            if (target != null && CurrentAttackBehaviour.IsAvailable)
            {
                if (IsAvailableAttack)
                {
                    agent.obstacleAvoidanceType = UnityEngine.AI.ObstacleAvoidanceType.NoObstacleAvoidance;

                    animator.SetInteger(AttackIndexHash, CurrentAttackBehaviour.animationIndex);
                    animator.SetTrigger(AttackTriggerHash);
                }
            }
        }
        #endregion Main Methods
    }
}
