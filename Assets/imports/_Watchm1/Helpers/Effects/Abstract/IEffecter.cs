using UnityEngine;

public enum EffectType
{
   DoScale,
   ZeroToNormalScale,
}
namespace _Watchm1.Helpers.Effects.Abstract
{
   public interface IEffecter<T> where T : class 
   {
      public EffectType EffectType { get; set; }
      void DoEffect();
   }
}
