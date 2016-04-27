using System.Collections.Generic;
using System.Linq;
using Assets.InputMapper.Devices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.InputMapper
{
    /// <summary>
    ///     Demo showing the input wizard mapping multiple devices on some commands.
    /// </summary>
    public sealed class InputWizardSample : MonoBehaviour
    {
        private const string SceneName = "InputMapper/InputWizard";
        private InputConfiguration _configuration;
        private bool _configured;

        private void Start()
        {
            // load scene and wait for it to be loaded
            SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        }

        private void Update()
        {
            // run once
            if (_configured)
            {
                return;
            }

            var scene = SceneManager.GetSceneByName(SceneName);
            if (!scene.isLoaded) return;

            // get the input wizard object
            var objects = scene.GetRootGameObjects();
            var o = objects.SingleOrDefault(s => s.name == "InputWizard");
            if (o == null) return;

            // create a configuration with two commands and configure wizard
            var commands = new List<Command>
            {
                new AxisCommand("Strafe", "Left", "Right"),
                new ButtonCommand("Jump")
            };
            var configuration = new InputConfiguration(commands);
            var devices = new HashSet<IDevice> {new AnalogKeyboardDevice(), new XboxPadDevice()};

            // push a prefab to use for buttons
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/InputMapper/InputWizardButton.prefab");

            // and configure the configuration !
            var wizard = o.GetComponent<InputWizard>();
            wizard.Configure(devices, configuration, prefab);

            _configuration = configuration;

            _configured = true;
        }

        private void FixedUpdate()
        {
            // analog buttons must be updated at each frame to behave properly
            if (_configuration != null) _configuration.Update(Time.deltaTime);
        }
    }
}