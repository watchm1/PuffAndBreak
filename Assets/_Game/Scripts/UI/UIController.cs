using _Watchm1.EventSystem.Events;
using UnityEngine;

namespace _Game.Scripts.UI
{
    public class UIController : MonoBehaviour
    {
         [SerializeField] public VoidEvent movementUpgrade;
         [SerializeField] public VoidEvent throwThornUpgrade;
         [SerializeField] public VoidEvent increaseMassUpgrade;


         public void UpgradeTrigger(VoidEvent someEvent)
         {
             someEvent.InvokeEvent();
         }
         public void ResetData()
         {
             PlayerPrefs.DeleteAll();
         }
             
    }
}
