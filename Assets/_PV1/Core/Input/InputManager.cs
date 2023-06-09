using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PV.Input
{
    [Serializable]
    public class InputManager : Core.Behaviour
    {
        public static InputManager Instance {
            get => s_Instance;
            set
            {
                if (s_Instance != null)
                    return;

                s_Instance = value;
            }
        }

        private static InputManager s_Instance = null;

        [SerializeField] private List<InputActionAsset> m_InputActionAssets = new List<InputActionAsset>();

        private List<InputAction> m_InputActions = new List<InputAction>();

        public override void OnAwake()
        {
            Instance = this;
            if (Instance != this)
                return;

            foreach (var inputAsset in m_InputActionAssets) {
                foreach (var actionMap in inputAsset.actionMaps.ToArray()) {
                    foreach (var binding in actionMap.actions) {
                        m_InputActions.Add(binding);
                    }
                }

                inputAsset.Enable();
            }
        }

        public override void OnDestroy()
        {
            if (s_Instance == this)
                s_Instance = null;
        }

        public void Subscribe(params Action<InputAction.CallbackContext>[] inputActionEvents)
        {
            if (inputActionEvents.Length == 0)
                throw new ArgumentNullException("InputManager: ", new Exception("Input action parameters are empty, failed to subscribe to input action"));

            foreach (var action in inputActionEvents) {
                if (action != null) {

                    InputAction inputAction = GetInputAction(action.Method.Name);
                    if (inputAction != null) {
                        inputAction.performed += action;
                        inputAction.canceled += action;
                        continue;
                    }

                    Debug.LogError("Could not subscribe to input action with action: " + action.Method.Name);
                }
            }
        }


        public void Unsubscribe(params Action<InputAction.CallbackContext>[] inputActionEvents)
        {
            if (inputActionEvents.Length == 0)
                throw new ArgumentNullException("InputManager: ", new Exception("Input action parameters are empty, failed to unsubscribe to input action"));

            foreach (var action in inputActionEvents) {
                if (action != null) {

                    InputAction inputAction = GetInputAction(action.Method.Name);
                    if (inputAction != null) {
                        inputAction.performed -= action;
                        inputAction.canceled -= action;
                        continue;
                    }

                    Debug.LogError("Could not unsubscribe to input action with action: " + action.Method.Name);
                }
            }
        }

        public void Enable(bool value, params Type[] actionAssetType)
        {
            if (actionAssetType.Length > 0) {
                for (int i = 0; i < actionAssetType.Length; i++) {
                    var asset = m_InputActionAssets.Find((asset) => asset.GetType() == actionAssetType[i]);
                    if (value) {
                        asset.Enable();
                        continue;
                    }
                    asset.Disable();
                }
                return;
            }

            foreach (var asset in m_InputActionAssets) {
                if (value) {
                    asset.Enable();
                    continue;
                }
                asset.Disable();
            }
        }

        public T ReadActionValue<T>(string inputActionName)
        {
            var action = GetInputAction(inputActionName);
            if (action != null) {
                return (T)action?.ReadValueAsObject();
            }
            return default;
        }

        private InputAction GetInputAction(string actionName)
        {
            var inputAction = m_InputActions.Find(act => {
                var name = MakeTypeName(act.name);
                return "On" + name == actionName;
            });

            if (inputAction != null) {
                return inputAction;
            }

            Debug.LogError("No InputAction found to subscribe to");
            return null;
        }

        #region Create typename methods
        /// <summary>
        /// Copied from Unity's <see cref="CSharpCodeHelpers"/> class
        /// </summary>
        private static string MakeIdentifier(string name, string suffix = "")
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (char.IsDigit(name[0]))
                name = "_" + name;

            // See if we have invalid characters in the name.
            var nameHasInvalidCharacters = false;
            for (var i = 0; i < name.Length; ++i) {
                var ch = name[i];
                if (!char.IsLetterOrDigit(ch) && ch != '_') {
                    nameHasInvalidCharacters = true;
                    break;
                }
            }

            // If so, create a new string where we remove them.
            if (nameHasInvalidCharacters) {
                var buffer = new StringBuilder();
                for (var i = 0; i < name.Length; ++i) {
                    var ch = name[i];
                    if (char.IsLetterOrDigit(ch) || ch == '_')
                        buffer.Append(ch);
                }

                name = buffer.ToString();
            }

            return name + suffix;
        }

        /// <summary>
        /// Copied from Unity's <see cref="CSharpCodeHelpers"/> class
        /// </summary>
        private static string MakeTypeName(string name, string suffix = "")
        {
            var symbolName = MakeIdentifier(name, suffix);
            if (char.IsLower(symbolName[0]))
                symbolName = char.ToUpper(symbolName[0]) + symbolName.Substring(1);
            return symbolName;
        }

        internal void Subscribe(object onInteract, object onEquipToggle)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
