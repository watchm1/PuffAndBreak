using _Game.Scripts.AbilitySystem;
using _Game.Scripts.Managers;
using _Watchm1.SceneManagment.Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Player
{
    public enum FishState
    {
        Puff,
        Shrinked
    }

    public class Player : MonoBehaviour
    {
        #region Definition

        public struct PlayerProps
        {
            public bool canMove;
            public FishState currentState;
            public int health;
            public SkinnedMeshRenderer childObjectMeshRenderer;
        }
        public PlayerProps props;
        [SerializeField] public GameObject childObject;
        public Animator childAnimator;
        public ThrowMechanicController throwMechanicController;
        private void Start()
        {
            props.health = 100;
            props.currentState = FishState.Puff;
            props.canMove = false;
            props.childObjectMeshRenderer = childObject.GetComponentInChildren<SkinnedMeshRenderer>();
            props.childObjectMeshRenderer.SetBlendShapeWeight(0,100);
            childAnimator = childObject.GetComponent<Animator>();
            throwMechanicController = GetComponent<ThrowMechanicController>();
            GetComponentInChildren<AbilityController>().abilities[1].Activate(gameObject);
            GetComponentInChildren<AbilityController>().abilities[2].Activate(gameObject);
        }

        private void Update()
        {
            if (!LevelManager.Instance.PlayModeActive())
            {
                return;
            }
            if (props.currentState == FishState.Puff && InputManager.Instance.Touching())
            {
                // transition to shrink state
                HandleShrinkState();
            }
            else if (props.currentState == FishState.Shrinked && !InputManager.Instance.Touching())
            {
                // transition to puff state
                HandlePuffState();
            }
            else
                return;
        }

        #endregion

        #region Methods

        private void HandlePuffState()
        {
            props.currentState = FishState.Puff;
            props.canMove = false;
            props.childObjectMeshRenderer.SetBlendShapeWeight(0, 100);
        }

        private void HandleShrinkState()
        {
            UseThrowMechanic();
            props.currentState = FishState.Shrinked;
            props.canMove = true;
            props.childObjectMeshRenderer.SetBlendShapeWeight(0, 0);
        }

        public void UseThrowMechanic()
        {
            StartCoroutine(throwMechanicController.ThrowThrown());
        }
        #endregion
    }
}