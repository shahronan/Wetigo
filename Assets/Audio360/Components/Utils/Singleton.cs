/*
 *  Copyright 2013-Present Two Big Ears Limited
 *  All rights reserved.
 *  http://twobigears.com
 */

using System.Collections;
using UnityEngine;

namespace TBE
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour, IObject
    {
        private static object lock_ = new object();
        private static T instance_;
        static bool onDestroyCalled_ = false;

        public static T Instance
        {
            get
            {
                if (onDestroyCalled_)
                {
                    return null;
                }

                lock (lock_)
                {
                    if (instance_ == null)
                    {
                        instance_ = (T)FindObjectOfType(typeof(T));
                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            Utils.logError("There is more than one instance of " + typeof(T).ToString() + ". Restart your scene?", null);
                            return instance_;
                        }
                        if (instance_ == null)
                        {
                            GameObject singleton = new GameObject();
                            instance_ = singleton.AddComponent<T>();
                            singleton.name = "[" + typeof(T).ToString() + "]";
                            if (instance_.mustNotDestroyOnLoad())
                            {
                                DontDestroyOnLoad(singleton);
                            }
                            instance_.onInit();
                        } 
                    }
                    return instance_;
                }
            }
        }

        void OnDestroy()
        {
            if (instance_ != null)
            {
                instance_.onTerminate();
            }
            onDestroyCalled_ = true;
        }
    }
}