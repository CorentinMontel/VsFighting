using Lib.StateMachine;
using UnityEngine;

namespace Player.StateMachine
{
    public class RunningState : AbstractState
    {
        private PlayerManager _playerManager;
        private Animator _playerAnimator;
        private PlayerMovement _playerMovement;
        
        public RunningState(PlayerManager playerManager)
        {
            _playerManager = playerManager;
            _playerAnimator = playerManager.PlayerAnimator;
            _playerMovement = playerManager.PlayerMovement;
        }
        
        public override void Enter()
        {
            _playerAnimator.SetBool(PlayerAnimation.Running, true);
            _playerMovement.ApplyHorizontalVelocity();
        }

        public override void Exit()
        {
            _playerAnimator.SetBool(PlayerAnimation.Running, false);
        }

        public override void Update()
        {
            if (Mathf.Abs(_playerMovement.horizontalMovement) < 0.01f)
            {
                _playerManager.StateMachine.TransitionTo(new IdleState(_playerManager));
            }

            if (_playerMovement.slideRequired)
            {
                _playerManager.StateMachine.TransitionTo(new SlidingState(_playerManager));
            }

            if (_playerMovement.CanJump())
            {
                _playerManager.StateMachine.TransitionTo(new JumpingState(_playerManager));
            }

            if (_playerMovement.simpleAttackRequired &&  !_playerManager.isEnduranceRegen)
            {
                _playerManager.StateMachine.TransitionTo(new AttackState(_playerManager));
            }
        }

        public override void FixedUpdate()
        {
            _playerMovement.ApplyHorizontalVelocity();
        }
    }
}