using System.Collections;
using System.Collections.Generic;
using Assets.InputMapper.Devices;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.InputMapper
{
    /// <summary>
    ///     Demo showing the input wizard mapping multiple devices on some commands.
    /// </summary>
    [UsedImplicitly]
    public sealed class InputWizardSample : MonoBehaviour
    {
        private const string InputWizardScene = "InputMapper/InputWizard";
        private const string InputWizardSampleScene = "InputMapper/InputWizardSample";

        private static bool Launched { get; set; }

        [UsedImplicitly]
        public IEnumerator Start()
        {
            // since we use this scene as the return scene
            // here a little guard to prevent an infinite loop
            if (Launched) yield break;

            /* setup the wizard */

            // configuration, devices, return scene

            var configuration = new InputConfiguration(new List<Command>
            {
                new AxisCommand("Strafe", "Left", "Right"),
                new ButtonCommand("Jump")
            });
            InputWizard.Configuration = configuration;

            var devices = new HashSet<IDevice> {new AnalogKeyboardDevice(), new XboxPadDevice()};
            InputWizard.Devices = devices;

            var scene = InputWizardSampleScene;
            InputWizard.Scene = scene;


            // prefabs

            var prefabButton = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/InputMapper/InputWizardButton.prefab");
            InputWizard.PrefabButton = prefabButton;

            var prefabExit = Instantiate(prefabButton);
            DontDestroyOnLoad(prefabExit); // awkard Unity ...
            prefabExit.GetComponentInChildren<Text>().text = "Return to menu";
            prefabExit.GetComponent<Button>().colors = new ColorBlock // red button
            {
                colorMultiplier = 1.0f,
                fadeDuration = 0.1f,
                normalColor = new Color(0.28f, 0.0f, 0.0f),
                highlightedColor = new Color(0.45f, 0.0f, 0.0f),
                pressedColor = new Color(0.38f, 0.0f, 0.0f),
                disabledColor = new Color(0.19f, 0.0f, 0.0f)
            };
            InputWizard.PrefabExit = prefabExit;

            // signal that we've run once !
            Launched = true;

            // load our scene wizard
            var async = SceneManager.LoadSceneAsync(InputWizardScene, LoadSceneMode.Single);
            yield return async;
        }
    }
}