using _Game.Scripts.DamageSystem;
using _Watchm1.EventSystem.Events;
using _Watchm1.Helpers.Effects.Abstract;
using UnityEngine;

namespace _Game.Scripts.Effects
{
    public class DamageTakenEffect : IEffecter<DamageTakenEffect>
    {
        public EffectType EffectType { get; set; }
        private GameObject _objectSelf;
        public void DoEffect()
        {
        }
        public DamageTakenEffect(GameObject objectSelf)
        {
            _objectSelf = objectSelf;

            var meshRenderer = _objectSelf.GetComponent<MeshRenderer>();
        }
    }
}
