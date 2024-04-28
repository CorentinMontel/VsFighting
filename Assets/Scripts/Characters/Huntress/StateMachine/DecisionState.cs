using System.Collections;
using Lib.StateMachine;
using Lib.Tools;
using UnityEngine;

namespace Characters.Huntress.StateMachine
{
    public class DecisionState: AbstractState
    {
        private readonly HuntressManager _huntressManager;
        private readonly Transform _target;
        
        public DecisionState(HuntressManager huntressManager, Transform target)
        {
            _huntressManager = huntressManager;
            _target = target;
        }
        
        public override void Enter()
        {
            _huntressManager.StartCoroutine(WaitForNextAction());
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            //
        }

        public override void FixedUpdate()
        {
            //
        }

        private IEnumerator WaitForNextAction()
        {
            yield return new WaitForSeconds((float) Random.Range(5, 20) / 10);

            float distance = DistanceHelper.AbsoluteDistance(_target, _huntressManager.huntressMovement.transform);

            if (distance < _huntressManager.shortDistanceRange)
            {
                _huntressManager.huntressStateMachine.TransitionTo(new LightAttackState(_huntressManager, _target));
            }
            else
            {
                _huntressManager.huntressStateMachine.TransitionTo(new FollowState(_huntressManager, _target));
            }

            yield return null;
        }
    }
}