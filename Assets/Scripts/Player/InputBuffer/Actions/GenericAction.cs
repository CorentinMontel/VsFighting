namespace Player.InputBuffer.Actions
{
    public class GenericAction: AbstractAction
    {
        public string name;

        protected override bool LoadValue(PlayerInput playerInput)
        {
            return false;
        }

        public override string GetName()
        {
            return name;
        }
    }
}