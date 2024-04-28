using UnityEngine;

namespace Player
{
    public class PlayerAnimation
    {
        public static readonly int Running = Animator.StringToHash("Running");
        public static readonly int Jumping = Animator.StringToHash("Jumping");
        public static readonly int TriggerJump = Animator.StringToHash("TriggerJump");
        public static readonly int Grounded = Animator.StringToHash("Grounded");
        public static readonly int FallVelocity = Animator.StringToHash("FallVelocity");
        public static readonly int TriggerSimpleAttack = Animator.StringToHash("TriggerSimpleAttack");
        public static readonly int Damage = Animator.StringToHash("Damage");
        public static readonly int Sliding = Animator.StringToHash("Sliding");
        public static readonly int Frozen = Animator.StringToHash("Frozen"); 
    }
}