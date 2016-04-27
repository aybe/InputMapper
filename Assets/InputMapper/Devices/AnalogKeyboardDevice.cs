using System;
using System.Collections.Generic;

namespace Assets.InputMapper.Devices
{
    /// <summary>
    ///     Represents an analog keyboard device.
    /// </summary>
    public sealed class AnalogKeyboardDevice : KeyboardDevice
    {
        private static readonly List<IDeviceButton> Bindings;

        static AnalogKeyboardDevice()
        {
            // create available bindings
            var bindings = new List<IDeviceButton>();
            foreach (KeyboardKey key in Enum.GetValues(typeof(KeyboardKey)))
            {
                bindings.Add(new AnalogKeyboardButton(key));
            }
            Bindings = bindings;
        }

        public override IEnumerable<IDeviceButton> Buttons
        {
            get { return Bindings; }
        }

        public override string Name
        {
            get { return "Keyboard (analog)"; }
        }
    }
}