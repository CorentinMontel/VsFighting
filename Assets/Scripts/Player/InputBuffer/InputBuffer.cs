using System.Collections.Generic;
using Player.InputBuffer.Actions;
using UnityEngine;

namespace Player.InputBuffer
{
    public class InputBuffer
    {
        private InputAction nextAction = null;

        public void Enqueue(InputAction action, List<string> excludes)
        {
            if (action.isEmpty)
            {
                return;
            }

            if (action.GetActionValue(ActionEnum.Jump) && ExcludesContains(ActionEnum.Jump, excludes))
            {
                action.SetActionValue(ActionEnum.Jump, false);
            }
            
            if (action.GetActionValue(ActionEnum.Slide) && ExcludesContains(ActionEnum.Slide, excludes))
            {
                action.SetActionValue(ActionEnum.Jump, false);
            }
            
            if (action.GetActionValue(ActionEnum.SimpleAttack) && ExcludesContains(ActionEnum.SimpleAttack, excludes))
            {
                action.SetActionValue(ActionEnum.SimpleAttack, false);
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

        private bool ExcludesContains(ActionEnum action, List<string> excludes)
        {
            return excludes != null && excludes.Contains(action.Value);
        }
    }
}