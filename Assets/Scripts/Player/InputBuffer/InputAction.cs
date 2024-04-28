using System;
using System.Collections.Generic;
using Player.InputBuffer.Actions;
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
        private List<AbstractAction> actions = null;

        public InputAction(float duration = DefaultDuration)
        {
            _expiresAt = Time.time + duration;
            //LoadActions();
        }

        /*private void LoadActions()
        {
            actions = new List<AbstractAction>
            {
                new JumpAction(),
                new SlideAction(),
                new SimpleAttackAction(),
            };
        }*/

        public bool IsExpired()
        {
            return Time.time > _expiresAt;
        }

        public static InputAction FromInput(PlayerInput playerInput, float duration = DefaultDuration)
        {
            InputAction instance = new InputAction(duration);

            /*foreach (AbstractAction action in instance.actions)
            {
                action.LoadValue(playerInput);
            }*/

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

        public InputAction Clone()
        {
            InputAction action = new();

            action.isEmpty = isEmpty;
            action.isJumping = isJumping;
            action.isSliding = isSliding;
            action.isSimpleAttack = isSimpleAttack;

            return action;
        }
    }
}