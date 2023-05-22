namespace TimeLib
{
    /// <summary>
    /// Represents a point in time in the format hour:minute:second.
    /// </summary>
    public readonly struct Time : IEquatable<Time>, IComparable<Time>
    {
        /// <summary>
        /// Gets the value of hours.
        /// </summary>
        public byte Hours { get; }

        /// <summary>
        /// Gets the value of minutes.
        /// </summary>
        public byte Minutes { get; }

        /// <summary>
        /// Gets the value of seconds.
        /// </summary>
        public byte Seconds { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> struct with the specified hours, minutes, and seconds.
        /// </summary>
        /// <param name="hours">The value of hours (optional, default is 0).</param>
        /// <param name="minutes">The value of minutes (optional, default is 0).</param>
        /// <param name="seconds">The value of seconds (optional, default is 0).</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the time values are invalid (hours > 23, minutes or seconds > 59) or negative.</exception>
        public Time(byte? hours = null, byte? minutes = null, byte? seconds = null)
        {
            Hours = hours ?? 0;
            Minutes = minutes ?? 0;
            Seconds = seconds ?? 0;

            if (Hours > 23 || Minutes > 59 || Seconds > 59)
                throw new ArgumentOutOfRangeException("Invalid time format.");

            if (Hours < 0 || Minutes < 0 || Seconds < 0)
                throw new ArgumentOutOfRangeException("Negative values are not allowed.");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> struct with the specified time string in the format "hour:minute:second".
        /// </summary>
        /// <param name="str">The string representing the time.</param>
        /// <exception cref="ArgumentException">Thrown when the time string is in an invalid format or contains invalid values.</exception>
        public Time(string str)
        {
            var split = str.Split(':', StringSplitOptions.TrimEntries);

            if (split.Length != 3)
                throw new ArgumentException("Invalid time format.");

            byte h, m, s;
            if (byte.TryParse(split[0], out h) &&
                byte.TryParse(split[1], out m) &&
                byte.TryParse(split[2], out s))
            {
                if (h > 23 || m > 59 || s > 59)
                    throw new ArgumentOutOfRangeException("Invalid time format.");

                if (h < 0 || m < 0 || s < 0)
                    throw new ArgumentException("Negative values are not allowed.");

                Hours = h;
                Minutes = m;
                Seconds = s;
            }
            else
            {
                throw new ArgumentException("Invalid time format or values.");
            }
        }

        /// <summary>
        /// Converts the time to its string representation in the format "hh:mm:ss".
        /// </summary>
        /// <returns>The string representation of the time.</returns>
        public override string ToString()
        {
            return string.Format("{0:00}:{1:00}:{2:00}", Hours, Minutes, Seconds);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Time"/> object is equal to the current <see cref="Time"/> object.
        /// </summary>
        /// <param name="other">The <see cref="Time"/> object to compare with the current <see cref="Time"/> object.</param>
        /// <returns><c>true</c> if the specified <see cref="Time"/> object is equal to the current <see cref="Time"/> object; otherwise, <c>false</c>.</returns>
        public bool Equals(Time other)
        {
            return Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;
        }

        /// <summary>
        /// Compares the current <see cref="Time"/> object with another <see cref="Time"/> object.
        /// </summary>
        /// <param name="other">The <see cref="Time"/> object to compare with the current <see cref="Time"/> object.</param>
        /// <returns>
        /// A value indicating the relative order of the objects being compared. The return value has the following meanings:
        /// Less than 0: The current object is less than the other object.
        /// 0: The current object is equal to the other object.
        /// Greater than 0: The current object is greater than the other object.
        /// </returns>
        public int CompareTo(Time other)
        {
            int hourComparison = Hours.CompareTo(other.Hours);
            if (hourComparison != 0)
                return hourComparison;

            int minuteComparison = Minutes.CompareTo(other.Minutes);
            if (minuteComparison != 0)
                return minuteComparison;

            return Seconds.CompareTo(other.Seconds);
        }

        /// <summary>
        /// Determines whether two <see cref="Time"/> objects are equal.
        /// </summary>
        /// <param name="time1">The first <see cref="Time"/> object to compare.</param>
        /// <param name="time2">The second <see cref="Time"/> object to compare.</param>
        /// <returns><c>true</c> if the two <see cref="Time"/> objects are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Time time1, Time time2)
        {
            return time1.Equals(time2);
        }

        /// <summary>
        /// Determines whether two <see cref="Time"/> objects are not equal.
        /// </summary>
        /// <param name="time1">The first <see cref="Time"/> object to compare.</param>
        /// <param name="time2">The second <see cref="Time"/> object to compare.</param>
        /// <returns><c>true</c> if the two <see cref="Time"/> objects are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Time time1, Time time2)
        {
            return !(time1 == time2);
        }

        /// <summary>
        /// Determines whether one <see cref="Time"/> object is less than another <see cref="Time"/> object.
        /// </summary>
        /// <param name="time1">The first <see cref="Time"/> object to compare.</param>
        /// <param name="time2">The second <see cref="Time"/> object to compare.</param>
        /// <returns><c>true</c> if the first <see cref="Time"/> object is less than the second <see cref="Time"/> object; otherwise, <c>false</c>.</returns>
        public static bool operator <(Time time1, Time time2)
        {
            return time1.CompareTo(time2) < 0;
        }

        /// <summary>
        /// Determines whether one <see cref="Time"/> object is less than or equal to another <see cref="Time"/> object.
        /// </summary>
        /// <param name="time1">The first <see cref="Time"/> object to compare.</param>
        /// <param name="time2">The second <see cref="Time"/> object to compare.</param>
        /// <returns><c>true</c> if the first <see cref="Time"/> object is less than or equal to the second <see cref="Time"/> object; otherwise, <c>false</c>.</returns>
        public static bool operator <=(Time time1, Time time2)
        {
            return time1.CompareTo(time2) <= 0;
        }

        /// <summary>
        /// Determines whether one <see cref="Time"/> object is greater than another <see cref="Time"/> object.
        /// </summary>
        /// <param name="time1">The first <see cref="Time"/> object to compare.</param>
        /// <param name="time2">The second <see cref="Time"/> object to compare.</param>
        /// <returns><c>true</c> if the first <see cref="Time"/> object is greater than the second <see cref="Time"/> object; otherwise, <c>false</c>.</returns>
        public static bool operator >(Time time1, Time time2)
        {
            return !(time1 <= time2);
        }

        /// <summary>
        /// Determines whether one <see cref="Time"/> object is greater than or equal to another <see cref="Time"/> object.
        /// </summary>
        /// <param name="time1">The first <see cref="Time"/> object to compare.</param>
        /// <param name="time2">The second <see cref="Time"/> object to compare.</param>
        /// <returns><c>true</c> if the first <see cref="Time"/> object is greater than or equal to the second <see cref="Time"/> object; otherwise, <c>false</c>.</returns>
        public static bool operator >=(Time time1, Time time2)
        {
            return !(time1 < time2);
        }

        /// <summary>
        /// Adds a <see cref="TimePeriod"/> to the current <see cref="Time"/> object and returns a new <see cref="Time"/> object.
        /// </summary>
        /// <param name="timePeriod">The <see cref="TimePeriod"/> to add.</param>
        /// <returns>A new <see cref="Time"/> object representing the result of the addition.</returns>
        public Time Plus(TimePeriod timePeriod)
        {
            long totalSeconds = Hours * 3600 + Minutes * 60 + Seconds;
            long periodSeconds = timePeriod.hours * 3600 + timePeriod.minutes * 60 + timePeriod.seconds;
            long resultSeconds = (totalSeconds + periodSeconds) % (24 * 3600);

            byte resultHours = (byte)(resultSeconds / 3600);
            byte resultMinutes = (byte)((resultSeconds % 3600) / 60);
            byte resultSecondsFinal = (byte)(resultSeconds % 60);

            return new Time(resultHours, resultMinutes, resultSecondsFinal);
        }

        /// <summary>
        /// Adds a <see cref="TimePeriod"/> to a <see cref="Time"/> object and returns a new <see cref="Time"/> object.
        /// </summary>
        /// <param name="time">The <see cref="Time"/> object to add the <see cref="TimePeriod"/> to.</param>
        /// <param name="timePeriod">The <see cref="TimePeriod"/> to add.</param>
        /// <returns>A new <see cref="Time"/> object representing the result of the addition.</returns>
        public static Time Plus(Time time, TimePeriod timePeriod)
        {
            return time.Plus(timePeriod);
        }

        /// <summary>
        /// Adds a <see cref="TimePeriod"/> to a <see cref="Time"/> object and returns a new <see cref="Time"/> object.
        /// </summary>
        /// <param name="time">The <see cref="Time"/> object to add the <see cref="TimePeriod"/> to.</param>
        /// <param name="timePeriod">The <see cref="TimePeriod"/> to add.</param>
        /// <returns>A new <see cref="Time"/> object representing the result of the addition.</returns>
        public static Time operator +(Time time, TimePeriod timePeriod)
        {
            return time.Plus(timePeriod);
        }

        /// <summary>
        /// Subtracts a <see cref="TimePeriod"/> from the current <see cref="Time"/> object and returns a new <see cref="Time"/> object.
        /// </summary>
        /// <param name="timePeriod">The <see cref="TimePeriod"/> to subtract.</param>
        /// <returns>A new <see cref="Time"/> object representing the result of the subtraction.</returns>
        public Time Minus(TimePeriod timePeriod)
        {
            long totalSeconds = Hours * 3600 + Minutes * 60 + Seconds;
            long periodSeconds = timePeriod.hours * 3600 + timePeriod.minutes * 60 + timePeriod.seconds;
            long resultSeconds = (totalSeconds - periodSeconds + 24 * 3600) % (24 * 3600);

            byte resultHours = (byte)(resultSeconds / 3600);
            byte resultMinutes = (byte)((resultSeconds % 3600) / 60);
            byte resultSecondsFinal = (byte)(resultSeconds % 60);

            return new Time(resultHours, resultMinutes, resultSecondsFinal);
        }

        /// <summary>
        /// Subtracts a <see cref="TimePeriod"/> from a <see cref="Time"/> object and returns a new <see cref="Time"/> object.
        /// </summary>
        /// <param name="time">The <see cref="Time"/> object to subtract the <see cref="TimePeriod"/> from.</param>
        /// <param name="timePeriod">The <see cref="TimePeriod"/> to subtract.</param>
        /// <returns>A new <see cref="Time"/> object representing the result of the subtraction.</returns>
        public static Time Minus(Time time, TimePeriod timePeriod)
        {
            return time.Minus(timePeriod);
        }

        /// <summary>
        /// Subtracts a <see cref="TimePeriod"/> from a <see cref="Time"/> object and returns a new <see cref="Time"/> object.
        /// </summary>
        /// <param name="time">The <see cref="Time"/> object to subtract the <see cref="TimePeriod"/> from.</param>
        /// <param name="timePeriod">The <see cref="TimePeriod"/> to subtract.</param>
        /// <returns>A new <see cref="Time"/> object representing the result of the subtraction.</returns>
        public static Time operator -(Time time, TimePeriod timePeriod)
        {
            return time.Minus(timePeriod);
        }
    }
}