using System;
using JetBrains.Annotations;
using XInputDotNetPure;

namespace Assets.InputMapper.Devices
{
    /// <summary>
    ///     Represents a trigger button on a XBOX pad.
    /// </summary>
    public sealed class XboxPadTriggerButton : IDeviceButton
    {
        /// <summary>
        ///     Constructor for serialization, use <see cref="XboxPadTriggerButton(XboxPadPlayer, XboxPadTrigger)" /> instead.
        /// </summary>
        [UsedImplicitly]
        public XboxPadTriggerButton()
        {
        }

        public XboxPadTriggerButton(XboxPadPlayer player, XboxPadTrigger trigger)
        {
            Player = player;
            Trigger = trigger;
        }

        public XboxPadPlayer Player { get; private set; }
        public XboxPadTrigger Trigger { get; private set; }

        public string GetDescription()
        {
            var player = (int) Player + 1;
            string trigger;
            switch (Trigger)
            {
                case XboxPadTrigger.Left:
                    trigger = "Left trigger";
                    break;
                case XboxPadTrigger.Right:
                    trigger = "Right trigger";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return string.Format("<b>Xbox pad {0}</b> <i>'{1}'</i>", player, trigger);
        }

        public float GetValue()
        {
            var state = GamePad.GetState((PlayerIndex) Player);
            float f;
            switch (Trigger)
            {
                case XboxPadTrigger.Left:
                    f = state.Triggers.Left;
                    break;
                case XboxPadTrigger.Right:
                    f = state.Triggers.Right;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return f;
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
            return string.Format("Player: {0}, Trigger: {1}", Player, Trigger);
        }

        #region Equality members

        private bool Equals(XboxPadTriggerButton other)
        {
            return Player == other.Player && Trigger == other.Trigger;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is XboxPadTriggerButton && Equals((XboxPadTriggerButton) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Player*397) ^ (int) Trigger;
            }
        }

        public static bool operator ==(XboxPadTriggerButton left, XboxPadTriggerButton right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(XboxPadTriggerButton left, XboxPadTriggerButton right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}