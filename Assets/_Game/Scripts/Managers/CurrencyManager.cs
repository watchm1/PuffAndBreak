using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using _Game.Scripts.LocalStorage;
using _Watchm1.EventSystem.Events;
using _Watchm1.Helpers.Logger;
using _Watchm1.Helpers.Singleton;
using UnityEngine;
using UnityEngine.Serialization;

public enum ChangeCurrencyType
{
    Spend,
    Earn
}
public class CurrencyManager : Singleton<CurrencyManager>
{
    #region Definition
    private int CurrencyCount { get; set; }
    [SerializeField] private VoidEvent onCurrencyChange;
    #endregion

    #region LifeCycle

    protected override void Awake()
    {
        base.Awake();
        if(PlayerPrefsInjector.CheckLocalStorageValue("Currency"))
        {
            var currentCurrency = PlayerPrefsInjector.GetIntValue("Currency");
            CurrencyCount = currentCurrency == 0 ? 0 : currentCurrency;
        }
        else
        {
            CurrencyCount = 0;
            PlayerPrefsInjector.SetIntValue("Currency", CurrencyCount);
        }
        WatchmLogger.Log(CurrencyCount);
    }

    #endregion

    #region Methods
    public void ChangeCurrency(ChangeCurrencyType type, int count)
    {
        switch (type)
        {
            case ChangeCurrencyType.Earn:
                CurrencyCount += count;
                break;
            case ChangeCurrencyType.Spend:
                CurrencyCount -= count;
                break;
            default:
                break;
        }
        PlayerPrefsInjector.SetIntValue("Currency",CurrencyCount);
        onCurrencyChange.InvokeEvent();
    }

    public int GetCurrentCurrencyValue()
    {
        return CurrencyCount;
    }
    #endregion
}
