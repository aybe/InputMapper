using System.Globalization;
using Assets.InputMapper.Devices;
using Assets.InputMapper.Maths;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.InputMapper
{
    /// <summary>
    ///     Demo showing the response of a binding to an analog keyboard device.
    /// </summary>
    public class InputWizardSampleAnalog : MonoBehaviour
    {
        private AnalogKeyboardButton _button;
        private bool _buttonPressed;
        private float _buttonValue;
        private PowerEasing _easing;
        private Slider _uiSlider;
        private Text _uiText;

        public EasingMode Mode;
        public float Power;

        public void Reset()
        {
            Mode = EasingMode.InOut;
            Power = 2.0f;
        }

        public void Start()
        {
            _easing = new PowerEasing {Mode = EasingMode.InOut};

            _button = new AnalogKeyboardButton(KeyboardKey.RightControl, _easing);

            var panel = GameObject.Find("Panel");
            _uiSlider = panel.transform.GetComponentInChildren<Slider>();
            _uiText = panel.transform.GetComponentInChildren<Text>();
        }

        public void Update()
        {
            _easing.Mode = Mode;
            _easing.Power = Power;
            _button.Update(Time.deltaTime);

            _buttonPressed = _button.IsPressed();
            _buttonValue = _button.GetValue();
        }

        public void OnGUI()
        {
            GUILayout.Label("Analog keyboard button");
            GUILayout.Label("Key: " + _button.Key);
            GUILayout.Label("EasingMode: " + Mode);
            GUILayout.Label("IsPressed: " + _buttonPressed);
            GUILayout.Label("Value: " + _buttonValue);
            _uiSlider.value = _buttonValue;
            _uiText.text = _buttonValue.ToString(CultureInfo.InvariantCulture);
        }
    }
}