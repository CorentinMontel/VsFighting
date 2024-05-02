using System;
using System.Collections.Generic;
using DefaultNamespace;
using Player.InputBuffer.Actions;
using UnityEngine;

namespace Player.InputBuffer
{
    public class InputAction
    {
        public bool isEmpty = false;

        private float _expiresAt;
        private Dictionary<ActionEnum, AbstractAction> actions = null;

        public InputAction(float duration = GameConfig.InputBufferDelaySec, bool loadEmpty = false)
        {
            _expiresAt = Time.time + duration;

            if (!loadEmpty)
            {
                LoadActions();
            }
            else
            {
                actions = new Dictionary<ActionEnum, AbstractAction>();
            }
        }

        private void LoadActions()
        {
            actions = new Dictionary<ActionEnum, AbstractAction>
            {
                { ActionEnum.Jump, new JumpAction() },
                { ActionEnum.Slide, new SlideAction() },
                { ActionEnum.SimpleAttack, new SimpleAttackAction() },
            };
        }

        public bool IsExpired()
        {
            return Time.time > _expiresAt;
        }

        public static InputAction FromInput(PlayerInput playerInput, float duration = GameConfig.InputBufferDelaySec)
        {
            InputAction instance = new InputAction(duration);

            foreach (AbstractAction action in instance.actions.Values)
            {
                action.Load(playerInput);
            }

            instance.ComputeEmpty();

            return instance;
        }

        public bool GetActionValue(ActionEnum actionName)
        {
            return actions.ContainsKey(actionName) && actions[actionName].GetValue();
        }
        
        public void SetActionValue(ActionEnum actionName, bool value)
        {
            if (!actions.ContainsKey(actionName))
            {
                return;
            }
            
            actions[actionName].SetValue(value);
        }

        public void ComputeEmpty()
        {
            foreach (AbstractAction action in actions.Values)
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

            foreach (ActionEnum actionName in actions.Keys)
            {
                newAction.actions.Add(actionName, actions[actionName].Clone());
            }

            return newAction;
        }

        public override string ToString()
        {
            List<string> actions = new List<string>();
            foreach (AbstractAction action in this.actions.Values)
            {
                actions.Add(action.GetName() + " = " + (action.GetValue() ? "TRUE" : "FALSE"));
            }
            return $"InputAction : {String.Join(", ", actions.ToArray())} - isEmpty = {(isEmpty ? "TRUE" : "FALSE")}";
        }
    }
}