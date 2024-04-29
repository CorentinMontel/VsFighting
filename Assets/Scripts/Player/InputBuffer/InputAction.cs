using System;
using System.Collections.Generic;
using Player.InputBuffer.Actions;
using Unity.VisualScripting;
using UnityEngine;

namespace Player.InputBuffer
{
    public class InputAction
    {
        public const float DefaultDuration = 0.5f;

        /*public bool isJumping = false;
        public bool isSliding = false;
        public bool isSimpleAttack = false;*/
        public bool isEmpty = false;

        private float _expiresAt;
        private List<AbstractAction> actions = null;

        public InputAction(float duration = DefaultDuration, bool loadEmpty = false)
        {
            _expiresAt = Time.time + duration;

            if (!loadEmpty)
            {
                LoadActions();
            }
            else
            {
                actions = new List<AbstractAction>();
            }
        }

        private void LoadActions()
        {
            actions = new List<AbstractAction>
            {
                new JumpAction(),
                new SlideAction(),
                new SimpleAttackAction(),
            };
        }

        public bool IsExpired()
        {
            return Time.time > _expiresAt;
        }

        public static InputAction FromInput(PlayerInput playerInput, float duration = DefaultDuration)
        {
            InputAction instance = new InputAction(duration);

            foreach (AbstractAction action in instance.actions)
            {
                action.Load(playerInput);
            }

            instance.ComputeEmpty();

            return instance;
        }

        public bool GetActionValue(ActionEnum actionName)
        {
            foreach (AbstractAction action in actions)
            {
                if (action.GetName() == actionName.Value)
                {
                    return action.GetValue();
                }
            }

            return false;
        }
        
        public void SetActionValue(ActionEnum actionName, bool value)
        {
            foreach (AbstractAction action in actions)
            {
                if (action.GetName() == actionName.Value)
                {
                    action.SetValue(value);
                    break;
                }
            }
        }

        public void ComputeEmpty()
        {
            foreach (AbstractAction action in actions)
            {
                if (!action.IsEmpty())
                {
                    isEmpty = false;
                    return;
                }
            }
            isEmpty = true;
        }

        public InputAction Clone()
        {
            InputAction newAction = new(loadEmpty: true);

            newAction.isEmpty = isEmpty;

            foreach (AbstractAction action in actions)
            {
                newAction.actions.Add(action.Clone());
            }

            return newAction;
        }

        public override string ToString()
        {
            List<string> actions = new List<string>();
            foreach (AbstractAction action in this.actions)
            {
                actions.Add(action.GetName() + " = " + (action.GetValue() ? "TRUE" : "FALSE"));
            }
            return $"InputAction : {String.Join(", ", actions.ToArray())} - isEmpty = {(isEmpty ? "TRUE" : "FALSE")}";
        }
    }
}