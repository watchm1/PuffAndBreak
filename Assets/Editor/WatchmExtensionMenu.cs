using System;
using System.IO;
using _Watchm1.Helpers.Logger;
using imports._Watchm1.SceneManagment.Settings;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Editor
{
    public class WatchmExtensionMenu : UnityEditor.Editor
    {
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
