using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GameTemplete
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    [RequireComponent(typeof(StatsManager), typeof(AttackStateController))]
    public class Actor : MonoBehaviour, IAttackable, IDamageable
    {
        #region Variables
        protected Animator animator = null;
        protected Rigidbody refRigidbody = null;
        protected CapsuleCollider capsuleCollider = null;

        protected int MoveHash = -1;
        protected int MoveSpeedHash = -1;
        protected int AttackTriggerHash = -1;
        protected int AttackIndexHash = -1;
        protected int AttackSpeedHash = -1;

        protected Transform myTransform = null;
        [SerializeField]
        protected Transform fireTransform = null;

        [SerializeField]
        protected Transform target = null;
        private float distanceToTarget = 0.0f;

        private float viewRadius = 10.0f;

        [SerializeField]
        private LayerMask targetMask;

        [SerializeField]
        private List<AttackBehaviour> attackBehaviours = new List<AttackBehaviour>();

        protected StatsManager statsManager = null;
        protected AttackStateController attackStateController = null;
        #endregion Variables

        #region Property
        public bool IsInAttackState => attackStateController.IsInAttackState;

        public bool IsAvailableAttack
        {
            get
            {
                if (target == null)
                    return false;
                if (CurrentAttackBehaviour == null)
                    return true;

                float radius = (capsuleCollider.radius * 0.5f) + (target.GetComponent<CapsuleCollider>().radius * 0.5f);
                float distance = Vector3.Distance(target.position, myTransform.position);
                float attackableRange = CurrentAttackBehaviour.attackableRange + radius;
                return distance <= attackableRange;
            }
        }
        #endregion Property

        #region Unity Methods
        private void Start() => InitializeActor();
        private void Update() => UpdateActor();
        private void FixedUpdate() => FixedUpdateActor();
        #endregion Unity Methods

        #region Main Methods
        protected virtual void InitializeActor()
        {
            myTransform = transform;

            animator = GetComponent<Animator>();
            refRigidbody = GetComponent<Rigidbody>();
            capsuleCollider = GetComponent<CapsuleCollider>();

            attackStateController = GetComponent<AttackStateController>();
            statsManager = GetComponent<StatsManager>();

            MoveHash = Animator.StringToHash(AnimatorKeys.Move);
            MoveSpeedHash = Animator.StringToHash(AnimatorKeys.MoveSpeed);

            AttackTriggerHash = Animator.StringToHash(AnimatorKeys.AttackTrigger);
            AttackIndexHash = Animator.StringToHash(AnimatorKeys.AttackIndex);
            AttackSpeedHash = Animator.StringToHash(AnimatorKeys.AttackSpeed);

            InitAttackBehaviour();
        }

        protected virtual void UpdateActor()
        {
            CheckAttackBehaviour();
        }

        protected virtual void FixedUpdateActor() { }

        public Transform SearchEnemy()
        {
            // 공격 상태에서 타겟 검색 null 처리
            if (attackStateController.IsInAttackState)
                return target;

            target = null;
            distanceToTarget = 0.0f;

            GameObject[] targetInViewRadius = CombatManager.GetAroundEnemyArr(myTransform.position, viewRadius, targetMask);
            if (targetInViewRadius != null)
            {
                for (int i = 0; i < targetInViewRadius.Length; i++)
                {
                    Transform findTarget = targetInViewRadius[i].transform;
                    float distanceToFindTarget = Vector3.Distance(myTransform.position, findTarget.position);

                    if (target == null || distanceToTarget > distanceToFindTarget)
                    {
                        target = findTarget;
                        distanceToTarget = distanceToFindTarget;
                    }
                }
            }

            return target;
        }

        /// <summary>
        /// 공격 행동을 초기화하는 함수
        /// </summary>
        private void InitAttackBehaviour()
        {
            foreach (AttackBehaviour behaviour in attackBehaviours)
            {
                if (CurrentAttackBehaviour == null)
                {
                    CurrentAttackBehaviour = behaviour;
                }

                behaviour.targetMask = targetMask;
            }
        }

        /// <summary>
        /// 사용 가능한 공격 행동을 검사하는 함수
        /// </summary>
        private void CheckAttackBehaviour()
        {
            if (IsInAttackState)
                return;

            if (CurrentAttackBehaviour == null || !CurrentAttackBehaviour.IsAvailable)
            {
                CurrentAttackBehaviour = null;

                foreach (AttackBehaviour behaviour in attackBehaviours)
                {
                    if (behaviour.IsAvailable)
                    {
                        if (CurrentAttackBehaviour == null || CurrentAttackBehaviour.priority < behaviour.priority)
                        {
                            CurrentAttackBehaviour = behaviour;
                        }
                    }
                }
            }
        }

        public void FaceToTarget(Transform faceTarget)
        {
            Vector3 dir = (faceTarget.position - myTransform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0.0f, dir.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10.0f);
        }

        public void RemoveTarget()
        {
            target = null;
        }

        protected virtual void EndAttack()
        {
            CurrentAttackBehaviour.ResetCoolTime();
            CurrentAttackBehaviour = null;

            RemoveTarget();
        }
        #endregion Main Methods

        #region IAttackable Interface
        public AttackBehaviour CurrentAttackBehaviour { get; private set; }

        public void OnExecuteAttack(int attackIndex)
        {
            if (CurrentAttackBehaviour != null && target != null)
            {
                CurrentAttackBehaviour.ExecuteAttack(target.gameObject, fireTransform);
            }
        }
        #endregion IAttackable Interface

        #region IDamageable Interface
        public bool IsDead => statsManager.health <= 0;

        public virtual void TakeDamage(CombatData data, GameObject hitEffectPrefabs)
        {
            if (IsDead)
                return;

            float damage = data.damage;
            damage = CombatManager.GetHitDamageWithDef(statsManager, damage);

            statsManager.AddHealth(-damage);
        }
        #endregion IDamageable Interface
    }
}
