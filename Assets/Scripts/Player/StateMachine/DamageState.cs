using System.Collections;
using Lib.StateMachine;
using Lib.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player.StateMachine
{
    public class DamageState: AbstractState
    {
        private readonly PlayerManager _playerManager;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly Transform _source;

        private Color originalColor;
        private bool takenDamage = false;
        
        public DamageState(PlayerManager playerManager, Transform source)
        {
            _playerManager = playerManager;
            _spriteRenderer = _playerManager.PlayerMovement.PlayerSpriteRenderer;
            _source = source;
        }
        public override void Enter()
        {
            originalColor = _playerManager.PlayerMovement.PlayerSpriteRenderer.color;
            _playerManager.FreezePlayer();
            _playerManager.PlayerInput.EnableBuffering();
            _playerManager.PlayerAnimator.SetTrigger(PlayerAnimation.Damage);
        }

        public override void Exit()
        {
            _playerManager.PlayerInput.DisableBuffering();
            _spriteRenderer.color = originalColor;
            _playerManager.UnfreezePlayer();
        }

        public override void Update()
        {
            _spriteRenderer.color = BlinkService.BlinkUpdateColor(_spriteRenderer.color);
        }
        
        public override bool AllowTransition(AbstractState nextState)
        {
            return takenDamage;
        }

        public override void FixedUpdate()
        {
            if (takenDamage)
            {
                return;
            }

            takenDamage = true;
            bool facingSource = DistanceHelper.IsFacing(_source, _playerManager.PlayerMovement.transform);
            _playerManager.PlayerMovement.PlayerRigidbody2D.AddForce(new Vector2(facingSource ? 3 : -3, 0), ForceMode2D.Impulse);
            _playerManager.StartCoroutine(ChangeStateRoutine());
        }

        private IEnumerator ChangeStateRoutine()
        {
            yield return new WaitForSeconds(1f);
            _playerManager.StateMachine.TransitionTo(new IdleState(_playerManager));
        }
    }
}