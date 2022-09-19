using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.DamageSystem;
using _Game.Scripts.Effects;
using _Game.Scripts.Enemy.State;
using _Watchm1.EventSystem.Events;
using _Watchm1.Helpers.Effects.Abstract;
using _Watchm1.Helpers.Logger;
using imports._Watchm1.SceneManagment.Settings;
using Pathfinding;
using Unity.VisualScripting;
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
    public class EnemyBrain : DamageDealer, IDamageable
    {
        #region Definition
        // AI
        [SerializeField] public AttackType attackType;
        [SerializeField] public List<Transform> randomLocationsForMovingAround;
        [SerializeField] private EnemyIconOverlayController enemyIconOverlayController;
        [SerializeField] private Transform iconTransform;
        
        [HideInInspector]public List<IEnemyState> states;
        [HideInInspector]public IEnemyState  currentState;
        
        
        
        public Animator animator;
        public AIDestinationSetter destinationSetter;
        public AIPath pathController;
        public GameObject targetPlayer;
        public EnemyState currentEnemyState;
        private int _playerStayTime;
        
        
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
            destinationSetter = GetComponent<AIDestinationSetter>();
            pathController = GetComponent<AIPath>();
            _playerStayTime = 2;
            randomLocationsForMovingAround = new List<Transform>();
            ChangeState(states[0]);
            enemyIconOverlayController.pivot = iconTransform;
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
                HandleIconShow();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                currentEnemyState = EnemyState.LookingAround;
                HandleIconShow();
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

        private void HandleIconShow()
        {
            if (!enemyIconOverlayController.gameObject.activeSelf)
            {
                enemyIconOverlayController.gameObject.SetActive(true);
                enemyIconOverlayController.isActive = true;
            }
            else
            {
                enemyIconOverlayController.gameObject.SetActive(false);
                enemyIconOverlayController.isActive = false;
            }
        }
        #endregion

        
    }
}
