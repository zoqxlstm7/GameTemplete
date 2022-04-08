using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplete
{
    public class Wait : MonoBehaviour
    {
        #region Variables
        // WaitForSecond 리스트 
        private static Dictionary<float, WaitForSeconds> waitList = new Dictionary<float, WaitForSeconds>();
        #endregion Variables

        #region Main Methods
        /// <summary>
        /// WaitForSecond 등록, 반환
        /// </summary>
        public static WaitForSeconds Seconds(float second)
        {
            if (!waitList.ContainsKey(second))
            {
                waitList.Add(second, new WaitForSeconds(second));
            }

            return waitList[second];
        }
        #endregion Main Methods
    }
}
