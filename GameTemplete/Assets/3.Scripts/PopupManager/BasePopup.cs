using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplete
{
    public class BasePopup : MonoBehaviour
    {
        #region Unity Methods
        private void OnEnable() => EnablePopup();
        private void Start() => InitializePopup();
        private void Update() => UpdatePopup();
        private void OnDisable() => DisablePopup();
        #endregion Unity Methods

        #region Main Methods
        /// <summary>
        /// 팝업을 BackManager에 등록
        /// </summary>
        private void EnablePopup() => BackManager.Instance.AddAction(Close);
        /// <summary>
        /// 팝업을 BackManager에서 해제
        /// </summary>
        protected virtual void DisablePopup() => BackManager.Instance.RemoveAction(Close);

        protected virtual void InitializePopup() { }
        protected virtual void UpdatePopup() { }

        public virtual void Close()
        {
            gameObject.SetActive(false);
            //PopupManager.Instance.ClosePopup(GetType());
        }
        #endregion Main Methods
    }
}
