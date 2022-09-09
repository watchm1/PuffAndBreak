using _Game.Scripts.LocalStorage;
using UnityEngine;

namespace _Game.Scripts.AbilitySystem
{
    public abstract class Ability : IExecuter
    {
        #region Definition
        public string AbilityName { get; set; }
        public int CurrentUpgradeLevel { get; set; }
        public int AbilityMaxLevel { get; set; }
        public int AbilityUnclock { get; set; }
        #endregion
        #region Methods
        public abstract void Execute(Player.Player player);
        public abstract void Initialize();
        #endregion
    }
}