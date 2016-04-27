using System.Collections.Generic;

namespace Assets.InputMapper
{
    /// <summary>
    ///     Defines a device with buttons.
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        ///     Gets the name of this device.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets the buttons of this device.
        /// </summary>
        IEnumerable<IDeviceButton> Buttons { get; }

        /// <summary>
        ///     Gets the first button that is currently active.
        /// </summary>
        /// <returns>The first button that is active, <c>null</c> otherwise.</returns>
        IDeviceButton GetActiveButton();
    }
}