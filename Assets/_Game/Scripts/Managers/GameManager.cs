using System;
using System.Collections.Generic;
using _Game.Scripts.LocalStorage;
using _Game.Scripts.Player;
using _Watchm1.Helpers.Logger;
using _Watchm1.Helpers.Singleton;
using UnityEngine;

namespace _Game.Scripts.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [System.Serializable]
        public struct LevelData
        {
            public int levelIndex;
            public GameObject currentLevelPrefab;
            public GameObject currentLevelBackEnvironment;
            public bool levelDone;
        }

        [SerializeField] public List<LevelData> levelDatas;
        [SerializeField] public AbilityController abilityController;
        private int _currentLevelIndex;

        protected override void Awake()
        {
            base.Awake();
            LoadLevelFunction();
        }

        private void Start()
        {
           
        }

        public void LoadLevelFunction()
        {
            if (!PlayerPrefsInjector.CheckLocalStorageValue("LevelIndex"))
            {
                PlayerPrefsInjector.SetIntValue("LevelIndex", 0);
                _currentLevelIndex = 0;
            }
            else
            {
                _currentLevelIndex = PlayerPrefsInjector.GetIntValue("LevelIndex");
            }
            try
            {
                var currentLevelData = levelDatas.Find(index => index.levelIndex == _currentLevelIndex);
                Instantiate(currentLevelData.currentLevelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                Instantiate(currentLevelData.currentLevelBackEnvironment, new Vector3(0, 0, 0), Quaternion.identity);
            }
            catch (Exception e)
            {
                WatchmLogger.Warning("Something went worng"+ e);
                throw;
            }
        }
    }
}