using System.Collections;
using DefaultNamespace;
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
            ComputeEnvironment();
            //_huntressManager.StartCoroutine(WaitForNextAction());
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                ComputeEnvironment();
            }
        }

        public override void FixedUpdate()
        {
            //
        }

        private void ComputeEnvironment()
        {
            /*
            Vector2 playerDirection = DistanceHelper.ComputeDirection(_huntressManager.huntressMovement.transform.position, _target.position);
            Vector2 facingDirection = new Vector2(_huntressManager.huntressMovement.transform.localScale.x, 0);
            
            Debug.DrawRay(_huntressManager.huntressMovement.transform.position, playerDirection * GameConfig.EnemyViewDistance, Color.blue, 10f);
            Debug.DrawRay(_huntressManager.huntressMovement.transform.position, facingDirection * GameConfig.EnemyViewDistance, Color.red, 10f);
            */

            int rayCount = 34;
            float step = (2 * Mathf.PI) / rayCount;
            for (float i = 0; i < 2 * Mathf.PI; i += step)
            {
                Vector2 vec = new Vector2(Mathf.Cos(i), Mathf.Sin(i));
                Debug.DrawRay(_huntressManager.huntressMovement.transform.position, vec * GameConfig.EnemyViewDistance, Color.blue, 10f);
            }
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