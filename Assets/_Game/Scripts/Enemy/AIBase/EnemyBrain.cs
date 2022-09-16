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
        [SerializeField] public Canvas enemyCanvas;
        [SerializeField] public AttackType attackType;
        [SerializeField] private VoidEvent onTakeDamage;
        
        
        [HideInInspector]public List<IEnemyState> states;
        [HideInInspector]public IEnemyState  currentState;
        [HideInInspector]public List<Transform> randomLocationsForMovingAround;
        
        
        
        public IEffecter<DamageTakenEffect> takeDamageEffect;
        public Animator animator;
        public AIDestinationSetter destinationSetter;
        public AIPath pathController;
        public GameObject targetPlayer;
        public EnemyState currentEnemyState;
        private float _horizontalSpeed;
        private float _verticalSpeed;
        private int _playerStayTime;
        private bool _firstDetection;
        
        
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
            enemyCanvas.gameObject.SetActive(false);
            
            ChangeState(new LookingAroundState());
            takeDamageEffect = new DamageTakenEffect(gameObject);
        }
        private void LateUpdate()
        {
            currentState.Update(this);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                currentEnemyState = EnemyState.DetectTimeLinePlayer;
                enemyCanvas.gameObject.SetActive(true);
                targetPlayer = other.gameObject;
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
                enemyCanvas.gameObject.SetActive(false);
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
        public void TakeDamage(int amount)
        {
            takeDamageEffect.DoEffect();
            onTakeDamage.InvokeEvent();
        }
        #endregion

        
    }
}
