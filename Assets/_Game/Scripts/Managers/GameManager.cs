using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.LocalStorage;
using _Watchm1.Helpers.Logger;
using _Watchm1.Helpers.Singleton;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private int currentLevelIndex;

    protected override void Awake()
    {
        base.Awake();
        LoadLevelFunction();
    }
    public void LoadLevelFunction()
    {
        if (!PlayerPrefsInjector.CheckLocalStorageValue("LevelIndex"))
        {
            PlayerPrefsInjector.SetIntValue("LevelIndex", 0);
            currentLevelIndex = 0;
        }
        else
        {
            currentLevelIndex = PlayerPrefsInjector.GetIntValue("LevelIndex");
        }
        try
        {
            var currentLevelData = levelDatas.Find(index => index.levelIndex == currentLevelIndex);
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