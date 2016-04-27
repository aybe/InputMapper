using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.InputMapper
{
    /// <summary>
    ///     Represents an input wizard button, manages/presents a command button.
    /// </summary>
    public sealed class InputWizardButton : MonoBehaviour
    {
        #region Fields

        private EventSystem _eventSystem;
        private string _targetButton;
        private Command _targetCommand;
        private HashSet<IDevice> _targetDevices;
        private InputWizard _wizard;

        #endregion

        #region Unity

        [UsedImplicitly]
        public void OnEnable()
        {
            _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        }

        [UsedImplicitly]
        public void Update()
        {
            UpdateButtonBindings();
        }

        #endregion

        #region Methods

        public void Setup([NotNull] InputWizard wizard, [NotNull] HashSet<IDevice> devices,
            [NotNull] Command targetCommand, [NotNull] string targetButton)
        {
            if (wizard == null) throw new ArgumentNullException("wizard");
            if (devices == null) throw new ArgumentNullException("devices");
            if (targetCommand == null) throw new ArgumentNullException("targetCommand");
            if (targetButton == null) throw new ArgumentNullException("targetButton");

            _wizard = wizard;
            _targetDevices = devices;
            _targetCommand = targetCommand;
            _targetButton = targetButton;

            UpdateButtonText();

            var button = GetComponent<Button>();
            button.onClick.AddListener(() => OnClick());
        }

        /// <summary>
        ///     Gets the text for a prefab button, e.g. 'Strafe, Left'.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="button"></param>
        /// <returns></returns>
        private static string GetPrefabText([NotNull] Command command, [NotNull] string button)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (button == null) throw new ArgumentNullException("button");

            var list = new List<string>();

            var commandName = string.IsNullOrEmpty(button)
                ? string.Format("<b>{0}</b>", command.Name)
                : string.Format("<b>{0}</b>, <i>{1}</i>", command.Name, button);
            list.Add(string.Format("{0}", commandName));

            // command buttons
            var buttonIndex = command.GetButtonIndex(button);
            var buttons = command.Buttons[buttonIndex];
            if (buttons.Any())
            {
                var descriptions = buttons.Select(s => s.GetDescription()).ToArray();
                var join = string.Join(", ", descriptions);
                var value = string.Format(@"{{{0}}}", join);
                list.Add(value);
            }
            var text = string.Join(Environment.NewLine, list.ToArray());
            return text;
        }

        private void OnClick()
        {
            StartCoroutine(GrabInput());
        }

        /// <summary>
        ///     Adds a binding to a command button by listening to first active button in a device store.
        /// </summary>
        /// <returns></returns>
        private IEnumerator GrabInput()
        {
            var buttonIndex = _targetCommand.GetButtonIndex(_targetButton);
            var buttons = _targetCommand.Buttons[buttonIndex];

            _wizard.SetPopupVisibility(true);

            // wait for no activity ! UX breaks returning right away otherwise
            while (_targetDevices.Any(s => s.GetActiveButton() != null))
                yield return new WaitForFixedUpdate();

            while (true)
            {
                foreach (var device in _targetDevices)
                {
                    // on active button update this instance
                    var active = device.GetActiveButton();
                    if (active == null) continue;
                    buttons.Add(active);
                    UpdateButtonText();

                    // 2nd catch, we must wait a bit before exit !
                    // not doing so would trigger this and others right away
                    yield return new WaitForFixedUpdate();
                    yield return new WaitForFixedUpdate();
                    _wizard.SetPopupVisibility(false);
                    yield break;
                }
                yield return new WaitForFixedUpdate();
            }
        }

        /// <summary>
        ///     Updates (clears) button bindings for this instance.
        /// </summary>
        private void UpdateButtonBindings()
        {
            var o = _eventSystem.currentSelectedGameObject;
            if (o == null || o != gameObject) return;
            if (!Input.GetKeyDown(KeyCode.Delete) && !Input.GetButtonDown("Cancel")) return;
            var buttonIndex = _targetCommand.GetButtonIndex(_targetButton);
            var buttons = _targetCommand.Buttons[buttonIndex];
            buttons.Clear();
            UpdateButtonText();
        }

        /// <summary>
        ///     Updates button text with a nice string showing current bindings.
        /// </summary>
        private void UpdateButtonText()
        {
            var text = GetPrefabText(_targetCommand, _targetButton);
            var children = GetComponentInChildren<Text>();
            children.text = text;
        }

        #endregion
    }
}