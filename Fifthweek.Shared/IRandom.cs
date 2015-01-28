namespace Fifthweek.Shared
{
    using System;

    public interface IRandom
    {
        /// <summary>
        /// Returns a nonnegative random integer.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to zero and less than System.Int32.MaxValue.
        /// </returns>
        int Next();

        /// <summary>
        /// Returns a nonnegative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number to be generated. <see cref="maxValue"/>
        /// must be greater than or equal to zero.
        /// </param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to zero, and less than <see cref="maxValue"/>;
        /// that is, the range of return values ordinarily includes zero but not <see cref="maxValue"/>.
        /// However, if <see cref="maxValue"/> equals zero, <see cref="maxValue"/> is returned.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <see cref="maxValue"/> is less than zero.
        /// </exception>
        int Next(int maxValue);
        
        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">
        /// The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number returned. <see cref="maxValue"/> must be
        /// greater than or equal to <see cref="minValue"/>.
        /// </param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to <see cref="minValue"/> and less than <see cref="maxValue"/>;
        /// that is, the range of return values includes <see cref="minValue"/> but not <see cref="maxValue"/>. If
        /// <see cref="minValue"/> equals <see cref="maxValue"/>, <see cref="minValue"/> is returned.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <see cref="minValue"/> is greater than <see cref="maxValue"/>
        /// </exception>
        int Next(int minValue, int maxValue);

        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// </summary>
        /// <param name="buffer">
        /// An array of bytes to contain random numbers.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <see cref="buffer"/> is null.
        /// </exception>
        void NextBytes(byte[] buffer);

        /// <summary>
        /// Returns a random floating-point number between 0.0 and 1.0.
        /// </summary>
        /// <returns>
        /// A double-precision floating point number greater than or equal to 0.0, and
        /// less than 1.0.
        /// </returns>
        double NextDouble();
    }
}