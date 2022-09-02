using _Watchm1.Helpers.Logger;
using _Watchm1.Helpers.Singleton;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Watchm1.Economy
{
 
    public class PersistEconomyManager : Singleton<PersistEconomyManager>
    {
        [FormerlySerializedAs("Currency")] public string currency = "Currency";

        public void SetToPrefsEconomy(int amount)
        {
            PlayerPrefs.SetInt(currency,amount);
        }

        public int GetCurrencyFromPrefs()
        {
            return PlayerPrefs.GetInt(currency);
        }

        public void CurrencyChangeListener()
        {
            WatchmLogger.Log(GetCurrencyFromPrefs());
        }
        public void ClearData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
