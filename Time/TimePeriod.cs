namespace TimeLib
{
    /// <summary>
    /// Represents a time period or duration in hours, minutes, and seconds.
    /// </summary>
    public readonly struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
         private readonly long _totalSeconds;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePeriod"/> struct with the specified hours, minutes, and seconds.
        /// </summary>
        /// <param name="hours">The number of hours.</param>
        /// <param name="minutes">The number of minutes.</param>
        /// <param name="seconds">The number of seconds.</param>
        /// 
        public TimePeriod(long hours = 0, long minutes = 0, long seconds = 0)
        {
            if (hours < 0 || minutes < 0 || seconds < 0)
                throw new ArgumentException("Negative values are not allowed.");

            _totalSeconds = hours * 3600 + minutes * 60 + seconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePeriod"/> struct representing the duration between two <see cref="Time"/> objects.
        /// </summary>
        /// <param name="start">The starting time.</param>
        /// <param name="end">The ending time.</param>
        public TimePeriod(Time start, Time end)
        {
            _totalSeconds = Math.Abs((end.Hours * 3600 + end.Minutes * 60 + end.Seconds) -
                            (start.Hours * 3600 + start.Minutes * 60 + start.Seconds));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePeriod"/> struct from a string representation of a duration in the format "hours:minutes:seconds".
        /// </summary>
        /// <param name="duration">The string representation of the duration.</param>
        /// <exception cref="ArgumentException">Thrown when the duration has an invalid format or contains negative values.</exception>

        public TimePeriod(string duration)
        {
            var split = duration.Split(':', StringSplitOptions.TrimEntries);
            if (split.Length != 3)
                throw new ArgumentException("Invalid duration format.");

            long h, m, s;
            if (long.TryParse(split[0], out h) &&
                long.TryParse(split[1], out m) &&
                long.TryParse(split[2], out s))
            {
                if (h < 0 || m < 0 || s < 0)
                    throw new ArgumentException("Negative values are not allowed.");

                _totalSeconds = h * 3600 + m * 60 + s;
            }
            else
            {
                throw new ArgumentException("Invalid duration format.");
            }
        }

        /// <summary>
        /// Gets the total number of seconds in the time period.
        /// </summary>
        public long totalSeconds => _totalSeconds;

        /// <summary>
        /// Gets the number of hours in the time period.
        /// </summary>
        public long hours => _totalSeconds / 3600;

        /// <summary>
        /// Gets the number of minutes in the time period.
        /// </summary>
        public long minutes => (_totalSeconds % 3600) / 60;

        /// <summary>
        /// Gets the number of seconds in the time period.
        /// </summary>
        public long seconds => _totalSeconds % 60;

        /// <summary>
        /// Converts the time period to its string representation in the format "hours:minutes:seconds".
        /// </summary>
        /// <returns>The string representation of the time period.</returns>

        public override string ToString()
        {
            return $"{hours}:{minutes:D2}:{seconds:D2}";
        }

        /// <summary>
        /// Determines whether the current instance of <see cref="TimePeriod"/> is equal to another <see cref="TimePeriod"/> object.
        /// </summary>
        /// <param name="other">The <see cref="TimePeriod"/> object to compare with.</param>
        /// <returns><c>true</c> if the time periods are equal; otherwise, <c>false</c>.</returns>

        public bool Equals(TimePeriod other)
        {
            return totalSeconds == other.totalSeconds;
        }

        /// <summary>
        /// Determines whether the current instance of <see cref="TimePeriod"/> is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns><c>true</c> if the object is a <see cref="TimePeriod"/> and is equal to the current instance; otherwise, <c>false</c>.</returns>

        public override bool Equals(object obj)
        {
            return obj is TimePeriod other && Equals(other);
        }

        /// <summary>
        /// Returns the hash code for the current instance of <see cref="TimePeriod"/>.
        /// </summary>
        /// <returns>The hash code for the current instance.</returns>

        public override int GetHashCode()
        {
            return totalSeconds.GetHashCode();
        }

        /// <summary>
        /// Compares the current instance of <see cref="TimePeriod"/> with another <see cref="TimePeriod"/> object.
        /// </summary>
        /// <param name="other">The <see cref="TimePeriod"/> object to compare with.</param>
        /// <returns>A value indicating the relative order of the objects being compared.</returns>

        public int CompareTo(TimePeriod other)
        {
            return totalSeconds.CompareTo(other.totalSeconds);
        }

        /// <summary>
        /// Determines whether two <see cref="TimePeriod"/> objects are equal.
        /// </summary>
        /// <param name="time1">The first <see cref="TimePeriod"/> object.</param>
        /// <param name="time2">The second <see cref="TimePeriod"/> object.</param>
        /// <returns><c>true</c> if the time periods are equal; otherwise, <c>false</c>.</returns>

        public static bool operator ==(TimePeriod time1, TimePeriod time2)
        {
            return time1.Equals(time2);
        }

        /// <summary>
        /// Determines whether two <see cref="TimePeriod"/> objects are not equal.
        /// </summary>
        /// <param name="time1">The first <see cref="TimePeriod"/> object.</param>
        /// <param name="time2">The second <see cref="TimePeriod"/> object.</param>
        /// <returns><c>true</c> if the time periods are not equal; otherwise, <c>false</c>.</returns>

        public static bool operator !=(TimePeriod time1, TimePeriod time2)
        {
            return !(time1 == time2);
        }

        /// <summary>
        /// Determines whether one <see cref="TimePeriod"/> is less than another <see cref="TimePeriod"/>.
        /// </summary>
        /// <param name="time1">The first <see cref="TimePeriod"/> object.</param>
        /// <param name="time2">The second <see cref="TimePeriod"/> object.</param>
        /// <returns><c>true</c> if the first time period is less than the second; otherwise, <c>false</c>.</returns>

        public static bool operator <(TimePeriod time1, TimePeriod time2)
        {
            return time1.CompareTo(time2) < 0;
        }

        /// <summary>
        /// Determines whether one <see cref="TimePeriod"/> is less than or equal to another <see cref="TimePeriod"/>.
        /// </summary>
        /// <param name="time1">The first <see cref="TimePeriod"/> object.</param>
        /// <param name="time2">The second <see cref="TimePeriod"/> object.</param>
        /// <returns><c>true</c> if the first time period is less than or equal to the second; otherwise, <c>false</c>.</returns>

        public static bool operator <=(TimePeriod time1, TimePeriod time2)
        {
            return time1.CompareTo(time2) <= 0;
        }

        /// <summary>
        /// Determines whether one <see cref="TimePeriod"/> is greater than another <see cref="TimePeriod"/>.
        /// </summary>
        /// <param name="time1">The first <see cref="TimePeriod"/> object.</param>
        /// <param name="time2">The second <see cref="TimePeriod"/> object.</param>
        /// <returns><c>true</c> if the first time period is greater than the second; otherwise, <c>false</c>.</returns>

        public static bool operator >(TimePeriod time1, TimePeriod time2)
        {
            return !(time1 <= time2);
        }

        /// <summary>
        /// Determines whether one <see cref="TimePeriod"/> is greater than or equal to another <see cref="TimePeriod"/>.
        /// </summary>
        /// <param name="time1">The first <see cref="TimePeriod"/> object.</param>
        /// <param name="time2">The second <see cref="TimePeriod"/> object.</param>
        /// <returns><c>true</c> if the first time period is greater than or equal to the second; otherwise, <c>false</c>.</returns>

        public static bool operator >=(TimePeriod time1, TimePeriod time2)
        {
            return !(time1 < time2);
        }

        /// <summary>
        /// Adds two <see cref="TimePeriod"/> objects together and returns new <see cref="TimePeriod"/> object .
        /// </summary>
        /// <param name="time1">The first <see cref="TimePeriod"/> object.</param>
        /// <param name="time2">The second <see cref="TimePeriod"/> object.</param>
        /// <returns>A new <see cref="TimePeriod"/> object representing the sum of the two time periods.</returns>

        public TimePeriod Plus(TimePeriod other)
        {
            long newTotalSeconds = (totalSeconds + other.totalSeconds);
            long newSeconds = newTotalSeconds % 60;
            long newMinutes = (newTotalSeconds % 3600) / 60;
            long newHours = newTotalSeconds / 3600;

            return new TimePeriod(newHours, newMinutes, newSeconds);
        }

        /// <summary>
        /// Adds two <see cref="TimePeriod"/> objects together and returns new <see cref="TimePeriod"/> object .
        /// </summary>
        /// <param name="time1">The first <see cref="TimePeriod"/> object.</param>
        /// <param name="time2">The second <see cref="TimePeriod"/> object.</param>
        /// <returns>A new <see cref="TimePeriod"/> object representing the sum of the two time periods.</returns>
        public static TimePeriod Plus(TimePeriod time1, TimePeriod time2)
        {

         return time1.Plus(time2);
        }

        /// <summary>
        /// Subtracts one <see cref="TimePeriod"/> object from another.
        /// </summary>
        /// <param name="time1">The first <see cref="TimePeriod"/> object.</param>
        /// <param name="time2">The second <see cref="TimePeriod"/> object.</param>
        /// <returns>A new <see cref="TimePeriod"/> object representing the difference between the two time periods.</returns>

        public TimePeriod Minus(TimePeriod other)
        {
            long newTotalSeconds = (totalSeconds - other.totalSeconds);
            long newSeconds = newTotalSeconds % 60;
            long newMinutes = (newTotalSeconds % 3600) / 60;
            long newHours = newTotalSeconds / 3600;


            if (newTotalSeconds < 0)
                throw new InvalidOperationException("Resulting time period cannot be negative.");
            return new TimePeriod(newHours, newMinutes, newSeconds);
        }

        /// <summary>
        /// Subtracts one <see cref="TimePeriod"/> object from another.
        /// </summary>
        /// <param name="time1">The first <see cref="TimePeriod"/> object.</param>
        /// <param name="time2">The second <see cref="TimePeriod"/> object.</param>
        /// <returns>A new <see cref="TimePeriod"/> object representing the difference between the two time periods.</returns>

        public static TimePeriod Minus(TimePeriod time1, TimePeriod time2)
        {
            return time1.Minus(time2);
        }

        /// <summary>
        /// Adds two <see cref="TimePeriod"/> objects together and returns new <see cref="TimePeriod"/> object .
        /// </summary>
        /// <param name="time1">The first <see cref="TimePeriod"/> object.</param>
        /// <param name="time2">The second <see cref="TimePeriod"/> object.</param>
        /// <returns>A new <see cref="TimePeriod"/> object representing the sum of the two time periods.</returns>

        public static TimePeriod operator +(TimePeriod time1, TimePeriod time2)
        {
            return time1.Plus(time2);
        }

        /// <summary>
        /// Subtracts one <see cref="TimePeriod"/> object from another.
        /// </summary>
        /// <param name="time1">The first <see cref="TimePeriod"/> object.</param>
        /// <param name="time2">The second <see cref="TimePeriod"/> object.</param>
        /// <returns>A new <see cref="TimePeriod"/> object representing the difference between the two time periods.</returns>

        public static TimePeriod operator -(TimePeriod time1, TimePeriod time2)
        {
            return time1.Minus(time2);
        }

        /// <summary>
        /// Multiplies a <see cref="TimePeriod"/> object by a scalar value.
        /// </summary>
        /// <param name="time1">The <see cref="TimePeriod"/> object.</param>
        /// <param name="long1">The scalar value.</param>
        /// <returns>A new <see cref="TimePeriod"/> object representing the result of the multiplication.</returns>

        public TimePeriod Multiply(long other)
        {
            if (other < 0)
                throw new ArgumentException("Multiplier has to be positive.");
            long newTotalSeconds = (totalSeconds * other);
            long newSeconds = newTotalSeconds % 60;
            long newMinutes = (newTotalSeconds % 3600) / 60;
            long newHours = newTotalSeconds / 3600;

            return new TimePeriod(newHours, newMinutes, newSeconds);
        }

        /// <summary>
        /// Multiplies a <see cref="TimePeriod"/> object by a scalar value.
        /// </summary>
        /// <param name="time1">The <see cref="TimePeriod"/> object.</param>
        /// <param name="long1">The scalar value.</param>
        /// <returns>A new <see cref="TimePeriod"/> object representing the result of the multiplication.</returns>

        public static TimePeriod Mulitply(TimePeriod time1, long long1)
        {

            return time1.Multiply(long1);
        }

        /// <summary>
        /// Multiplies a <see cref="TimePeriod"/> object by a scalar value.
        /// </summary>
        /// <param name="time1">The <see cref="TimePeriod"/> object.</param>
        /// <param name="long1">The scalar value.</param>
        /// <returns>A new <see cref="TimePeriod"/> object representing the result of the multiplication.</returns>

        public static TimePeriod operator *(TimePeriod time1, long long1)
        {
            return time1.Multiply(long1);
        }

    }
}