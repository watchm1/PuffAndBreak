using _Game.Scripts.AbilitySystem.@abstract;
using _Game.Scripts.Player;
using _Watchm1.Helpers.Logger;
using imports._Watchm1.SceneManagment.Settings;
using UnityEngine;

namespace _Game.Scripts.AbilitySystem
{
    public class ThrowThornAbility : Ability
    {
        public sealed override void Initialize()
        {
            abilityName = GameSettings.Current.throwThornsAbilityName;
            base.Initialize();
        }
        public ThrowThornAbility() : base()
        {
            Initialize();
        }
        public override void Activate(GameObject player)
        {
            Player.Player owner = player.GetComponent<Player.Player>();
            if (upgradeCount > 0)
            {
                owner.throwMechanicController.earnedAbility = true;
                if (abilityPower > 0)
                {
                    owner.throwMechanicController.SetRequirementsForMechanic(upgradeCount, abilityPower);
                }
                else
                {
                    owner.throwMechanicController.earnedAbility = false;
                }
            }
            else
            {
                WatchmLogger.Error("You haven't earn this ability");
                owner.throwMechanicController.earnedAbility = false;
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