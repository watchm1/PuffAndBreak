using _Game.Scripts.AbilitySystem.@abstract;
using _Watchm1.Helpers.Logger;
using imports._Watchm1.SceneManagment.Settings;
using UnityEngine;

namespace _Game.Scripts.AbilitySystem
{
    public class IncreaseMassAbility : Ability
    {
        public sealed override void Initialize()
        {
            abilityName = GameSettings.Current.largeMassAbilityName;
            base.Initialize();
        }

        public IncreaseMassAbility() : base()
        {
            Initialize();
        }

        public override void UpgradeAction()
        {
            if (unlocked == 1)
            {
                if (upgradeCount < maxUpgradeCount)
                {
                    abilityPower += (upgradeCount * 0.2f);
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

        public override void Activate(GameObject player)
        {
            if (upgradeCount > 0)
            {
                //todo: localscale
            }
        }
    }
}