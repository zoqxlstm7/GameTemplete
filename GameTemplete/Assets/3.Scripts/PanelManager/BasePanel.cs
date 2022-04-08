using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplete
{
    public class BasePanel : MonoBehaviour
    {
        #region Unity Methods
        private void Awake() => PanelManager.RegistPanel(GetType(), this);
        private void Start() => InitializePanel();
        private void Update() => UpdatePanel();
        private void OnDestroy() => DestroyPanel();
        #endregion Unity Methods

        #region Main Methods
        protected virtual void InitializePanel() { }
        protected virtual void UpdatePanel() { }
        protected virtual void DestroyPanel() => PanelManager.UnRegistPanel(GetType());

        public void Show() => gameObject.SetActive(true);
        public void Close() => gameObject.SetActive(false);
        #endregion Main Methods
    }
}
