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
        }
        public PlayerProps props;
        private void Start()
        {
            props.health = 100;
            props.currentState = FishState.Puff;
            props.canMove = false;
        }
        private void Update()
        {
            if (props.currentState == FishState.Puff && InputManager.Instance.Touching())
            {
                props.currentState = FishState.Shrinked;
                props.canMove = true;
                WatchmLogger.Warning("shrinked");
            }
            else if (props.currentState == FishState.Shrinked && !InputManager.Instance.Touching())
            {
                props.currentState = FishState.Puff;
                WatchmLogger.Warning("puffed");
                props.canMove = false;
            }
            else
                return;
        }

        #endregion
    }
}