using System;
using System.Collections.Generic;

namespace Assets.InputMapper.Devices
{
    /// <summary>
    ///     Represents a XBOX pad device.
    /// </summary>
    public sealed class XboxPadDevice : IDevice
    {
        private static readonly List<IDeviceButton> Bindings;

        static XboxPadDevice()
        {
            // create available bindings
            var bindings = new List<IDeviceButton>();
            foreach (XboxPadPlayer player in Enum.GetValues(typeof(XboxPadPlayer)))
            {
                foreach (XboxPadDigital digital in Enum.GetValues(typeof(XboxPadDigital)))
                {
                    bindings.Add(new XboxPadDigitalButton(player, digital));
                }

                foreach (XboxPadDPad dPad in Enum.GetValues(typeof(XboxPadDPad)))
                {
                    bindings.Add(new XboxPadDPadButton(player, dPad));
                }

                foreach (XboxPadAxis axis in Enum.GetValues(typeof(XboxPadAxis)))
                {
                    foreach (XboxPadAxisDirection direction in Enum.GetValues(typeof(XboxPadAxisDirection)))
                    {
                        bindings.Add(new XboxPadAxisButton(player, axis, direction));
                    }
                }

                foreach (XboxPadTrigger trigger in Enum.GetValues(typeof(XboxPadTrigger)))
                {
                    bindings.Add(new XboxPadTriggerButton(player, trigger));
                }
            }
            Bindings = bindings;
        }

        public IEnumerable<IDeviceButton> Buttons
        {
            get { return Bindings; }
        }


        public string Name
        {
            get { return "XBOX pad"; }
        }

        public IDeviceButton GetActiveButton()
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