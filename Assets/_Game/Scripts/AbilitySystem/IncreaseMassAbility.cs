using _Game.Scripts.AbilitySystem.@abstract;
using _Game.Scripts.Camera;
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
            abilityPower = 1.1f;
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
                    
                    abilityPower += (0.02f);
                    upgradeCount += 1;
                    price *= 2;
                    base.UpgradeAction();
                }
                else
                {
                    // todo:: make buy ability button disable
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
            if (upgradeCount <= maxUpgradeCount)
            {
                Transform childMesh = player.transform.GetChild(0);
                var cam = GameObject.FindObjectOfType<CameraMovement>();
                var oldLocaleScale = new Vector3(11.3f, 11.3f, 11.3f);
                if (upgradeCount > 0)
                {
                    if (abilityPower > 0)
                    {
                        childMesh.transform.localScale *= abilityPower;
                        cam._offSet += cam._defaultOffset * 0.2f;
                    }
                    else
                    {
                        childMesh.transform.localScale = oldLocaleScale;
                        cam._offSet = cam._defaultOffset;
                    }
                }
                else
                {
                    childMesh.transform.localScale = oldLocaleScale;
                }
            }
        }
    }
}