using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplete
{
    public class EventManager
    {
        #region Variables
        public delegate void EventDelegate(params object[] parameters);
        private static Dictionary<EventType, EventDelegate> dicEvents = new Dictionary<EventType, EventDelegate>();
        #endregion Variables

        #region Main Methods
        /// <summary>
        /// 이벤트 등록
        /// </summary>
        public static void AddEvent(EventType type, EventDelegate callback)
        {
            if (dicEvents.ContainsKey(type))
            {
                if(dicEvents[type].Equals(callback))
                {
                    LogManager.Warning("[이벤트 중복 딜리게이트 등록시도] {type}");
                }
                else
                {
                    dicEvents[type] += callback;
                }
            }
            else
            {
                dicEvents.Add(type, callback);
            }
        }

        /// <summary>
        /// 이벤트 제거
        /// </summary>
        /// <param name="type"></param>
        /// <param name="callback"></param>
        public static void RemoveEvent(EventType type, EventDelegate callback)
        {
            if (dicEvents.ContainsKey(type))
            {
                if(dicEvents[type].GetInvocationList().Length == 1 && dicEvents[type].Equals(callback))
                {
                    dicEvents.Remove(type);
                }
                else
                {
                    dicEvents[type] -= callback;
                }
            }
        }

        public static void InvokeEvent(EventType type, params object[] parameters)
        {
            LogManager.Log($"Event : {type}", LogManager.Color.green);

            if (dicEvents.ContainsKey(type))
            {
                dicEvents[type].Invoke(parameters);
            }
        }
        #endregion Main Methods
    }
}
