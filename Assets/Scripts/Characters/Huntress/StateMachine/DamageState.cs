using System.Collections;
using System.Linq;
using Lib.StateMachine;
using UnityEngine;

namespace Characters.Huntress.StateMachine
{
    public class DamageState: AbstractState
    {
        private readonly HuntressManager _huntressManager;
        private readonly Transform _source;
        private bool takenDamage = false;

        public DamageState(HuntressManager huntressManager, Transform source)
        {
            _huntressManager = huntressManager;
            _source = source;
        }
        
        public override void Enter()
        {
            _huntressManager.huntressAnimator.SetTrigger(HuntressAnimation.Damage);
            //_huntressManager.huntressMovement
        }

        public override void Exit()
        {
            //
        }

        public override void Update()
        {
            if (takenDamage && !_huntressManager.huntressAnimator.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
            {
                _huntressManager.huntressStateMachine.TransitionTo(new FollowState(_huntressManager, _source));
            }
        }

        public override void FixedUpdate()
        {
            if (takenDamage)
            {
                return;
            }

            takenDamage = true;
            bool facingSource = _source.position.x - _huntressManager.center.transform.position.x < 0;
            _huntressManager.huntressMovement.huntressRigidBody.AddForce(new Vector2(facingSource ? 3 : -3, 5), ForceMode2D.Impulse);
        }
        
        public new bool Is(string assertion)
        {
            return new[] { "FlipStuck" }.Contains(assertion);
        }

        public new bool AllowTransition(AbstractState nextAbstractState)
        {
            throw new System.NotImplementedException();
        }
    }
}