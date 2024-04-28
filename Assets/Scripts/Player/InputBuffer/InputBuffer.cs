using System.Collections.Generic;
using UnityEngine;

namespace Player.InputBuffer
{
    public class InputBuffer
    {
        private readonly PlayerInput _playerInput;
        private InputAction nextAction = null;
        
        public InputBuffer(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        public void Enqueue(InputAction action, List<string> excludes)
        {
            if (action.isEmpty)
            {
                return;
            }

            if (action.isJumping && excludes != null && excludes.Contains("jump"))
            {
                action.isJumping = false;
            }
            nextAction = action;
        }

        public InputAction Dequeue()
        {
            if (null == nextAction)
            {
                return null;
            }
            
            if (nextAction.IsExpired())
            {
                ResetBuffer();
            }
            
            InputAction action = nextAction;
            ResetBuffer();
            return action;
        }

        private void ResetBuffer()
        {
            nextAction = null;
        }
    }
}