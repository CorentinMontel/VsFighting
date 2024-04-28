using System.Collections.Generic;
using Lib.StateMachine;
using UnityEngine;

namespace Player.StateMachine
{
    public class JumpingState : AbstractState
    {
        private PlayerManager _playerManager;
        private Animator _playerAnimator;
        private PlayerMovement _playerMovement;

        private bool _jumpRequired = true;
        private int _currentJumps;
        
        public JumpingState(PlayerManager playerManager, int currentJumps = 1)
        {
            _playerManager = playerManager;
            _playerAnimator = playerManager.PlayerAnimator;
            _playerMovement = playerManager.PlayerMovement;
            _currentJumps = currentJumps;
        }
        
        public override void Enter()
        {
            _playerAnimator.SetBool(PlayerAnimation.Jumping, true);
            _playerAnimator.SetTrigger(PlayerAnimation.TriggerJump);

            if (_currentJumps >= _playerManager.maxJumps)
            {
                _playerManager.PlayerInput.EnableBuffering();
            }
            else
            {
                _playerManager.PlayerInput.EnableBuffering(new List<string>{"jump"});
            }
        }

        public override void Exit()
        {
            _playerAnimator.SetBool(PlayerAnimation.Jumping, false);
            
            _playerManager.PlayerInput.DisableBuffering();
        }

        public override void Update()
        {
            if (_jumpRequired)
            {
                return;
            }
            if (Mathf.Abs(_playerMovement.PlayerRigidbody2D.velocity.y) < 0.01f)
            {
                _playerManager.StateMachine.TransitionTo(new IdleState(_playerManager));
            }

            if (_playerManager.PlayerInput.IsJumping() && _currentJumps < _playerManager.maxJumps)
            {
                _playerManager.StateMachine.TransitionTo(new JumpingState(_playerManager, _currentJumps + 1));
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