using System.Threading;
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
            PlayerMovement ownerMovement = player.GetComponent<PlayerMovement>();
            if (upgradeCount > 0)
            {
                //todo: do fast movement one time !!
                if (abilityPower > 0)
                {
                    ownerMovement.multiplierCount = abilityPower;
                }
                else
                {
                    ownerMovement.multiplierCount = 1;
                }
            }
            else
            {
                ownerMovement.multiplierCount = 1;
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
                    // todo:: make buy ability button disable
                    // will came another event for pop up ?
                }
            }
            else
            {
                base.UpgradeAction();
            }
        }
    }
}