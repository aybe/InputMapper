using System;
using JetBrains.Annotations;
using XInputDotNetPure;

namespace Assets.InputMapper.Devices
{
    /// <summary>
    ///     Represents an axis button on a XBOX pad.
    /// </summary>
    public sealed class XboxPadAxisButton : IDeviceButton
    {
        /// <summary>
        ///     Constructor for serialization, use <see cref="XboxPadAxisButton(XboxPadPlayer,XboxPadAxis,XboxPadAxisDirection)" />
        ///     instead.
        /// </summary>
        [UsedImplicitly]
        public XboxPadAxisButton()
        {
        }

        public XboxPadAxisButton(XboxPadPlayer player, XboxPadAxis axis, XboxPadAxisDirection direction)
        {
            Player = player;
            Axis = axis;
            Direction = direction;
        }

        public XboxPadPlayer Player { get; private set; }
        public XboxPadAxis Axis { get; private set; }
        public XboxPadAxisDirection Direction { get; private set; }

        public string GetDescription()
        {
            var player = (int) Player + 1;
            var stick = Axis == XboxPadAxis.LeftX || Axis == XboxPadAxis.LeftY ? "Left" : "Right";
            var direction = Direction == XboxPadAxisDirection.Positive ? "+" : "-";
            var axis = Axis == XboxPadAxis.LeftX || Axis == XboxPadAxis.RightX ? "X" : "Y";
            var format = string.Format("{0} {1}{2}", stick, direction, axis);
            var description = string.Format("<b>Xbox pad {0}</b> <i>'{1}'</i>", player, format);
            return description;
        }

        public float GetValue()
        {
            var state = GamePad.GetState((PlayerIndex) Player);
            var sticks = state.ThumbSticks;
            float value;

            switch (Axis)
            {
                case XboxPadAxis.LeftX:
                    value = sticks.Left.X;
                    break;
                case XboxPadAxis.RightX:
                    value = sticks.Right.X;
                    break;
                case XboxPadAxis.LeftY:
                    value = sticks.Left.Y;
                    break;
                case XboxPadAxis.RightY:
                    value = sticks.Right.Y;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (Direction)
            {
                case XboxPadAxisDirection.Positive:
                    return value > 0.0f ? value : 0.0f;
                case XboxPadAxisDirection.Negative:
                    return value < 0.0f ? value : 0.0f;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool IsPressed()
        {
            var value = GetValue();
            var isPressed = value < 0.0f || value > 0.0f;
            return isPressed;
        }

        public void Update(float deltaTime)
        {
        }

        public override string ToString()
        {
            return string.Format("Player: {0}, Axis: {1}, Direction: {2}", Player, Axis, Direction);
        }

        #region Equality members

        private bool Equals(XboxPadAxisButton other)
        {
            return Player == other.Player && Axis == other.Axis && Direction == other.Direction;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is XboxPadAxisButton && Equals((XboxPadAxisButton) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Player;
                hashCode = (hashCode*397) ^ (int) Axis;
                hashCode = (hashCode*397) ^ (int) Direction;
                return hashCode;
            }
        }

        public static bool operator ==(XboxPadAxisButton left, XboxPadAxisButton right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(XboxPadAxisButton left, XboxPadAxisButton right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}