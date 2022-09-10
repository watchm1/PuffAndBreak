using UnityEngine;

namespace _Game.Scripts.LocalStorage
{
    public static class PlayerPrefsInjector
    {
        public static void SetIntValue(string name, int value)
        {
            PlayerPrefs.SetInt(name, value);
            PlayerPrefs.Save();
        }

        public static void SetString(string name, string value)
        {
            PlayerPrefs.SetString(name, value);
            PlayerPrefs.Save();
        }

        public static void SetFloat(string name, float value)
        {
            PlayerPrefs.SetFloat(name, value);
            PlayerPrefs.Save();
        }

        public static int GetIntValue(string name)
        {
            return PlayerPrefs.GetInt(name);
        }

        public static string GetString(string name)
        {
            return PlayerPrefs.GetString(name);
        }

        public static float GetFloat(string name)
        {
            return PlayerPrefs.GetFloat(name);
        }
        public static void HardReset()
        {
            PlayerPrefs.DeleteAll();
        }

        public static bool CheckLocalStorageValue(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}