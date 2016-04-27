using System;
using JetBrains.Annotations;
using XInputDotNetPure;

namespace Assets.InputMapper.Devices
{
    /// <summary>
    ///     Represents a D-Pad button on a XBOX pad.
    /// </summary>
    public sealed class XboxPadDPadButton : IDeviceButton
    {
        /// <summary>
        ///     Constructor for serialization, use <see cref="XboxPadDPadButton(XboxPadPlayer, XboxPadDPad)" /> instead.
        /// </summary>
        [UsedImplicitly]
        public XboxPadDPadButton()
        {
        }

        public XboxPadDPadButton(XboxPadPlayer player, XboxPadDPad dPad)
        {
            Player = player;
            DPad = dPad;
        }

        public XboxPadPlayer Player { get; private set; }
        public XboxPadDPad DPad { get; private set; }

        public string GetDescription()
        {
            var player = (int) Player + 1;
            return string.Format("<b>Xbox pad {0}</b> <i>'D-Pad {1}'</i>", player, DPad);
        }

        public float GetValue()
        {
            var padState = GamePad.GetState((PlayerIndex) Player);
            var dPad = padState.DPad;
            ButtonState buttonState;
            switch (DPad)
            {
                case XboxPadDPad.Up:
                    buttonState = dPad.Up;
                    break;
                case XboxPadDPad.Down:
                    buttonState = dPad.Down;
                    break;
                case XboxPadDPad.Left:
                    buttonState = dPad.Left;
                    break;
                case XboxPadDPad.Right:
                    buttonState = dPad.Right;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var value = buttonState == ButtonState.Pressed ? 1.0f : 0.0f;
            return value;
        }

        public bool IsPressed()
        {
            var value = GetValue();
            var isPressed = value > 0.0f;
            return isPressed;
        }

        public void Update(float deltaTime)
        {
        }

        public override string ToString()
        {
            return string.Format("Player: {0}, DPad: {1}", Player, DPad);
        }

        #region Equality members

        private bool Equals(XboxPadDPadButton other)
        {
            return Player == other.Player && DPad == other.DPad;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is XboxPadDPadButton && Equals((XboxPadDPadButton) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Player*397) ^ (int) DPad;
            }
        }

        public static bool operator ==(XboxPadDPadButton left, XboxPadDPadButton right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(XboxPadDPadButton left, XboxPadDPadButton right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}