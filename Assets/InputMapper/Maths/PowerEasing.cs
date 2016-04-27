using System;

namespace Assets.InputMapper.Maths
{
    /// <summary>
    ///     Easing that uses a power function.
    /// </summary>
    public sealed class PowerEasing : Easing
    {
        public PowerEasing()
        {
            Power = 2.0d;
        }

        public double Power { get; set; }

        public override double GetValueCore(double t)
        {
            var d = Math.Pow(t, Math.Max(0.0d, Power));
            return d;
        }
    }
}