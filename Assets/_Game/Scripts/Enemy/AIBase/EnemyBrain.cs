using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Enemy.State;
using imports._Watchm1.SceneManagment.Settings;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Scripts.Enemy.AIBase
{
    public enum EnemyState
    {
        LookingAround,
        DetectTimeLinePlayer,
        FollowPlayer,
        AttackPlayer,
        Death,
    }
    public class EnemyBrain : MonoBehaviour
    {
        #region Definition
        // AI
        [HideInInspector]public List<IEnemyState> states;
        [HideInInspector]public IEnemyState  currentState;
        public AIDestinationSetter destinationSetter;
        public AIPath pathController;
        // movement variables
        private float _horizontalSpeed;
        private float _verticalSpeed;
        [SerializeField]public List<Transform> randomLocationsForMovingAround;
        private int _playerStayTime;
        // animation
        public Animator animator;
        // state
        public EnemyState currentEnemyState;
        #endregion
        #region LifeCycle
        private void Start()
        {
            states = new List<IEnemyState>
            {
                new LookingAroundState(),
                new DetecTimeLinePlayer(),
                new FollowPlayerState(),
                new AttackPlayerState(),
                new DeathState()
            };
            currentState = null; // todo:: will change
            destinationSetter = GetComponent<AIDestinationSetter>();
            pathController = GetComponent<AIPath>();
            _verticalSpeed = GameSettings.Current.playerForwardSpeed;
            _horizontalSpeed = GameSettings.Current.playerHorizontalSpeed;
            _playerStayTime = 2;


            ChangeState(new LookingAroundState());
        }
        private void Update()
        {
            currentState.Update(this);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                currentEnemyState = EnemyState.DetectTimeLinePlayer;
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(IsCharacterInsideZone());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                currentEnemyState = EnemyState.LookingAround;
            }
        }
        #endregion

        #region Methods
        public void ChangeState(IEnemyState enemyState)
        {
            currentState = enemyState;
            currentState.OnBegin(this);
        }
        private IEnumerator IsCharacterInsideZone()
        {
            yield return new WaitForSeconds(_playerStayTime);
            if (currentEnemyState == EnemyState.DetectTimeLinePlayer)
            {
                currentEnemyState = EnemyState.FollowPlayer;
            }
            else
            {
                yield break;
            }
        }

        public void StateChanger()
        {
            switch (currentEnemyState)
            {
                case EnemyState.LookingAround:
                    ChangeState(states[0]);
                    break;
                case EnemyState.DetectTimeLinePlayer:
                    ChangeState(states[1]);
                    break;
                case EnemyState.FollowPlayer:
                    ChangeState(states[2]);
                    break;
                case EnemyState.AttackPlayer:
                    ChangeState(states[3]);
                    break;
                case EnemyState.Death:
                    ChangeState(states[4]);
                    break;
            }
        }

        #endregion
        
    }
}
