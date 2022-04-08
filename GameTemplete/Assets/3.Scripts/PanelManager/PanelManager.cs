using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameTemplete
{
    public class PanelManager
    {
        #region Variables
        // 패널 리스트
        public static Dictionary<Type, BasePanel> panels = new Dictionary<Type, BasePanel>();
        #endregion Variables

        #region Main Methods
        /// <summary>
        /// 패널을 등록하는 함수
        /// </summary>
        public static void RegistPanel(Type panelType, BasePanel panel)
        {
            if (panels.ContainsKey(panelType))
            {
#if UNITY_EDITOR
                LogManager.Warning($"[Panel] 이미 등록된 패널입니다. {panelType}");
#endif
                return;
            }

            panels.Add(panelType, panel);
        }

        /// <summary>
        /// 패널을 등록해제 하는 함수
        /// </summary>
        public static void UnRegistPanel(Type panelType)
        {
            if (!panels.ContainsKey(panelType))
            {
#if UNITY_EDITOR
                LogManager.Warning($"[Panel] 등록되지 않은 패널입니다. {panelType}");
#endif
                return;
            }

            panels.Remove(panelType);
        }

        /// <summary>
        /// 등록된 패널을 가져오는 함수
        /// </summary>
        public static T GetPanel<T>() where T : BasePanel
        {
            Type panelType = typeof(T);
            if (!panels.ContainsKey(panelType))
            {
#if UNITY_EDITOR
                LogManager.Warning($"[Panel] 패널 정보를 가져올 수 없습니다. {panelType}");
#endif
                return null;
            }

            // 패널을 가져오며 보여줌
            panels[panelType].Show();
            return panels[panelType] as T;
        }
        #endregion Main Methods
    }
}
