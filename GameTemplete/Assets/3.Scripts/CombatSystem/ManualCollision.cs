using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplete
{
    public class ManualCollision : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private Vector3 boxSize = new Vector3(2.0f, 2.0f, 2.0f);    // 박스 사이즈
        #endregion Variables

        #region Main Methods
        /// <summary>
        /// 박스 콜라이더를 오버랩하여 타겟을 검출하는 함수
        /// </summary>
        /// <param name="targetMask">타겟 마스크</param>
        /// <returns></returns>
        public Collider[] CheckOverlapBox(LayerMask targetMask)
        {
            return Physics.OverlapBox(transform.position, boxSize * 0.5f, transform.rotation, targetMask);
        }
        #endregion Main Methods

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            // 기즈모를 월드 위치에 그려주기 위해 매트릭스 사용
            // localToWorldMatrix: 로컬 위치를 월드위치로 변경해 주는 함수
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.yellow;

            Gizmos.DrawWireCube(Vector3.zero, boxSize);
        }
#endif
    }
}
