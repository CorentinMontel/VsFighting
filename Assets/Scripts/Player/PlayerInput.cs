using System;
using System.Collections.Generic;
using System.IO;
using Player.InputBuffer;
using UnityEngine;

namespace Player
{
    public class PlayerInput
    {
        private static PlayerInput _instance = null;

        private Dictionary<string, KeyAttribution> availableKeys = new Dictionary<string, KeyAttribution>();

        private InputBuffer.InputBuffer _inputBuffer;
        private InputAction currentInput = null;
        private InputAction bufferedInput = null;

        private bool _buffering = false;
        private List<string> _excludes = new();

        public PlayerInput()
        {
            LoadKeyMap();
            _inputBuffer = new InputBuffer.InputBuffer(this);
        }

        private void LoadKeyMap()
        {
            string cwd = Directory.GetCurrentDirectory();
            string content = File.ReadAllText(cwd + "/Assets/Config/KeyMap.json");
            var decoded = JsonUtility.FromJson<KeyMap>(content);

            foreach (KeyAttribution keyAttribution in decoded.Bindings)
            {
                keyAttribution.Load();
                availableKeys.Add(keyAttribution.Name, keyAttribution);
            }
        }
        
        public void EnableBuffering(List<string> excludes = null)
        {
            _buffering = true;
            _excludes = excludes;
        }

        public void DisableBuffering()
        {
            _buffering = false;
            _excludes = new ();
        }

        public void Update()
        {
            // Create current input representation
            currentInput = InputAction.FromInput(this);

            if (_buffering)
            {
                _inputBuffer.Enqueue(currentInput.Clone(), _excludes);
                return;
            }

            // Dequeue buffered input
            bufferedInput = _inputBuffer.Dequeue();

            // If current input isn't empty clear buffered input
            if (!currentInput.isEmpty)
            {
                bufferedInput = null;
            }

            // If buffered input is not null, replace current
            if (bufferedInput != null)
            {
                currentInput = bufferedInput;
                bufferedInput = null;
            }
        }
        
        public float GetHorizontalAxis()
        {
            return Input.GetAxis("Horizontal");
        }

        private bool GetKeyDown(string name, bool defaultValue, bool fromInputAction = true)
        {
            if (fromInputAction)
            {
                return defaultValue;
            }
            return Input.GetKeyDown(availableKeys[name].Code);
        }

        public bool IsJumping(bool fromInputAction = true)
        {
            return GetKeyDown("jump", currentInput?.isJumping ?? false, fromInputAction);
        }

        public bool IsSliding(bool fromInputAction = true)
        {
            return GetKeyDown("slide", currentInput?.isSliding ?? false, fromInputAction);
        }

        public bool IsSimpleAttack(bool fromInputAction = true)
        {
            return GetKeyDown("simple_attack", currentInput?.isSimpleAttack ?? false, fromInputAction);
        }

        public static PlayerInput GetSingletonInstance()
        {
            if (null == _instance)
            {
                _instance = new PlayerInput();
                Debug.LogWarning("PlayerInput Singleton instance loaded. Remove it before final build");
            }

            Debug.Log("PlayerInput Singleton instance given");
            return _instance;
        }
        
        [System.Serializable]
        public class KeyAttribution
        {
            public string Name;
            public string Key;
            public string Description;
            public KeyCode Code;

            public void Load()
            {
                KeyCode.TryParse(Key, out Code);
            }
        }
        
        [System.Serializable]
        public class KeyMap
        {
            public List<KeyAttribution> Bindings;
        }
    }
}