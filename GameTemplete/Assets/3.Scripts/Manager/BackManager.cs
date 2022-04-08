using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameTemplete
{
    public class BackManager : SingletonMonobehaviour<BackManager>
    {
        #region Variables
        protected override bool DontDestroyOnLoad => true;

        // 팝업 리스트
        private static List<System.Action> popupList = new List<System.Action>();
        #endregion Variables

        #region Unity Methods
        private void Update()
        {
            CheckBackButton();
        }
        #endregion Unity Methods

        #region Main Methods   
        protected override void OnAwake()
        {
            
        }

        /// <summary>
        /// 팝업 등록
        /// </summary>
        public void AddAction(Action action) => popupList.Add(action);
        /// <summary>
        /// 팝업 해제
        /// </summary>
        public void RemoveAction(Action action) => popupList.Remove(action);

        /// <summary>
        /// Back 버튼이 눌렸는지 검사하는 함수
        /// </summary>
        private void CheckBackButton()
        {
            // Todo: 팝업이 1회이상 나와야 인스턴스가 생성되며 CheckBackButton 가능한 문제 수정

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PopupClose();
            }
        }

        public void PopupClose()
        {
            if (popupList.Count > 0)
            {
                int index = popupList.Count - 1;
                popupList[index].Invoke();
            }
            else
            {
                //Todo: 로비와 인게임 나누기(게임종료, 전투종료)
                //     게임 종료 팝업 출력
                //PopupManager.Instance.ShowPopup<PopupMessage>().ShowMessage(
                //    "게임을 종료하시겠습니까?"
                //    , "종료"
                //    , "닫기"
                //    , () => { }
                //    , () => PopupManager.Instance.ClosePopup<PopupMessage>());
            }
        }
        #endregion Main Methods
    }
}
