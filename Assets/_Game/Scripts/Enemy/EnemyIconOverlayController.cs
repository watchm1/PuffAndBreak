using UnityEngine;

namespace _Game.Scripts.Enemy
{
    public class EnemyIconOverlayController : MonoBehaviour
    {
        [SerializeField] public Transform pivot;
        [SerializeField] private GameObject icon;
        private GameObject _uiUse;
        public bool isActive;
        private void Start()
        {
            isActive = false;
            _uiUse = Instantiate(icon, GameObject.FindGameObjectWithTag("InGameUI").transform).gameObject;
            _uiUse.SetActive(false);
        }

        private void LateUpdate()
        {
            if (isActive)
            {
                if (UnityEngine.Camera.main != null)
                    _uiUse.transform.position = UnityEngine.Camera.main.WorldToScreenPoint(pivot.transform.position);
            }
        }
        public void GetPivotTransform(Transform target)
        {
            pivot = target;
        }

        public void SetFadeEffect(bool value)
        {
            var fadeEffect =_uiUse.gameObject.GetComponent<FadeEffect>();
            fadeEffect.enabled = value;
        }
        public void HandleIconShow()
        {
            if (!isActive)
            {
                isActive = true;
                _uiUse.SetActive(true);
            }
            else
            {
                _uiUse.SetActive(false);
                isActive = false;
            }
        }
    }
}