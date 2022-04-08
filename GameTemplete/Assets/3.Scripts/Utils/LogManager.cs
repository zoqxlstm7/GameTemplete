using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

using Debug = UnityEngine.Debug;

namespace GameTemplete
{
    public class LogManager
    {
        #region Variables
        public enum Color
        {
            aqua,
            black,
            blue,
            brown,
            cyan,
            darkblue,
            fuchsia,
            green,
            grey,
            lightblue,
            lime,
            magenta,
            maroon,
            navy,
            olive,
            purple,
            red,
            silver,
            teal,
            white,
            yellow,
            MAX
        }
        #endregion Variables

        #region Main Methods
        public static void Error(string message)
        {
#if UNITY_EDITOR
            Debug.LogError(message);
            return;
#endif
        }

        public static void Warning(string message)
        {
#if UNITY_EDITOR
            Debug.LogWarning(message);
            return;
#endif
        }

        public static void Log(string message, Color color = Color.white)
        {
#if UNITY_EDITOR
            //StackTrace stackTrace = new StackTrace();
            //for (int i = 0; i < stackTrace.FrameCount - 1; i++)
            //{
            //    StackFrame stackFrame = stackTrace.GetFrame(i);
            //    if (stackFrame.GetMethod().Name.Contains("ThreadStart"))
            //    {
            //        if (color == Color.white)
            //        {
            //            color = Color.yellow;
            //        }

            //        message = $"[Server] {message}";
            //        break;
            //    }
            //}

            Debug.Log(string.Format("<color={0}>{1}</color>", color.ToString(), message));
            return;
#endif
        }
        #endregion Main Methods
    }
}
