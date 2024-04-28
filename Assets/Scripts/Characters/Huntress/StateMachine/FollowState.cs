using System.Collections;
using Lib.StateMachine;
using UnityEngine;

namespace Characters.Huntress.StateMachine
{
    public class FollowState: AbstractState
    {
        private readonly HuntressManager _huntressManager;
        private readonly Transform _target;
        
        private float _huntressVelocity = 0f;

        public FollowState(HuntressManager huntressManager, Transform target)
        {
            _huntressManager = huntressManager;
            _target = target;
        }
        
        public override void Enter()
        {
            //
        }

        public override void Exit()
        {
            //
        }

        public override void Update()
        {
            Vector2 source = _huntressManager.huntressMovement.transform.position;
            
            float distanceToPoint = _target.position.x - source.x;

            // If huntress near player, stop follow
            if (Mathf.Abs(distanceToPoint) < 2)
            {
                _huntressVelocity = 0;
                _huntressManager.huntressStateMachine.TransitionTo(new DecisionState(_huntressManager, _target));
                return;
            }
            
            _huntressVelocity = distanceToPoint > 0 ? _huntressManager.patrolSpeed : -_huntressManager.patrolSpeed;
            _huntressManager.huntressAnimator.SetFloat(HuntressAnimation.HorizontalMovement, _huntressVelocity);
        }

        public override void FixedUpdate()
        {
            if (!_huntressManager.grounded)
            {
                return;
            }
            _huntressManager.huntressMovement.ApplyMovement(new Vector2(_huntressVelocity, 0));
        }
    }
}