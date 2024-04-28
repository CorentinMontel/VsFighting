using Lib.StateMachine;

namespace Characters.Huntress.StateMachine
{
    public class IdleState: AbstractState
    {
        private readonly HuntressManager _huntressManager;

        public IdleState(HuntressManager huntressManager)
        {
            _huntressManager = huntressManager;
        }
        
        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            //
        }

        public override void Update()
        {
            //
        }

        public override void FixedUpdate()
        {
            //
        }
    }
}