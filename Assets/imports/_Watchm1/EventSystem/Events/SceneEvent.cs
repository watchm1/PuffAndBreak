using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Watchm1.EventSystem.Events
{
    [CreateAssetMenu(fileName = "Create void Event" ,menuName = "Watchm1Extension/SceneEvent")]
    public class SceneEvent : BaseEvent<Scene>
    {
    }
}
