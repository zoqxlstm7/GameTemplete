using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplete
{
    public abstract class SingletonMonobehaviour<T> : MonoBehaviour where T : Component
    {
        #region Variables
        private static bool hasInstance = false;
        private static T instance = null;
        #endregion Variables

        #region Property
        public static T Instance
        {
            get
            {
                if (!hasInstance)
                {
                    hasInstance = true;

                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject go = Resources.Load<GameObject>($"Singleton/{typeof(T).Name}");
                        if(go != null)
                        {
                            instance = Instantiate(go).GetComponent<T>();
                        }
                        else
                        {
                            go = new GameObject(typeof(T).Name);
                            instance = go.AddComponent<T>();
                        }
                    }
                }


                return instance;
            }
        }

        protected abstract bool DontDestroyOnLoad { get; }
        #endregion Property

        #region Unity Methods
        private void Awake()
        {
            if (CheckAnotherInstance())
                return;

            // 접근이 없는 Singleton 생성
            T instantiation = Instance;
            if(DontDestroyOnLoad)
            {
                DontDestroyOnLoad(this);
            }

            OnAwake();
        }

        protected virtual void OnDestroy()
        {
            if(instance == this)
            {
                instance = null;
                hasInstance = false;
            }
        }
        #endregion Unity Methods

        #region Main Methods
        protected abstract void OnAwake();

        bool CheckAnotherInstance()
        {
            T[] instances = FindObjectsOfType<T>();
            if(instances.Length >= 2)
            {
                foreach (T other in instances)
                {
                    if(other != instance)
                    {
                        Destroy(other.gameObject);
                    }
                }

                return true;
            }

            return false;
        }
        #endregion Main Methods
    }
}
