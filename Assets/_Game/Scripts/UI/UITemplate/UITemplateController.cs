using System;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Game.Scripts.UI.UITemplate
{
    public class UITemplateController : MonoBehaviour
    {
        // toggle goal panel
        // toggle of goal panel
        // toggle upgrade panel
        // toggle of upgrade panel


        #region Definition

        // all panels
        [SerializeField] private GameObject basePanel;
        [SerializeField] private GameObject goalPanel;
        [SerializeField] private GameObject upgradePanel;
        // basePanelButton;
        [SerializeField] private GameObject goalButton;

        /// <summary>
        /// ////////////////////////// upgrade pannel
        /// </summary>
        [SerializeField] private GameObject boatUpgradeCard;
        [SerializeField] private GameObject crewUpgradeCard;
        [SerializeField] private GameObject itemUpgradeCard;
        [SerializeField] private GameObject line;
        [SerializeField] private GameObject containerBoat;
        [SerializeField] private GameObject containerCrew;
        [SerializeField] private GameObject containerItem;
        private float _smallHeight;
        private float _largeHeight;
        private RectTransform _boatTranform;
        private RectTransform _crewTransform;
        private RectTransform _itemTransform;
        #endregion

        #region LifeCycle

        private void Start()
        {
            goalPanel.SetActive(false);
            basePanel.SetActive(false);
            upgradePanel.SetActive(false);
            StartCoroutine(FirstOpening());
            _largeHeight = boatUpgradeCard.GetComponent<RectTransform>().rect.height;
            _smallHeight = crewUpgradeCard.GetComponent<RectTransform>().rect.height;
            _boatTranform = boatUpgradeCard.GetComponent<RectTransform>();
            _crewTransform = crewUpgradeCard.GetComponent<RectTransform>();
            _itemTransform = itemUpgradeCard.GetComponent<RectTransform>();
        }

        #endregion

        #region Methods
        private void ToggleEffect(GameObject obj, bool on)
        {
            GameObject childObj = null;
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                if (obj.transform.GetChild(i).CompareTag("PopUp"))
                {
                    childObj = obj.transform.GetChild(i).gameObject;
                }
            }
            if (on)
            {
                if (childObj != null)
                    childObj.transform.DOScale(Vector3.zero, 0.4f).OnComplete(() =>
                    {
                        obj.SetActive(false);
                    });
            }
            else
            {
                if (childObj != null) childObj.transform.localScale = Vector3.zero;
                obj.SetActive(true);
                if (childObj != null) childObj.transform.DOScale(new Vector3(1, 1, 1), 1f);
            }
        }
        public void ToggleGoal()
        {
            ToggleEffect(goalPanel, goalPanel.activeSelf);
        }
        public void ToggleUpgrade()
        {
            ToggleEffect(upgradePanel, upgradePanel.activeSelf);
        }

        public void GoalClaim()
        {
            // will edit
        }

        private void HandleUpgradeAcitivities(int type)
        {
            switch (type)
            {
                case 0:
                    containerBoat.SetActive(true);                    
                    containerCrew.SetActive(false);                    
                    containerItem.SetActive(false);
                    break;
                case 1:
                    containerBoat.SetActive(false);                    
                    containerCrew.SetActive(true);                    
                    containerItem.SetActive(false);
                    break;
                case 2:
                    containerBoat.SetActive(false);                    
                    containerCrew.SetActive(false);                    
                    containerItem.SetActive(true);
                    break;
                
                
            }
        }
        public void ActiveBoat()
        {
           
            line.GetComponent<Image>().color = _boatTranform.GetComponent<Image>().color;
            HandleUpgradeAcitivities(0);
        }
        
        public void ActiveCrew()
        {
            line.GetComponent<Image>().color = _crewTransform.GetComponent<Image>().color;
            HandleUpgradeAcitivities(1);

        }

        public void ActiveItem()
        {
            line.GetComponent<Image>().color = _itemTransform.GetComponent<Image>().color;
            HandleUpgradeAcitivities(2);
        }
        
        private IEnumerator FirstOpening()
        {
            yield return new WaitForSeconds(2f);
            basePanel.SetActive(true);
        }
        #endregion

       

        
        
    }
}
