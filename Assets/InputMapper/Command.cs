using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Assets.InputMapper
{
    /// <summary>
    ///     Represents a command.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        ///     Constructor for serialization and derived implementations.
        /// </summary>
        [UsedImplicitly]
        protected Command()
        {
        }

        /// <summary>
        ///     Gets the buttons of this command, i.e. there are as many buttons as there are labels, each button set has one or
        ///     many device button(s) which user input is fetched from.
        /// </summary>
        public List<HashSet<IDeviceButton>> Buttons { get; protected set; }

        /// <summary>
        ///     Gets the labels of this command, e.g. a button command would have one, an axis command would have two.
        /// </summary>
        public string[] Labels { get; protected set; }

        /// <summary>
        ///     Gets the name of this command.
        /// </summary>
        public string Name { get; protected set; }


        /// <summary>
        ///     Gets the index of a button from its name.
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        public int GetButtonIndex(string buttonName)
        {
            if (buttonName == null) throw new ArgumentNullException("buttonName");
            return Array.IndexOf(Labels, buttonName);
        }

        /// <summary>
        ///     Gets the name of a button from its index.
        /// </summary>
        /// <param name="buttonIndex"></param>
        /// <returns></returns>
        public string GetButtonName(int buttonIndex)
        {
            return Labels[buttonIndex];
        }

        /// <summary>
        ///     Gets the value of this command (see Remarks).
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///     Depending the implementation, value would range from -1.0 to +1.0 (e.g. an axis command), from 0.0 to 1.0
        ///     (e.g. a button command), or a different range (discouraged).
        /// </remarks>
        [UsedImplicitly]
        public abstract float GetValue();

        public override string ToString()
        {
            return Name;
        }

        #region Equality members

        protected bool Equals(Command other)
        {
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Command) obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static bool operator ==(Command left, Command right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Command left, Command right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}