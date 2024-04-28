using UnityEngine;

namespace Player.InputBuffer
{
    public class InputAction
    {
        public const float DefaultDuration = 0.5f;
        
        public bool isJumping = false;
        public bool isSliding = false;
        public bool isSimpleAttack = false;
        public bool isEmpty = false;
        
        private float _expiresAt;

        public InputAction(float duration = DefaultDuration)
        {
            _expiresAt = Time.time + duration;
        }

        public bool IsExpired()
        {
            return Time.time > _expiresAt;
        }

        public static InputAction FromInput(PlayerInput playerInput, float duration = DefaultDuration)
        {
            InputAction instance = new InputAction(duration);
            
            instance.isJumping = playerInput.IsJumping(false);
            instance.isSliding = playerInput.IsSliding(false);
            instance.isSimpleAttack = playerInput.IsSimpleAttack(false);

            instance.ComputeEmpty();

            return instance;
        }

        public void ComputeEmpty()
        {
            isEmpty = !(isSliding || isSimpleAttack || isJumping);
        }
    }
}