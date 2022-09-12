using System;
using System.Collections.Generic;
using _Watchm1.Helpers.Extensible.@abstract;
using _Watchm1.Helpers.Extensible.concrete;
using TMPro.Examples;
using UnityEngine;

namespace _Game.Scripts.Pool
{
    public class ObjectPool : MonoBehaviour, IExtensible
    {
        #region Definition

        [System.Serializable]
        public struct Pool
        {
            public GameObject poolObjectPrefab;
            public int poolSize;
            public List<GameObject> pooledObject;
        }

        [SerializeField] public Pool[] pools;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            for (int i = 0; i < pools.Length; i++)
            {
                pools[i].pooledObject = new List<GameObject>();
                for (int j = 0; j < pools[i].pooledObject.Count; j++)
                {
                    var obj = Instantiate(pools[i].poolObjectPrefab);
                    obj.transform.SetParent(transform);
                }
            }
        }

        #endregion

        #region Methods

        public GameObject GetObjectFromPool(int type)
        {
            if (type >= pools.Length)
            {
                return null;
            }
            else
            {
                if (pools[type].pooledObject.Count <= 0)
                {
                    var obj = Instantiate(pools[type].poolObjectPrefab);
                    obj.SetActive(true);
                    return obj;
                }
                else
                {
                    var obj = pools[type].pooledObject[0];
                    obj.SetActive(true);
                    pools[type].pooledObject.Remove(obj);
                    return obj;
                }
            }
        }

        public void ReturnObjectToPool(int type, GameObject obj)
        {
            obj.transform.SetParent(transform);
            obj.SetActive(false);
            obj.transform.localPosition = Vector3.zero;
            pools[type].pooledObject.Add(obj);
        }
        
        #endregion
    }
    
    
    
}