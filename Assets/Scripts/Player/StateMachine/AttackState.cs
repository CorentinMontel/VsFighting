using Lib.Character;
using Lib.StateMachine;
using UnityEngine;

namespace Player.StateMachine
{
    public class AttackState : AbstractState
    {
        private PlayerManager _playerManager;
        private Animator _playerAnimator;
        private PlayerMovement _playerMovement;
        
        public AttackState(PlayerManager playerManager)
        {
            _playerManager = playerManager;
            _playerAnimator = playerManager.PlayerAnimator;
            _playerMovement = playerManager.PlayerMovement;
        }
        
        public override void Enter()
        {
            _playerAnimator.SetTrigger(PlayerAnimation.TriggerSimpleAttack);
            _playerMovement.ApplyHorizontalVelocity();
            _playerManager.ConsumeEndurance(30);
            _playerMovement.SwordHitbox.attack = new Attack(20, _playerManager.transform);
        }

        public override void Exit()
        {
            _playerMovement.SwordHitbox.attack = null;
        }

        public override void Update()
        {
            if (!_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("SimpleAttack"))
            {
                _playerManager.StateMachine.TransitionTo(new IdleState(_playerManager));
            }
        }

        public override void FixedUpdate()
        {
            //
        }
    }
}