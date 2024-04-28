using System;

namespace Lib.StateMachine
{
    public class StateMachine
    {
        public AbstractState CurrentState { get; protected set; }
        
        public event Action<AbstractState> _stateChanged;
        
        // exit this state and enter another
        public void TransitionTo(AbstractState nextAbstractState)
        {
            if (!CurrentState.AllowTransition(nextAbstractState))
            {
                return;
            }
            CurrentState.Exit();
            CurrentState = nextAbstractState;
            nextAbstractState.Enter();
	

            // notify other objects that state has changed
            _stateChanged?.Invoke(nextAbstractState);
        }
        
        public void Update()
        {
            if (CurrentState != null)
            {
                CurrentState.Update();
            }
        }
        
        public void FixedUpdate()
        {
            if (CurrentState != null)
            {
                CurrentState.FixedUpdate();
            }
        }
        
        public void Initialize(AbstractState abstractState)
        {
            CurrentState = abstractState;
            abstractState.Enter();
	

            // notify other objects that state has changed
            _stateChanged?.Invoke(abstractState);
        }
    }
}