using Lib.Character;
using Lib.StateMachine;
using Lib.Tools;
using UnityEngine;

namespace Characters.Huntress.StateMachine
{
    public class LightAttackState: AbstractState
    {
        private readonly HuntressManager _huntressManager;
        private readonly Transform _target;
        private readonly Animator _huntressAnimator;
        
        public LightAttackState(HuntressManager huntressManager, Transform target)
        {
            _huntressManager = huntressManager;
            _huntressAnimator = huntressManager.huntressAnimator;
            _target = target;
        }
        
        public override void Enter()
        {
            _huntressManager.huntressRenderer.transform.localScale = new Vector3(
                DistanceHelper.IsFacing(_huntressManager.huntressMovement.transform, _target.transform) ? 1 : -1, 
                1, 
                1
            );
            _huntressAnimator.SetTrigger(HuntressAnimation.LightAttack1);
            _huntressManager.SwordHitbox.attack = new Attack(20, _huntressManager.transform);
        }

        public override void Exit()
        {
            //
        }

        public override void Update()
        {
            if (!_huntressAnimator.GetCurrentAnimatorStateInfo(0).IsName("LightAttack"))
            {
                _huntressManager.huntressStateMachine.TransitionTo(new DecisionState(_huntressManager, _target));
            }
        }

        public override void FixedUpdate()
        {
            //
        }
    }
}