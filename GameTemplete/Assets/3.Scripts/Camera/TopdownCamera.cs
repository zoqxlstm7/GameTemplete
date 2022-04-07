using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplete
{
    public class TopdownCamera : MonoBehaviour
    {
        #region Variables
        public float height = 5f;               // 카메라가 위치할 높이
        public float distance = 10f;            // 타겟과의 거리
        public float angle = 45f;               // 바라볼 각도
        public float lookAtHeight = 2f;         // 카메라가 바라볼 높이
        public float smoothSpeed = 0.5f;        // 자연스럽게 이동하기 위한 속도값
        public Transform target = null;

        private Vector3 refVelocity = Vector3.zero;     // 내부 계산을 위한 속도벡터
        private Transform myTransform = null;
        #endregion Variables

        #region Unity Methods
        private void LateUpdate()
        {
            HandleCamera();
        }
        #endregion Unity Methods

        #region Other Methods
        /// <summary>
        /// 카메라를 핸들링하는 함수
        /// </summary>
        public void HandleCamera()
        {
            // 타겟이 없다면 리턴
            if (!target)
                return;

            // 월드 포지션 벡터 계산
            Vector3 worldPosition = (Vector3.forward * -distance) + (Vector3.up * height);
            // 계산된 월드 포지션 위치에서 up 벡터를 기준으로 회전벡터 계산
            Vector3 rotatedVector = Quaternion.AngleAxis(angle, Vector3.up) * worldPosition;

            // 카메라가 타겟의 머리쪽을 바라보도록 타겟 위치 재설정
            Vector3 finalTargetPosition = target.position;
            finalTargetPosition.y += lookAtHeight;

            // 최종 카메라 위치 계산
            Vector3 finalCameraPosition = finalTargetPosition + rotatedVector;

            // 카메라가 자연스럽게 움직일 수 있도록 보간 처리
            myTransform.position = Vector3.SmoothDamp(myTransform.position, finalCameraPosition, ref refVelocity, smoothSpeed);
            // 카메라가 타겟을 바라보도록 설정
            transform.LookAt(target.position);
        }
        #endregion Other Methods

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            // 타겟이 존재한다면
            if (target)
            {
                Vector3 lookAtPosition = target.position;
                lookAtPosition.y += lookAtHeight;
                // 타겟을 바라보는 위치에 라인과 원을 그림
                Gizmos.DrawLine(transform.position, lookAtPosition);
                Gizmos.DrawSphere(lookAtPosition, 0.5f);
            }

            // 카메라 위치에 원을 그림
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
#endif
    }
}
