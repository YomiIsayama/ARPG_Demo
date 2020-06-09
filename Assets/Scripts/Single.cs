﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SingleInstance
{
    public class Single<T> where T:new()
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }
        protected Single() { }
    }
}
