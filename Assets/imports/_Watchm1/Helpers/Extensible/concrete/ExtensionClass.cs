using _Watchm1.Helpers.Extensible.@abstract;
using _Watchm1.Helpers.Logger;
using Unity.VisualScripting;
using UnityEngine;

namespace _Watchm1.Helpers.Extensible.concrete
{
    public static class ExtensionClass
    {
        public static void ChangeColor(this IExtensible extensible,Color color, GameObject obj)
        {
            var metarial = obj.GetComponent<Renderer>().material;
            var newColor = new Color(r:color.r, g:color.g, b:color.b, a:.24f);
            metarial.color = newColor;
            
        }
        public static void ThrowObjectToRandomPoint(this IExtensible extensible,GameObject obj, float xRandomRange, float yRandomRange, float zRandomRange)
        {
            var newPosition = new Vector3();
            newPosition.x = (int)Random.Range(-obj.transform.position.x, obj.transform.position.x + xRandomRange);
            newPosition.y = obj.transform.position.y;
            newPosition.z = (int)Random.Range(obj.transform.position.z, obj.transform.position.z+ zRandomRange);
            obj.transform.position = Vector3.Lerp(obj.transform.position, newPosition, Time.deltaTime );
        }
    }
}
