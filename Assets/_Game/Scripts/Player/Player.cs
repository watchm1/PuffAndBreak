using _Game.Scripts.Collectible;
using _Game.Scripts.DamageSystem;
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
    [System.Serializable]
    public struct PlayerProps
    {
        public bool canMove;
        public FishState currentState;
        public int health;
        public SkinnedMeshRenderer childObjectMeshRenderer;
    }
    public class Player : MonoBehaviour, IDamageable
    {
        #region Definition

       
        [SerializeField] public GameObject childObject;
        [SerializeField] public ParticleSystem takenDamageEffect;
        [SerializeField] public DamageEffect effect;
        [SerializeField] private ThrowSpikeController controller;
        public Animator childAnimator;
        public PlayerProps props;
        private InputManager _inputManager;
        private AbilityController _abilityController;
        
        #endregion
        private void Start()
        {
            props.health = 100;
            props.currentState = FishState.Puff;
            props.canMove = false;
            props.childObjectMeshRenderer = childObject.GetComponentInChildren<SkinnedMeshRenderer>();
            props.childObjectMeshRenderer.SetBlendShapeWeight(0,100);
            _abilityController = GameManager.Instance.abilityController;
            takenDamageEffect.Pause();
            childAnimator = childObject.GetComponent<Animator>();
            _inputManager = InputManager.Instance;
            HandleGrowAbility();
        }

        private void Update()
        {
            if (!LevelManager.Instance.PlayModeActive())
            {
                return;
            }
            if (props.currentState == FishState.Puff && _inputManager.Touching())
            {
                // transition to shrink state
                HandleShrinkState();
            }
            else if (props.currentState == FishState.Shrinked && !_inputManager.Touching())
            {
                // transition to puff state
                HandlePuffState();
            }
            else
                return;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CollectibleItem collectibleItem))
            {
                collectibleItem.HandleCollection();
            }
        }


        #region Methods

        private void HandlePuffState()
        {
            props.currentState = FishState.Puff;
            props.canMove = false;
            props.childObjectMeshRenderer.SetBlendShapeWeight(0, 100);
        }

        private void HandleShrinkState()
        {
            controller.ThrowSpikeFunction();
            props.currentState = FishState.Shrinked;
            props.canMove = true;
            props.childObjectMeshRenderer.SetBlendShapeWeight(0, 0);
        }

        #endregion

        public void TakeDamage(int amount)
        {
            //todo:: adding health controller
            takenDamageEffect.Play();
            HealthDealer(amount);
        }

        private void HealthDealer(int amount)
        {
            effect.FlashEffect();
            if (HealthManager.Instance.CurrentHealth > amount)
            {
                HealthManager.Instance.CurrentHealth -= amount;
            }
            else
            {
                HealthManager.Instance.CurrentHealth = 0;
                LevelManager.Instance.InvokeLevelFail();
            }
        }
        public void HandleGrowAbility()
        {
            var localScale = transform.localScale;
            localScale += localScale * _abilityController.GetMultiplier(AbilityType.Grow);
            transform.localScale = localScale;
        }
    }
}