using System;
using System.Collections.Generic;
using _Game.Scripts.AbilitySystem.@abstract;
using _Game.Scripts.LocalStorage;
using _Watchm1.Helpers.Logger;
using imports._Watchm1.SceneManagment.Settings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.AbilitySystem
{
    public class AbilityController : SerializedMonoBehaviour
    {
        #region Definition

        public List<Ability> abilities;

        #endregion

        #region LifeCycle

        private void Start()
        {
            WatchmLogger.Log(GameSettings.Current.throwThornsAbilityName);
            Initializer();
        }
        #endregion
        #region Methods
        private void Initializer()
        {
            abilities = new List<Ability>
            {
                new FastMovementAbility(),
                new IncreaseMassAbility(),
                new ThrowThornAbility()
            };
        }
        [Button("Delete PrefsData")]
        private void DeletData()
        {
            PlayerPrefsInjector.DeleteAll();
            PlayerPrefs.Save();
            WatchmLogger.Log("deleted");
        }
        #endregion
    }
}