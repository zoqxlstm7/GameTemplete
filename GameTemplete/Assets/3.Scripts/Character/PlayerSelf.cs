using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplete
{
    public class PlayerSelf : Actor
    {
        #region Variables
        private Camera refCamera;

        private Vector3 lastDirection = Vector3.zero;
        [SerializeField]
        private float turnSmoothing = 0.05f;
        #endregion Variables

        #region Main Methods
        protected override void InitializeActor()
        {
            base.InitializeActor();

            refCamera = Camera.main;
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

        /// <summary>
        /// 마지막 방향 설정
        /// </summary>
        private void SetLastDirection(Vector3 direction)
        {
            lastDirection = direction;
        }

        /// <summary>
        /// 캐릭터가 이상한 방향으로 틀어지는 것을 방지하기 위해
        /// 포지션을 재구성
        /// </summary>
        private void Repositioning()
        {
            if (lastDirection != Vector3.zero)
            {
                lastDirection.y = 0.0f;
                Quaternion targetRotation = Quaternion.LookRotation(lastDirection);
                Quaternion newRotation = Quaternion.Slerp(refRigidbody.rotation, targetRotation, turnSmoothing);
                refRigidbody.MoveRotation(newRotation);
            }
        }

        /// <summary>
        /// 리지드바디의 y값을 제거하는 함수
        /// </summary>
        private void RemoveVerticalVelocity()
        {
            Vector3 horizontalVelocity = refRigidbody.velocity;
            horizontalVelocity.y = 0.0f;

            refRigidbody.velocity = horizontalVelocity;
        }

        /// <summary>
        /// 이동방향에 따라 플레이어의 방향을 전환해주는 함수
        /// </summary>
        private Vector3 Rotating(float horizontal, float vertical)
        {
            // 카메라의 방향에 따른 방향벡터를 반환
            Vector3 forward = refCamera.transform.TransformDirection(Vector3.forward);

            forward.y = 0.0f;
            forward = forward.normalized;

            // 내적이 0인 벡터는 서로 직교한다.
            // x,y를 바꾸고 한쪽으로 부호를 바꾸면 노말벡터를 구할수있다.
            Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);
            // 원하는 타겟방향
            Vector3 targetDirection = Vector3.zero;
            targetDirection = forward * vertical + right * horizontal;

            // 이동중이고 이동하려고 한다면
            if (targetDirection != Vector3.zero)
            {
                // 타겟각도 계산
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                // 타겟각도로 로테이션
                //Quaternion newRotation = Quaternion.Slerp(refRigidbody.rotation, targetRotation, turnSmoothing);
                Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSmoothing);

                // 캐릭터 회전 및 마지막 방향 저장
                //refRigidbody.MoveRotation(newRotation);
                transform.rotation = newRotation;
                SetLastDirection(targetDirection);
            }

            // 가만히 서있거나, 카메라가 마지막 방향으로
            // 회전중이라면 포지션 재조정
            if (!(Mathf.Abs(horizontal) > 0.9f || Mathf.Abs(vertical) > 0.9f))
            {
                Repositioning();
            }

            return targetDirection;
        }

        void MovementManagement()
        {
            float h = Input.GetAxisRaw(InputKeys.Horizontal);
            float v = Input.GetAxisRaw(InputKeys.Vertical);

            if ((Mathf.Abs(h) > 0.9f || Mathf.Abs(v) > 0.9f))
            {
                // y값이 0보다 크다면 어딘가에 껴있다는 것
                if (refRigidbody.velocity.y > 0)
                {
                    RemoveVerticalVelocity();
                }

                if (!attackStateController.IsInAttackState)
                {
                    // 회전
                    Rotating(h, v);
                }

                // 속도값 설정
                Vector3 dir = new Vector3(h, 0.0f, v);
                if (dir != Vector3.zero)
                {
                    animator.SetBool(MoveHash, true);
                    myTransform.position += transform.forward * 3.5f * Time.deltaTime;
                }
                else
                {
                    animator.SetBool(MoveHash, false);
                }
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
        }
        #endregion Main Methods
    }
}
