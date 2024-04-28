namespace Lib.StateMachine
{
    public abstract class AbstractState
    {
        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update();
        
        public abstract void FixedUpdate();

        public bool Is(string assertion)
        {
            return false;
        }

        public virtual bool AllowTransition(AbstractState nextAbstractState)
        {
            return true;
        }
    }
}