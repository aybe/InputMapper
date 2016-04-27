using System;

namespace Assets.InputMapper.Maths
{
    /// <summary>
    ///     Base class for an easing.
    /// </summary>
    public abstract class Easing
    {
        public EasingMode Mode { get; set; }

        /// <summary>
        ///     Defines the core function for this instance, an easing for <see cref="EasingMode.In" /> mode within a normalized
        ///     time; <see cref="GetValue" /> will then transform it to match current <see cref="Mode" /> response.
        /// </summary>
        /// <param name="t">Normalized time.</param>
        /// <returns></returns>
        public abstract double GetValueCore(double t);

        /// <summary>
        ///     Gets the eased value for specified time.
        /// </summary>
        /// <param name="t">Normalized time.</param>
        /// <returns></returns>
        public double GetValue(double t)
        {
            double d;
            switch (Mode)
            {
                case EasingMode.In:
                    d = GetValueCore(t);
                    break;
                case EasingMode.Out:
                    d = 1.0d - GetValueCore(1.0d - t);
                    break;
                case EasingMode.InOut:
                    if (t < 0.5d)
                    {
                        d = 0.5d*GetValueCore(t*2.0d);
                    }
                    else
                    {
                        d = 0.5d + 0.5d*(1.0d - GetValueCore((1.0d - t)*2.0d));
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return d;
        }
    }
}