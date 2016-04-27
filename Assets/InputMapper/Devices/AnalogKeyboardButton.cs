using System;
using Assets.InputMapper.Maths;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.InputMapper.Devices
{
    /// <summary>
    ///     Represents a button on an analog keyboard device.
    /// </summary>
    public sealed class AnalogKeyboardButton : KeyboardButton
    {
        private float _time;

        /// <summary>
        ///     Constructor for serialization, use any of the others instead.
        /// </summary>
        [UsedImplicitly]
        public AnalogKeyboardButton()
        {
        }

        /// <summary>
        ///     Create a new instance of <see cref="AnalogKeyboardButton" /> that uses a <see cref="PowerEasing" />.
        /// </summary>
        /// <param name="key"></param>
        public AnalogKeyboardButton(KeyboardKey key) : this(key, new PowerEasing())
        {
        }

        /// <summary>
        ///     Create a new instance of <see cref="AnalogKeyboardButton" /> with a custom easing.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="easing"></param>
        public AnalogKeyboardButton(KeyboardKey key, Easing easing)
        {
            Key = key;
            Easing = easing;
        }

        /// <summary>
        ///     Gets or sets the easing function for emulating an analog feeling.
        /// </summary>
        public Easing Easing { get; set; }

        public override string GetDescription()
        {
            return string.Format("<b>Analog key</b> <i>'{0}'</i>", Key);
        }

        public override float GetValue()
        {
            if (Easing == null) throw new InvalidOperationException("Easing property is not set.");
            var ease = Easing.GetValue(_time);
            return (float) ease;
        }

        public override bool IsPressed()
        {
            var pressed = Input.GetKey((KeyCode) Key);
            return pressed;
        }

        public override void Update(float deltaTime)
        {
            float value;

            var pressed = IsPressed();
            if (pressed)
            {
                value = _time + deltaTime;
            }
            else
            {
                value = _time - deltaTime;
            }

            _time = Mathf.Clamp01(value);
        }

        public override string ToString()
        {
            return string.Format("Key: {0}", Key);
        }
    }
}