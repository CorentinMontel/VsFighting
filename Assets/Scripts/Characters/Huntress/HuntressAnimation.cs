using UnityEngine;

namespace Characters.Huntress
{
    public class HuntressAnimation
    {
        public static readonly int HorizontalMovement = Animator.StringToHash("HorizontalMovement");
        public static readonly int VerticalMovement = Animator.StringToHash("VerticalMovement");
        public static readonly int Grounded = Animator.StringToHash("Grounded");
        public static readonly int LightAttack1 = Animator.StringToHash("LightAttack1");
        public static readonly int Death = Animator.StringToHash("Death");
        public static readonly int Damage = Animator.StringToHash("Damage");
    }
}