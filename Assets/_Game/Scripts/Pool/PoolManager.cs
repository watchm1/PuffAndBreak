using System;
using _Watchm1.Helpers.Singleton;
using UnityEngine;

namespace _Game.Scripts.Pool
{
    public class PoolManager : Singleton<PoolManager>
    {
        public ObjectPool pool;

        private void Start()
        {
            pool = FindObjectOfType<ObjectPool>();
        }
    }
}
