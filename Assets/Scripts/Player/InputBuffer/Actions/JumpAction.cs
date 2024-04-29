namespace Player.InputBuffer.Actions
{
    public class JumpAction: AbstractAction
    {
        protected override bool LoadValue(PlayerInput playerInput)
        {
            return playerInput.IsJumping(false);
        }

        public override string GetName()
        {
            return "jump";
        }
    }
}