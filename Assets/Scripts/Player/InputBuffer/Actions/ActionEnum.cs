namespace Player.InputBuffer.Actions
{
    public class ActionEnum
    {
        public string Value { get; private set; }
        
        private ActionEnum(string value)
        {
            Value = value;
        }
        
        public static ActionEnum Jump = new("jump");
        public static ActionEnum Slide = new("slide");
        public static ActionEnum SimpleAttack = new("simple_attack");

        public override string ToString()
        {
            return Value;
        }
    }
}