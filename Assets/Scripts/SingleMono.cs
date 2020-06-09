using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SingleInstance
{

    public class SingleMono<T> : MonoBehaviour
        where T : SingleMono<T>
    {
        private const string ROOT_NAME = "SingleMono";
        private static T _instance = null;

        private static GameObject root
        {
            get
            {
                GameObject singleMono = GameObject.Find(ROOT_NAME);
                if (singleMono == null)
                {
                    singleMono = new GameObject(ROOT_NAME);
                    DontDestroyOnLoad(singleMono);
                }
                return singleMono;
            }
        }

        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject(typeof(T).Name);
                    gameObject.transform.SetParent(root.transform);
                    _instance = gameObject.AddComponent<T>();
                }
                return _instance;
            }
        }
    }

}

