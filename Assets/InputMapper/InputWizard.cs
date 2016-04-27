using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.InputMapper
{
    /// <summary>
    ///     Represents an input wizard, an UI for mapping device buttons to commands.
    /// </summary>
    public sealed class InputWizard : MonoBehaviour
    {
        #region Fields

        private EventSystem _eventSystem;
        private GameObject _panelButtons;
        private GameObject _panelCommands;
        private GameObject _panelPopup;
        private GameObject _root;
        private GameObject _selection;

        #endregion

        #region Unity

        [UsedImplicitly]
        public void OnEnable()
        {
            _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            _panelButtons = GameObject.Find("Buttons");
            _panelCommands = GameObject.Find("Commands");
            _panelPopup = GameObject.Find("Popup");
            _root = GameObject.Find("Root");
        }

        [UsedImplicitly]
        public void Update()
        {
            UpdateSelection();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Configures this instance.
        /// </summary>
        /// <param name="devices">Device store to use for listening to input events.</param>
        /// <param name="configuration">Input configuration to map inputs to.</param>
        /// <param name="prefab">Prefab button to use for presenting command buttons.</param>
        public void Configure([NotNull] HashSet<IDevice> devices, [NotNull] InputConfiguration configuration,
            [NotNull] GameObject prefab)
        {
            if (devices == null) throw new ArgumentNullException("devices");
            if (configuration == null) throw new ArgumentNullException("configuration");
            if (prefab == null) throw new ArgumentNullException("prefab");

            // clean-up previous session
            var transforms = _panelButtons.transform.Cast<Transform>().ToList();
            transforms.ForEach(s => DestroyImmediate(s.gameObject));

            // create a UI element for each command button
            foreach (var command in configuration.Commands)
            {
                foreach (var label in command.Labels)
                {
                    // clone and add our prefab
                    var o = Instantiate(prefab);
                    o.name = string.Format("ButtonCommand{0}{1}", command.Name, label);
                    o.transform.SetParent(_panelButtons.transform, false);

                    // setup our scanner !
                    var button = o.AddComponent<InputWizardButton>();
                    button.Setup(this, devices, command, label);
                }
            }

            // initialize UI
            SetPopupVisibility(false);
        }

        /// <summary>
        ///     Sets the visibility of the panel that asks for user input.
        /// </summary>
        /// <param name="visible"></param>
        internal void SetPopupVisibility(bool visible)
        {
            if (visible)
            {
                // clear selection and put popup panel foremost
                _eventSystem.SetSelectedGameObject(null);
                _panelPopup.transform.SetAsLastSibling();
            }
            else
            {
                // put buttons panel foremost
                _panelCommands.transform.SetAsLastSibling();
            }
        }

        /// <summary>
        ///     Updates selection in UI: ensures something is always selected, restores selection after a panel switch.
        /// </summary>
        private void UpdateSelection()
        {
            // if no command is selected, select first one
            var transform1 = _panelButtons.transform;
            if (_selection == null && transform1.childCount > 0)
                _selection = transform1.GetChild(0).gameObject;

            // track last good known selection
            var o = _eventSystem.currentSelectedGameObject;
            if (o != null) _selection = o;

            // when commands are visible, restore last good known selection
            // at the same time, this ensures we always have something selected
            // thus, mouse can't break keyboard navigation anymore
            var transform2 = _root.transform;
            var count = transform2.childCount;
            if (count <= 0) return;
            var child = transform2.GetChild(count - 1);
            if (child.gameObject != _panelCommands) return;
            if (_eventSystem.currentSelectedGameObject == null)
                _eventSystem.SetSelectedGameObject(_selection);
        }

        #endregion
    }
}