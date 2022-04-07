using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GameTemplete
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAgent : Actor
    {
        #region Variables
        private StateMachine<EnemyAgent> stateMachine = null;
        #endregion Variables

        #region Main Methods
        protected override void InitializeActor()
        {
            base.InitializeActor();

            attackStateController.OnExitTrigger = EndAttack;

            InitStateMachine();
        }

        protected override void UpdateActor()
        {
            if (IsDead)
                return;
            if (IsInAttackState)
                return;

            base.UpdateActor();

            stateMachine.Update(Time.deltaTime);
        }

        protected virtual void InitStateMachine()
        {
            stateMachine = new StateMachine<EnemyAgent>(this, new IdleState());
            stateMachine.AddState(new MoveState());
            stateMachine.AddState(new AttackState());
        }

        protected override void EndAttack()
        {
            base.EndAttack();
            stateMachine.ChangeState<IdleState>();
        }
        #endregion Main Methods
    }
}
