using System;
using _Game.Scripts.Managers;
using _Watchm1.Helpers.Logger;
using UnityEngine;

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
        private void Start()
        {
            props.health = 100;
            props.currentState = FishState.Puff;
            props.canMove = false;
            props.childObjectMeshRenderer = childObject.GetComponent<SkinnedMeshRenderer>();
            props.childObjectMeshRenderer.SetBlendShapeWeight(0,100);
        }

        private void Update()
        {
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
            WatchmLogger.Warning("puffed");
            props.canMove = false;
            props.childObjectMeshRenderer.SetBlendShapeWeight(0, 100);
        }

        private void HandleShrinkState()
        {
        
            props.currentState = FishState.Shrinked;
            props.canMove = true;
            WatchmLogger.Warning("shrinked");
            props.childObjectMeshRenderer.SetBlendShapeWeight(0, 0);

        }

        #endregion
    }
}