using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Assets.InputMapper
{
    /// <summary>
    ///     Represents a button command.
    /// </summary>
    public sealed class ButtonCommand : Command
    {
        /// <summary>
        ///     Constructor for serialization, use <see cref="ButtonCommand(string)" /> instead.
        /// </summary>
        [UsedImplicitly]
        public ButtonCommand()
        {
        }

        /// <summary>
        ///     Create a new instance of <see cref="ButtonCommand" /> (see Remarks).
        /// </summary>
        /// <param name="name">Command name.</param>
        /// <remarks>This particular command does have an empty label since its name designates its purpose.</remarks>
        public ButtonCommand([NotNull] string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            Name = name;
            Labels = new[] {""};
            Buttons = new List<HashSet<IDeviceButton>>
            {
                new HashSet<IDeviceButton>()
            };
        }

        public override float GetValue()
        {
            var buttons = Buttons[0];
            var value = buttons.Where(s => s.IsPressed()).Select(s => s.GetValue()).FirstOrDefault();
            return value;
        }
    }
}