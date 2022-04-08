using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameTemplete
{
    public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        #region Variables
        [SerializeField]
        private RectTransform background;  // 조이스틱 백그라운드 rect trasform
        [SerializeField]
        private RectTransform joystick;    // 조이스틱 rect transform

        private static Vector2 inputVector = Vector2.zero;  // 입력벡터
        private float bgRadius = 0.0f;                      // 조이스틱 백그라운드 반지름
        private float stickRadius = 0.0f;                   // 조이스틱 반지름
        #endregion Variables

        #region Unity Methods
        private void Start()
        {
            // 반지름 계산
            bgRadius = background.rect.width / 2;
            stickRadius = joystick.rect.width / 2;
        }
        #endregion Unity Methods

        #region EventSystems Interface
        /// <summary>
        /// 드래그 될 때 호출되는 함수
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData)
        {
            // 마우스 포지션과 조이스틱 백그라운드 간의 벡터를 계산
            inputVector = (eventData.position - (Vector2)background.position);
            // 스틱이 백그라운드를 벗어나지 못하도록 보정
            // 백그라운에 조이스틱이 걸치는 걸 방지하기 위해 스틱의 반지름만큼
            // 추가적으로 빼줘 백그라운드 내에 스틱이 위치하도록 한다.
            inputVector = Vector2.ClampMagnitude(inputVector, bgRadius - stickRadius);

            // 계산된 입력벡터의 위치로 조이스틱 이동
            joystick.localPosition = inputVector;
            // 방향벡터로 정규화
            inputVector = inputVector.normalized;
        }

        /// <summary>
        /// 포인터가 눌렸을 때 호출되는 함수
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            // 드래그 함수에 인자 전달
            OnDrag(eventData);
        }

        /// <summary>
        /// 포인터가 떨어졌을 때 호출되는 함수
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(PointerEventData eventData)
        {
            // 입력벡터 초기화 및 조이스틱 위치 초기화
            inputVector = Vector3.zero;
            joystick.localPosition = Vector3.zero;
        }
        #endregion EventSystems Interface

        #region Main Methods
        /// <summary>
        /// 수평값 반환 함수
        /// </summary>
        /// <returns></returns>
        public static float GetHorizontal()
        {
            return inputVector.x;
        }

        /// <summary>
        /// 수직값 반환 함수
        /// </summary>
        /// <returns></returns>
        public static float GetVertical()
        {
            return inputVector.y;
        }
        #endregion Main Methods
    }
}
