using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.DamageSystem;
using _Game.Scripts.Enemy.State;
using _Watchm1.Helpers.Logger;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;

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

    public enum AttackType
    {
        Shark,
        Diver,
        Whale
    }
    public class EnemyBrain : DamageDealer, IDamageable
    {
        #region Definition
        // AI
        [SerializeField] public AttackType attackType;
        [SerializeField] public List<Transform> randomLocationsForMovingAround;
        [HideInInspector]public List<IEnemyState> states;
        [HideInInspector]public IEnemyState  currentState;
        public Animator animator;
        public AIDestinationSetter destinationSetter;
        public AIPath pathController;
        public GameObject targetPlayer;
        public EnemyState currentEnemyState;
        private int _playerStayTime;


        [SerializeField]private EnemyIconOverlayController iconOverlayController;
        #endregion
        #region LifeCycle
        private void Start()
        {
            states = new List<IEnemyState>
            {
                new LookingAroundState(),
            };
            destinationSetter = GetComponent<AIDestinationSetter>();
            pathController = GetComponent<AIPath>();
            _playerStayTime = 2;
            randomLocationsForMovingAround = new List<Transform>();
            ChangeState(states[0]);
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
                targetPlayer = other.gameObject;
                iconOverlayController.HandleIconShow();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                currentEnemyState = EnemyState.LookingAround;
                iconOverlayController.HandleIconShow();
            }
        }
        #endregion

        #region Methods
        public void ChangeState(IEnemyState enemyState)
        {
            currentState = enemyState;
            currentState.OnBegin(this);
        }
        public IEnumerator IsCharacterInsideZone()
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

        public void HandleRotation(Transform target)
        {
            // todo: will code for enemy's rotation will set for enemy's target
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
        public void TakeDamage(int amount)
        {
        }
        
        #endregion

        
    }
}
