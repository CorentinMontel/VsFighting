namespace Player.InputBuffer.Actions
{
    public class SimpleAttackAction: AbstractAction
    {
        protected override bool LoadValue(PlayerInput playerInput)
        {
            return playerInput.IsSimpleAttack(false);
        }

        public override string GetName()
        {
            return "simple_attack";
        }
    }
}