using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplete
{
    public abstract class AttackBehaviour : MonoBehaviour
    {
        #region Variables
#if UNITY_EDITOR
        // 어떤 기능인지 주석 정의
        [Multiline]
        public string developmentDescription = "";
#endif

        public int animationIndex = 0;                 // 공격 애니메이션 인덱스

        public int priority = 0;                       // 우선순위

        public float factor = 0;                       // 계수
        public float attackableRange = 2.0f;           // 공격 가능 범위

        [SerializeField]
        protected float coolTime;                       // 쿨타임
        [SerializeField]
        protected float calcCoolTime = 0.0f;            // 계산될 쿨타임

        public GameObject effectPrefab;                 // 타격 이펙트 프리팹

        [HideInInspector]
        public LayerMask targetMask;                    // 타겟 마스크

        protected Actor owner = null;
        protected StatsManager statsManager = null;
        #endregion Variables

        #region Property
        // 공격 사용 가능 여부 (쿨타임이 다 찼는가)
        public bool IsAvailable => calcCoolTime >= coolTime;

        protected float Damage => statsManager.GetValue(StatAttribute.AttackPower) * factor;
        #endregion Property

        #region Unity Methods
        private void Start() => InitializeAttackBehaviour();
        private void Update() => UpdateAttackBehaviour();
        #endregion Unity Methods

        #region Main Methods
        protected virtual void InitializeAttackBehaviour()
        {
            owner = GetComponent<Actor>();
            statsManager = GetComponent<StatsManager>();

            // 쿨타임 초기화
            calcCoolTime = coolTime;
        }

        protected virtual void UpdateAttackBehaviour()
        {
            // 쿨타임 계산
            if (calcCoolTime < coolTime)
            {
                calcCoolTime += Time.deltaTime;
            }
        }

        public void ResetCoolTime()
        {
            calcCoolTime = 0.0f;
        }

        /// <summary>
        /// 공격 실행 함수
        /// 매개변수가 많아질수록 매개변수를 확장하여 관리하는 편이 유리
        /// </summary>
        public abstract void ExecuteAttack(GameObject target = null, Transform fireTransform = null);
        #endregion Main Methods
    }
}
