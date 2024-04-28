namespace Player.InputBuffer.Actions
{
    public abstract class AbstractAction
    {
        protected bool value = false;

        protected abstract bool GetValue(PlayerInput playerInput);

        public void LoadValue(PlayerInput playerInput)
        {
            value = GetValue(playerInput);
        }
    }
}