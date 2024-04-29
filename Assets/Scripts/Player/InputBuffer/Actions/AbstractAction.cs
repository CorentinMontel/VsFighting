namespace Player.InputBuffer.Actions
{
    public abstract class AbstractAction
    {
        protected bool value = false;

        protected abstract bool LoadValue(PlayerInput playerInput);

        public void Load(PlayerInput playerInput)
        {
            value = LoadValue(playerInput);
        }

        public bool GetValue()
        {
            return value;
        }
        
        public void SetValue(bool newValue)
        {
            value = newValue;
        }

        public bool IsEmpty()
        {
            return !value;
        }

        public AbstractAction Clone()
        {
            GenericAction action = new GenericAction();
            action.value = value;
            action.name = GetName();

            return action;
        }

        public abstract string GetName();
    }
}