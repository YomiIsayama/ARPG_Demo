using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SingleInstance
{

    public class SingleMono<T> : MonoBehaviour
        where T : SingleMono<T>
    {
        private static T _instance;
        public static T Instance()
        {
            if (null == _instance)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (null == _instance)
                {
                    GameObject singleton = new GameObject("(SingleMono)" + typeof(T).ToString());
                    _instance = singleton.AddComponent<T>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return _instance;


        }
    }
        
}

