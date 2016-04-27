using System;
using System.Collections.Generic;

namespace Assets.InputMapper.Devices
{
    /// <summary>
    ///     Represents a keyboard device.
    /// </summary>
    public class KeyboardDevice : IDevice
    {
        private static readonly List<IDeviceButton> Bindings;

        static KeyboardDevice()
        {
            // create available bindings
            var bindings = new List<IDeviceButton>();
            foreach (KeyboardKey key in Enum.GetValues(typeof(KeyboardKey)))
            {
                bindings.Add(new KeyboardButton(key));
            }
            Bindings = bindings;
        }

        public virtual IEnumerable<IDeviceButton> Buttons
        {
            get { return Bindings; }
        }

        public virtual string Name
        {
            get { return "Keyboard (digital)"; }
        }

        public virtual IDeviceButton GetActiveButton()
        {
            foreach (var binding in Bindings)
            {
                var isPressed = binding.IsPressed();
                if (isPressed)
                {
                    return binding;
                }
            }
            return null;
        }
    }
}