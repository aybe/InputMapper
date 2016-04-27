namespace Assets.InputMapper
{
    /// <summary>
    ///     Defines a device button.
    /// </summary>
    public interface IDeviceButton
    {
        /// <summary>
        ///     Gets the button description.
        /// </summary>
        /// <returns></returns>
        string GetDescription();

        /// <summary>
        ///     Gets analog value for this button (see Remarks).
        /// </summary>
        /// <returns>A value between 0.0 and 1.0.</returns>
        /// <remarks>
        ///     When underlying button is an emulation such as an analog keyboard button, some easing function such as 'back'
        ///     will return a value outside the 0.0 to 1.0 range. Care should be taken if such easing is used within an axis
        ///     command, i.e. it will behave as if the opposite button of the axis command was pressed instead.
        /// </remarks>
        float GetValue();

        /// <summary>
        ///     Gets digital value for this button.
        /// </summary>
        /// <returns></returns>
        bool IsPressed();

        /// <summary>
        ///     Updates this button (see Remarks).
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <remarks>
        ///     A button such as an analog-emulated keyboard button must be updated to return an accurate value when it is
        ///     queried.
        /// </remarks>
        void Update(float deltaTime);
    }
}