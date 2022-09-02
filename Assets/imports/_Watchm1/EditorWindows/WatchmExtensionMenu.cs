using System;
using System.IO;
using _Watchm1.Config;
using _Watchm1.Helpers.Logger;
using _Watchm1.SceneManagment.Settings;
using UnityEditor;
using Object = UnityEngine.Object;

namespace _Watchm1.EditorWindows
{
    public class WatchmExtensionMenu : UnityEditor.Editor
    {
        
        [MenuItem("WATCHMEXTENSION/Level Settings")]
        static void OpenLevelSettings()
        {
            var settings = LevelSettings.Current;

            if (settings == null)
            {
                settings = CreateInstance<LevelSettings>();
                CreateAssetIfNotExist(settings, "_Game/Settings/Resources");
            }
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = settings;
        }
       
        [MenuItem("WATCHMEXTENSION/Game Settings")]

        static void OpenGameSettings()
        {
            var settings = GameSettings.Current;

            if (settings == null)
            {
                settings = CreateInstance<GameSettings>();
                CreateAssetIfNotExist(settings, "_Game/Settings/Resources");
            }
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = settings;    
            
            
        }
        public static void CreateAssetIfNotExist<T>(T obj, string folderName) where T : Object
        {
            try
            {
                var path = $"Assets/{folderName}";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var file = $"{path}/{typeof(T).Name}.asset";
                WatchmLogger.Log($"Successfully created {file}");

                string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(file);
                AssetDatabase.CreateAsset(obj, assetPathAndName);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                WatchmLogger.Log(e);
                throw;
            }
        }
    }
}
