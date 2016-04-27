using System;
using JetBrains.Annotations;
using XInputDotNetPure;

namespace Assets.InputMapper.Devices
{
    /// <summary>
    ///     Represents a digital button on a XBOX pad.
    /// </summary>
    public sealed class XboxPadDigitalButton : IDeviceButton
    {
        /// <summary>
        ///     Constructor for serialization, use <see cref="XboxPadDigitalButton(XboxPadPlayer,XboxPadDigital)" /> instead.
        /// </summary>
        [UsedImplicitly]
        public XboxPadDigitalButton()
        {
        }

        public XboxPadDigitalButton(XboxPadPlayer player, XboxPadDigital digital)
        {
            Player = player;
            Digital = digital;
        }

        public XboxPadPlayer Player { get; private set; }
        public XboxPadDigital Digital { get; private set; }

        public string GetDescription()
        {
            var player = (int) Player + 1;
            string button;
            switch (Digital)
            {
                case XboxPadDigital.A:
                case XboxPadDigital.B:
                case XboxPadDigital.X:
                case XboxPadDigital.Y:
                case XboxPadDigital.Back:
                case XboxPadDigital.Start:
                case XboxPadDigital.Guide:
                    button = string.Format("Button {0}", Digital);
                    break;
                case XboxPadDigital.LeftStick:
                    button = "Left thumb";
                    break;
                case XboxPadDigital.RightStick:
                    button = "Right thumb";
                    break;
                case XboxPadDigital.LeftShoulder:
                    button = "Left shoulder";
                    break;
                case XboxPadDigital.RightShoulder:
                    button = "Right shoulder";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return string.Format("<b>Xbox pad {0}</b> <i>'{1}'</i>", player, button);
        }

        public float GetValue()
        {
            var state = GamePad.GetState((PlayerIndex) Player);
            var buttons = state.Buttons;
            ButtonState buttonState;
            switch (Digital)
            {
                case XboxPadDigital.A:
                    buttonState = buttons.A;
                    break;
                case XboxPadDigital.B:
                    buttonState = buttons.B;
                    break;
                case XboxPadDigital.X:
                    buttonState = buttons.X;
                    break;
                case XboxPadDigital.Y:
                    buttonState = buttons.Y;
                    break;
                case XboxPadDigital.LeftStick:
                    buttonState = buttons.LeftStick;
                    break;
                case XboxPadDigital.RightStick:
                    buttonState = buttons.RightStick;
                    break;
                case XboxPadDigital.LeftShoulder:
                    buttonState = buttons.LeftShoulder;
                    break;
                case XboxPadDigital.RightShoulder:
                    buttonState = buttons.RightShoulder;
                    break;
                case XboxPadDigital.Back:
                    buttonState = buttons.Back;
                    break;
                case XboxPadDigital.Start:
                    buttonState = buttons.Start;
                    break;
                case XboxPadDigital.Guide:
                    buttonState = buttons.Guide;
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
            return string.Format("Player: {0}, Digital: {1}", Player, Digital);
        }

        #region Equality members

        private bool Equals(XboxPadDigitalButton other)
        {
            return Player == other.Player && Digital == other.Digital;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is XboxPadDigitalButton && Equals((XboxPadDigitalButton) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Player*397) ^ (int) Digital;
            }
        }

        public static bool operator ==(XboxPadDigitalButton left, XboxPadDigitalButton right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(XboxPadDigitalButton left, XboxPadDigitalButton right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}