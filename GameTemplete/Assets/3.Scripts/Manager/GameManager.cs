using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplete
{
    public class GameManager : SingletonMonobehaviour<GameManager>
    {
        protected override bool DontDestroyOnLoad => true;

        protected override void OnAwake()
        {
            
        }
    }
}
