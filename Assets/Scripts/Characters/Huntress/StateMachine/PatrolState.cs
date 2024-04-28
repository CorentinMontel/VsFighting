using System.Collections;
using Lib.StateMachine;
using UnityEngine;

namespace Characters.Huntress.StateMachine
{
    public class PatrolState: AbstractState
    {
        private readonly HuntressManager _huntressManager;
        private readonly Transform[] _points;
        
        private int currentPointIndex = -1;
        private bool _changePoint = false;
        private float _huntressVelocity = 0f;

        public PatrolState(HuntressManager huntressManager, Transform[] points = null)
        {
            _huntressManager = huntressManager;
            _points = points;
        }
        
        public override void Enter()
        {
            if (_points is { Length: > 0 })
            {
                currentPointIndex = 0;
            }
        }

        public override void Exit()
        {
            //
        }

        public override void Update()
        {
            if (currentPointIndex == -1 || _changePoint)
            {
                return;
            }
            
            Vector2 source = _huntressManager.huntressMovement.transform.position;
            Vector2 target = GetCurrentPoint();
            
            float distanceToPoint = target.x - source.x;

            if (Mathf.Abs(distanceToPoint) < 0.2)
            {
                _changePoint = true;
                _huntressManager.StartCoroutine(ChangeState());
                return;
            }
            
            _huntressVelocity = distanceToPoint > 0 ? _huntressManager.patrolSpeed : -_huntressManager.patrolSpeed;
            _huntressManager.huntressAnimator.SetFloat(HuntressAnimation.HorizontalMovement, _huntressVelocity);
        }

        public override void FixedUpdate()
        {
            if (currentPointIndex == -1 || _changePoint)
            {
                return;
            }
            
            _huntressManager.huntressMovement.ApplyMovement(new Vector2(_huntressVelocity, 0));
        }

        private Vector2 GetCurrentPoint()
        {
            if (currentPointIndex != -1)
            {
                return _points[currentPointIndex].position;
            }

            return Vector2.zero;
        }

        private IEnumerator ChangeState()
        {
            yield return new WaitForSeconds(3f);
            currentPointIndex++;

            if (currentPointIndex >= _points.Length)
            {
                currentPointIndex = 0;
            }

            _changePoint = false;
        }
    }
}