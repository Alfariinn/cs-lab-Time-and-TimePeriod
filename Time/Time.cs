namespace TimeLib
{
    public sealed class Time : IEquatable<Time>, IComparable<Time>
    {

        public byte hours { get; init; }
        public byte minutes { get; init; }
        public byte seconds { get; init; }

        public Time(byte? hours = null, byte? minutes = null, byte? seconds = null)
        {
            this.hours = hours ?? 0;
            this.minutes = minutes ?? 0;
            this.seconds = seconds ?? 0;
            if (hours > 23 || minutes > 59 || seconds > 59)
                throw new ArgumentOutOfRangeException();
        }
        public Time(string str)
        {
            var split = str.Split(':', StringSplitOptions.TrimEntries);
            byte h;
            if (byte.TryParse(split[0], out h))
                hours = h;
            else
                throw new ArgumentException();
            byte m;
            if (byte.TryParse(split[1], out m))
                minutes = m;
            else
                throw new ArgumentException();
            byte s;
            if (byte.TryParse(split[2], out s))
                seconds = s;
            else
                throw new ArgumentException();

            if (hours > 23 || minutes > 59 || seconds > 59)
                throw new ArgumentOutOfRangeException();

        }


        public override string ToString()
        {
            return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }

        public bool Equals(Time? other)
        {
            if (other is null)
                return false;

            return hours == other.hours && minutes == other.minutes && seconds == other.seconds;
        }

        public int CompareTo(Time? other)
        {
            if (other is null)
                return 1;

            int hourComparison = hours.CompareTo(other.hours);
            if (hourComparison != 0)
                return hourComparison;

            int minuteComparison = minutes.CompareTo(other.minutes);
            if (minuteComparison != 0)
                return minuteComparison;

            return seconds.CompareTo(other.seconds);
        }
        public static bool operator ==(Time? time1, Time? time2)
        {
            if (ReferenceEquals(time1, time2))
                return true;

            if (time1 is null || time2 is null)
                return false;

            return time1.Equals(time2);
        }

        public static bool operator !=(Time? time1, Time? time2)
        {
            return !(time1 == time2);
        }

        public static bool operator <(Time? time1, Time? time2)
        {
            if (time1 is null || time2 is null)
                throw new ArgumentNullException();

            return time1.CompareTo(time2) < 0;
        }

        public static bool operator <=(Time? time1, Time? time2)
        {
            if (time1 is null || time2 is null)
                throw new ArgumentNullException();

            return time1.CompareTo(time2) <= 0;
        }

        public static bool operator >(Time? time1, Time? time2)
        {
            return !(time1 <= time2);
        }

        public static bool operator >=(Time? time1, Time? time2)
        {
            return !(time1 < time2);
        }





    }
}