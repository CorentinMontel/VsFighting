using Lib.StateMachine;
using UnityEngine;

namespace Player.StateMachine
{
    public class FallingState : AbstractState
    {
        private PlayerManager _playerManager;
        private Animator _playerAnimator;
        private PlayerMovement _playerMovement;

        private bool _jumpRequired = true;
        
        public FallingState(PlayerManager playerManager)
        {
            _playerManager = playerManager;
            _playerAnimator = playerManager.PlayerAnimator;
            _playerMovement = playerManager.PlayerMovement;
        }
        
        public override void Enter()
        {
            //_playerAnimator.SetBool(PlayerAnimation.Jumping, true);
            //_playerAnimator.SetTrigger(PlayerAnimation.TriggerJump);
        }

        public override void Exit()
        {
            //_playerAnimator.SetBool(PlayerAnimation.Jumping, false);
        }

        public override void Update()
        {
            if (Mathf.Abs(_playerMovement.PlayerRigidbody2D.velocity.y) < 0.01f)
            {
                _playerManager.StateMachine.TransitionTo(new IdleState(_playerManager));
            }
        }

        public override void FixedUpdate()
        {
            if (_jumpRequired)
            {
                _playerMovement.ApplyJump();
                _jumpRequired = false;
            }
            _playerMovement.ApplyHorizontalVelocity();
        }
    }
}