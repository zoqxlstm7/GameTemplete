using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplete
{
    public class AttackStateController : MonoBehaviour
    {
        #region delegate Handler
        public delegate void OnEnterAttackState();
        public delegate void OnExitAttackState();

        // 애니메이터의 공격 상태에 진입, 탈출했을 때의 핸들러
        public OnEnterAttackState enterAttackStateHandler;
        public OnExitAttackState exitAttackStateHandler;

        private IAttackable attackable = null;

        public System.Action OnExitTrigger = null;
        #endregion delegate Handler

        #region Property
        // 애니메이터에서 현재 공격 상태에 있는지 검사
        public bool IsInAttackState
        {
            get;
            private set;
        }
        #endregion Property

        #region Unity Methods
        private void Start()
        {
            InitializeController();
        }
        #endregion Unity Methods

        #region Main Methods
        protected virtual void InitializeController()
        {
            attackable = GetComponent<IAttackable>();

            // 핸들러 함수 등록
            enterAttackStateHandler = new OnEnterAttackState(EnterAttackState);
            exitAttackStateHandler = new OnExitAttackState(ExitAttackState);
        }

        public void ResetAttackState()
        {
            IsInAttackState = false;
        }

        /// <summary>
        /// 애니메이터에서 공격 상태에 들어갔을 때 호출되는 함수
        /// </summary>
        public void OnStartOfAttackState()
        {
            IsInAttackState = true;
            enterAttackStateHandler();
        }

        /// <summary>
        /// 애니메이터에서 공격 상태가 끝났을 때 호출되는 함수
        /// </summary>
        public void OnEndOfAttackState()
        {
            IsInAttackState = false;
            exitAttackStateHandler();
        }

        /// <summary>
        /// 공격 상태가 시작될 때 호출되는 함수
        /// </summary>
        protected virtual void EnterAttackState()
        {

        }

        /// <summary>
        /// 공격 상태에서 빠져나올 때 호출되는 함수
        /// </summary>
        protected virtual void ExitAttackState()
        {
            OnExitTrigger?.Invoke();
        }

        /// <summary>
        /// 애니메이터 이벤트에서 호출되는 함수
        /// </summary>
        /// <param name="attackIndex"></param>
        public void OnCheckAttackCollider(int attackIndex)
        {
            // 공격이 가능한 오브젝트인지 검사
            // 공격 애니메이션 도중 어떠한 형태로 적을 검출해서 데미지를 가할지 정함
            attackable?.OnExecuteAttack(attackIndex);
        }
        #endregion Main Methods
    }
}
