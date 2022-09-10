using _Game.Scripts.AbilitySystem.@abstract;
using _Game.Scripts.LocalStorage;
using _Game.Scripts.Player;
using _Watchm1.Helpers.Logger;
using imports._Watchm1.SceneManagment.Settings;
using UnityEngine;

namespace _Game.Scripts.AbilitySystem
{
    public class FastMovementAbility : Ability
    {
        public sealed override void Initialize()
        {
            abilityName = GameSettings.Current.fastMovementAbilityName;
            base.Initialize();
        }

        public FastMovementAbility() : base()
        {
            Initialize();
        }

        public override void Activate(GameObject player)
        {
            if (upgradeCount > 0)
            {
                //todo: do fast movement one time !!
                PlayerMovement ownerMovement = player.GetComponent<PlayerMovement>();
                ownerMovement.multiplier = abilityPower;
            }
            else
            {
                WatchmLogger.Log("ability not active");
            }
        }

        public override void UpgradeAction()
        {
            if (unlocked == 1)
            {
                if (upgradeCount < maxUpgradeCount)
                {
                    abilityPower += (upgradeCount * 0.5f);
                    upgradeCount += 1;
                    price *= 2;
                    base.UpgradeAction();
                }
                else
                {
                    WatchmLogger.Error("You cant upgrade your ability cause already max level");
                }
            }
            else
            {
                base.UpgradeAction();
            }
        }
    }
}