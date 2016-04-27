using JetBrains.Annotations;
using UnityEngine;

namespace Assets.InputMapper.Devices
{
    /// <summary>
    ///     Represents a button on a keyboard device.
    /// </summary>
    public class KeyboardButton : IDeviceButton
    {
        /// <summary>
        ///     Constructor for serialization, use <see cref="KeyboardButton(KeyboardKey)" /> instead.
        /// </summary>
        [UsedImplicitly]
        public KeyboardButton()
        {
        }

        public KeyboardButton(KeyboardKey key)
        {
            Key = key;
        }

        public KeyboardKey Key { get; protected set; }

        public virtual string GetDescription()
        {
            return string.Format("<b>Key</b> <i>'{0}'</i>", Key);
        }

        public virtual float GetValue()
        {
            var key = Input.GetKey((KeyCode) Key);
            var value = key ? 1.0f : 0.0f;
            return value;
        }

        public virtual bool IsPressed()
        {
            var value = GetValue();
            var isPressed = value > 0.0f;
            return isPressed;
        }

        public virtual void Update(float deltaTime)
        {
        }

        public override string ToString()
        {
            return string.Format("Key: {0}", Key);
        }

        #region Equality members

        protected bool Equals(KeyboardButton other)
        {
            return Key == other.Key;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((KeyboardButton) obj);
        }

        public override int GetHashCode()
        {
            return (int) Key;
        }

        public static bool operator ==(KeyboardButton left, KeyboardButton right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(KeyboardButton left, KeyboardButton right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}