using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace _Watchm1.Config
{
    public interface IBaseBuilder<T> where T : class
    {
        void Build();
    }
}


    
