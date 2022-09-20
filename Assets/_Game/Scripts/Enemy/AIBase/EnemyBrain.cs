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


        [SerializeField]public EnemyIconOverlayController iconOverlayController;
        #endregion
        #region LifeCycle
        private void Start()
        {
            states = new List<IEnemyState>
            {
                new LookingAroundState(),
                new FollowPlayerState()
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
                targetPlayer = other.gameObject;
                iconOverlayController.HandleIconShow();
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
                ChangeState(states[0]);
                iconOverlayController.HandleIconShow();
                WatchmLogger.Log("Current target => "+ destinationSetter.target.name);

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
            currentEnemyState = EnemyState.FollowPlayer;
            ChangeState(states[1]);
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
                case EnemyState.FollowPlayer:
                    ChangeState(states[1]);
                    break;
            }
        }
        public void TakeDamage(int amount)
        {
        }

        public void SetNpcRotation(Transform target)
        {
            Vector3 newRot = new Vector3(); 
            if (transform.position.x < target.transform.position.x)
            {
                if (newRot.y != 90)
                    newRot.y = 90;
            }
            else
            {
                if(newRot.y != -90)
                    newRot.y = -90;
            }

            newRot.z = 0f;
            if (transform.GetChild(0).localRotation != Quaternion.Euler(newRot))
            {
                transform.GetChild(0).localRotation = Quaternion.Euler(newRot);
            }
        }
        #endregion

        
    }
}
