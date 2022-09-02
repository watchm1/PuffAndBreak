using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _Watchm1.Config;
using _Watchm1.EventSystem.Listener;
using _Watchm1.Helpers.Logger;
using Sirenix.OdinInspector;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace _Watchm1.SceneManagment.Settings
{
    [System.Serializable]
    public enum GameType
    {
        Idle,
        Runner,
        Other
    }
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Wathcm1Extension/Create Level settings")]
    public class LevelSettings : BaseSettings<LevelSettings>
    {   
        [SerializeField, OnValueChanged("OnSceneFolderTypeChanged")] public GameType Type = GameType.Runner;
        [SerializeField, ShowIf("Type", GameType.Runner)] private string runnerFolder = "Assets/_Game/Scenes/Runner";
        [SerializeField, ShowIf("Type", GameType.Idle)] private string idleFolder = "Assets/_Game/Scenes/Idle";
        [SerializeField, ShowIf("Type", GameType.Other)] private string otherFolder= "Assets/_Game/Scenes/Other";

        [ShowInInspector, Sirenix.OdinInspector.ReadOnly] private string _levelScenesFolder =
            $"Assets/{Path.DirectorySeparatorChar}_Game{Path.DirectorySeparatorChar}Scenes{Path.DirectorySeparatorChar}Runner";

        [SerializeField] public List<string> allLevelSceneName = new();
        private static List<string> DefaultSceneNames
        {
            get
            {
                var defaultScenes = Enum.GetValues(typeof(DefaultScene)).Cast<DefaultScene>()
                    .Select(e => e.ToString()).ToList();
                return defaultScenes;
            }
        }

        public string LevelSceneFolder => _levelScenesFolder; 

        #if UNITY_EDITOR
        public void OnSceneFolderTypeChanged()
        {
            // adding gamesettings fixing
            CheckSceneFolder();
            ResetList();
            UpdateBuildList();
        }
        

        private List<string> GetAllLevelSceneNames()
        {
            var files = Directory.GetFiles(_levelScenesFolder, "*.unity")
                .Select(Path.GetFileNameWithoutExtension).ToList();
            return files;
        }

        private void LoadAllLevelSceneNames()
        {
            try
            {
                var levelSceneNames = GetAllLevelSceneNames();
                allLevelSceneName = levelSceneNames;
                UnityEditor.EditorUtility.SetDirty(this);
            }
            catch (Exception e)
            {
                WatchmLogger.Error(e);
                throw;
            }
        }
        private void CheckSceneFolder()
        {
            _levelScenesFolder = Type switch
            {
                GameType.Idle => idleFolder,
                GameType.Other => otherFolder,
                GameType.Runner => runnerFolder,
                _ => _levelScenesFolder
            };
        }
    
        private void ResetList()
        {
            LoadAllLevelSceneNames();
        }

        private void UpdateBuildList()
        {
            CheckSceneFolder();
            ResetList();
            var buildScenePaths = UnityEditor.EditorBuildSettings.scenes.Select(s => s.path).ToList();
            var scenesToAdd = new List<UnityEditor.EditorBuildSettingsScene>();
            foreach (var buildScenePath in buildScenePaths)
            {
                var sceneName = Path.GetFileNameWithoutExtension(buildScenePath);
                if (!DefaultSceneNames.Contains(sceneName))
                {
                    continue;
                }
                scenesToAdd.Add(new UnityEditor.EditorBuildSettingsScene(buildScenePath, true));
            }
            scenesToAdd.AddRange(allLevelSceneName.Select(s => new EditorBuildSettingsScene(Path.Combine(_levelScenesFolder, $"{s}.unity"), true)));
            var hasChanged = scenesToAdd.Count > 0;
            if (!hasChanged)
            {
                WatchmLogger.Log("No changes in build scene list");
                return;
            }
            WatchmLogger.Log("Updating build list scenes");
            scenesToAdd.ForEach(s => WatchmLogger.Log(s.path));
            UnityEditor.EditorBuildSettings.scenes = scenesToAdd.ToArray();


        }
        #endif
    }
}
