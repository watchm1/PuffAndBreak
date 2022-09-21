using System;
using System.Collections;
using _Watchm1.Helpers.Logger;
using UnityEngine;

namespace _Game.Scripts.DamageSystem
{
    public class DamageEffect : MonoBehaviour
    {
        private SkinnedMeshRenderer _meshRenderer;
        private Material _originalMetarial;
        [SerializeField] private Material damageEffectMaterial;
        [SerializeField] private float duration; 

        private void Start()
        {
            _meshRenderer = GetComponent<SkinnedMeshRenderer>();
            _originalMetarial = _meshRenderer.material;
        }
        private IEnumerator FlashRoutine()
        {
            _meshRenderer.material = damageEffectMaterial;
            yield return new WaitForSeconds(duration);
            _meshRenderer.material = _originalMetarial;
        }

        public void FlashEffect()
        {
            WatchmLogger.Log("Çalıştı");
            StartCoroutine(FlashRoutine());
        }
    }
}
    
