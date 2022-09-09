using _Game.Scripts.AbilitySystem.@abstract;
using _Watchm1.Helpers.Logger;
using UnityEngine;

namespace _Game.Scripts.AbilitySystem
{
    public class ThrowThornAbility : Ability
    {
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Activate(GameObject player)
        {
            if (upgradeCount > 0)
            {
                Player.Player owner = player.GetComponent<Player.Player>();
                owner.throwMechanicController.SetRequirementsForMechanic(upgradeCount, abilityPower);
            }
            else
            {
                WatchmLogger.Error("You haven't earn this ability");
            }
        }

        public override void UpgradeAction()
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
    }
}