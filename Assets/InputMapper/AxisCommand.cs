using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Assets.InputMapper
{
    /// <summary>
    ///     Represents an axis command.
    /// </summary>
    public sealed class AxisCommand : Command
    {
        /// <summary>
        ///     Constructor for serialization, use <see cref="AxisCommand(string,string,string)" /> instead.
        /// </summary>
        [UsedImplicitly]
        public AxisCommand()
        {
        }

        /// <summary>
        ///     Create a new instance of <see cref="AxisCommand" /> (see Remarks).
        /// </summary>
        /// <param name="name">Command name.</param>
        /// <param name="label1">Name of first label (e.g. left/up).</param>
        /// <param name="label2">Name of second label (e.g right/down).</param>
        /// <remarks><paramref name="label1" /> and <paramref name="label2" /> must differ.</remarks>
        public AxisCommand([NotNull] string name, [NotNull] string label1, [NotNull] string label2)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (label1 == null) throw new ArgumentNullException("label1");
            if (label2 == null) throw new ArgumentNullException("label2");
            if (label1 == label2) throw new ArgumentOutOfRangeException("Labels must differ.", (Exception) null);
            Name = name;
            Labels = new[] {label1, label2};
            Buttons = new List<HashSet<IDeviceButton>>
            {
                new HashSet<IDeviceButton>(),
                new HashSet<IDeviceButton>()
            };
        }

        public override float GetValue()
        {
            var f1 = Buttons[0].Select(s => s.GetValue()).FirstOrDefault(s => s < 0.0f || s > 0.0f);
            var f2 = Buttons[1].Select(s => s.GetValue()).FirstOrDefault(s => s < 0.0f || s > 0.0f);
            return f1 > 0.0f || f1 < 0.0f ? Math.Abs(f1)*-1.0f : Math.Abs(f2)*+1.0f;
        }
    }
}