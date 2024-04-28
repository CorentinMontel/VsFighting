using System.Collections;
using Lib.StateMachine;
using UnityEngine;

namespace Player.StateMachine
{
    public class SlidingState: AbstractState
    {
        private bool _slidingTrigger = false;
        private PlayerManager _playerManager;

        public SlidingState(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }
        
        public override void Enter()
        {
            _playerManager.PlayerInput.EnableBuffering();
            _playerManager.PlayerAnimator.SetBool(PlayerAnimation.Sliding, true);
        }

        public override void Exit()
        {
            _playerManager.PlayerInput.DisableBuffering();
            _playerManager.PlayerAnimator.SetBool(PlayerAnimation.Sliding, false);
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            if (_slidingTrigger)
            {
                return;
            }

            _slidingTrigger = true;

            float inputDirection = _playerManager.PlayerInput.GetHorizontalAxis();
            int direction;
            if (Mathf.Abs(inputDirection) > 0.05f)
            {
                direction = inputDirection > 0 ? 1 : -1;
            }
            else
            {
                direction = _playerManager.PlayerMovement.PlayerSpriteRenderer.transform.localScale.x > 0 ? 1 : -1;                
            }
            _playerManager.PlayerMovement.PlayerRigidbody2D.AddForce(new Vector2(direction * 6, 0), ForceMode2D.Impulse);
            _playerManager.StartCoroutine(ApplySlide());
        }

        private IEnumerator ApplySlide()
        {
            GameObject go = _playerManager.PlayerMovement.PlayerCollider.gameObject;
            int originalLayer = go.layer;
            go.layer = LayerMask.NameToLayer("Invincibility");
            yield return new WaitForSeconds(0.5f);
            go.layer = originalLayer;
            yield return new WaitForSeconds(0.25f);
            _playerManager.StateMachine.TransitionTo(new IdleState(_playerManager));
        }
    }
}