using Lib.StateMachine;
using UnityEngine;

namespace Player.StateMachine
{
    public class IdleState : AbstractState
    {
        private Animator _playerAnimator;
        private PlayerMovement _playerMovement;
        private PlayerManager _playerManager;
        
        public IdleState(PlayerManager playerManager)
        {
            _playerManager = playerManager;
            _playerAnimator = playerManager.PlayerAnimator;
            _playerMovement = playerManager.PlayerMovement;
        }
        
        public override void Enter()
        {
            //Debug.Log("Enter IDLE");
        }

        public override void Exit()
        {
            //Debug.Log("Exit IDLE");
        }

        public override void Update()
        {
            if (Mathf.Abs(_playerMovement.horizontalMovement) > 0.01f)
            {
                _playerManager.StateMachine.TransitionTo(new RunningState(_playerManager));
            }
            
            if (_playerMovement.CanJump())
            {
                _playerManager.StateMachine.TransitionTo(new JumpingState(_playerManager));
            }
            
            if (_playerMovement.slideRequired)
            {
                _playerManager.StateMachine.TransitionTo(new SlidingState(_playerManager));
            }
            
            if (_playerMovement.simpleAttackRequired && !_playerManager.isEnduranceRegen)
            {
                _playerManager.StateMachine.TransitionTo(new AttackState(_playerManager));
            }
        }

        public override void FixedUpdate()
        {}
    }
}