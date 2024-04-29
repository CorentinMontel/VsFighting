namespace Player.InputBuffer.Actions
{
    public class SlideAction: AbstractAction
    {
        protected override bool LoadValue(PlayerInput playerInput)
        {
            return playerInput.IsSliding(false);
        }

        public override string GetName()
        {
            return "slide";
        }
    }
}