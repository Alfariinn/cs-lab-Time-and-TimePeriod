using TimeLib;
namespace TimeUnitTests
{
    [TestClass]
    public class TimeTests
    {
        [DataTestMethod]
        [DataRow("00:00:00", true)]
        [DataRow("21:37:00", true)]
        [DataRow("23:59:59", true)]
        [DataRow("24:00:00", false)]
        [DataRow("12:60:00", false)]
        [DataRow("12:34:60", false)]
        [DataRow("12:34:56:78", false)]
        [DataRow("1r:3s:5p", false)]
        public void Constructor_StringInput_Valid(string input, bool shouldSucceed)
        {
            bool succeeded = true;
            try
            {
                var time = new Time(input);
            }
            catch (ArgumentException)
            {
                succeeded = false;
            }

            Assert.AreEqual(shouldSucceed, succeeded);
        }

        [DataTestMethod]
        [DataRow(0, 0, 0, true)]
        [DataRow(21, 37, 0, true)]
        [DataRow(23, 59, 59, true)]
        [DataRow(24, 0, 0, false)]
        [DataRow(12, 60, 0, false)]
        [DataRow(12, 34, 60, false)]
        [DataRow(255, 255, 255, false)]
        [DataRow(-1, -100, -2137, false)]
        public void Constructor_ByteInput_Valid(int hours, int minutes, int seconds, bool shouldSucceed)
        {
            bool succeeded = true;
            try
            {
                var time = new Time((byte)hours, (byte)minutes, (byte)seconds);
            }
            catch (ArgumentOutOfRangeException)
            {
                succeeded = false;
            }

            Assert.AreEqual(shouldSucceed, succeeded);
        }

        [DataTestMethod]
        [DataRow("00:00:00", "00:00:00", true)]
        [DataRow("12:34:56", "12:34:56", true)]
        [DataRow("21:37:00", "21:37:00", true)]
        [DataRow("23:59:59", "00:00:01", false)]
        [DataRow("12:34:56", "12:34:55", false)]
        public void Time_Equality(string timeStr1, string timeStr2, bool expectedEquality)
        {
            var time1 = new Time(timeStr1);
            var time2 = new Time(timeStr2);

            bool actualEquality = time1.Equals(time2);

            Assert.AreEqual(expectedEquality, actualEquality);
        }

        [DataTestMethod]
        [DataRow("12:34:56", "12:34:57", -1)]
        [DataRow("12:34:56", "12:34:56", 0)]
        [DataRow("12:34:56", "12:34:55", 1)]
        public void Time_CompareTo(string timeStr1, string timeStr2, int expectedComparision)
        {
            var time1 = new Time(timeStr1);
            var time2 = new Time(timeStr2);

            int actualComparison = time1.CompareTo(time2);

            Assert.IsTrue(actualComparison == expectedComparision);
        }

        [DataTestMethod]
        [DataRow("12:34:56", "01:23:45", "13:58:41")]
        [DataRow("23:59:59", "00:00:01", "00:00:00")]
        [DataRow("23:59:59", "00:00:02", "00:00:01")]
        public void Plus_TimeAndTimePeriod_Addition(string timeStr, string timePeriodStr, string expectedTime)
        {
            var time = new Time(timeStr);
            var timePeriod = new TimePeriod(timePeriodStr);
            var expected = new Time(expectedTime);

            var actual = time.Plus(timePeriod);

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("12:34:56", "01:23:45", "11:11:11")]
        [DataRow("00:00:01", "00:00:02", "23:59:59")]
        [DataRow("00:00:00", "00:00:01", "23:59:59")]
        public void Minus_TimeAndTimePeriod_Subtraction(string timeStr, string timePeriodStr, string expectedTime)
        {
            var time = new Time(timeStr);
            var timePeriod = new TimePeriod(timePeriodStr);
            var expected = new Time(expectedTime);

            var actual = time.Minus(timePeriod);

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("12:34:56", "01:23:45", "13:58:41")]
        [DataRow("23:59:59", "00:00:01", "00:00:00")]
        [DataRow("23:59:59", "00:00:02", "00:00:01")]
        public void OperatorPlus_TimeAndTimePeriod_Addition(string timeStr, string timePeriodStr, string expectedTime)
        {
            var time = new Time(timeStr);
            var timePeriod = new TimePeriod(timePeriodStr);
            var expected = new Time(expectedTime);

            var actual = time + timePeriod;

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("12:34:56", "01:23:45", "11:11:11")]
        [DataRow("00:00:01", "00:00:02", "23:59:59")]
        [DataRow("00:00:00", "00:00:01", "23:59:59")]
        public void OperatorMinus_TimeAndTimePeriod_Subtraction(string timeStr, string timePeriodStr, string expectedTime)
        {
            var time = new Time(timeStr);
            var timePeriod = new TimePeriod(timePeriodStr);
            var expected = new Time(expectedTime);

            var actual = time - timePeriod;

            Assert.AreEqual(expected, actual);
        }
    }

    [TestClass]
    public class TimePeriodTests
    {
        [DataTestMethod]
        [DataRow("00:00:00", true)]
        [DataRow("12:34:56", true)]
        [DataRow("23:59:59", true)]
        [DataRow("24:00:00", true)]
        [DataRow("12:60:00", true)]
        [DataRow("12:34:60", true)]
        [DataRow("-12:-34:-60", false)]
        [DataRow("12:34", false)]
        [DataRow("12:34:56:78", false)]
        [DataRow("1r:3s:5p", false)]
        public void Constructor_StringInput_Valid(string input, bool shouldSucceed)
        {
            bool succeeded = true;
            try
            {
                var timePeriod = new TimePeriod(input);
            }
            catch (ArgumentException)
            {
                succeeded = false;
            }

            Assert.AreEqual(shouldSucceed, succeeded);
        }

        [DataTestMethod]
        [DataRow(0, 0, 0, true)]
        [DataRow(12, 34, 56, true)]
        [DataRow(23, 59, 59, true)]
        [DataRow(-12, 34, 56, false)]
        [DataRow(23, -59, 59, false)]
        [DataRow(123, 15, -59, false)]
        [DataRow(24, 0, 0, true)]
        [DataRow(12, 60, 0, true)]
        [DataRow(12, 34, 60, true)]
        public void Constructor_IndividualValues_Valid(long hours, long minutes, long seconds, bool shouldSucceed)
        {
            bool succeeded = true;
            try
            {
                var timePeriod = new TimePeriod(hours, minutes, seconds);
            }
            catch (ArgumentException)
            {
                succeeded = false;
            }

            Assert.AreEqual(shouldSucceed, succeeded);
        }

        [DataTestMethod]
        [DataRow("12:34:56", "01:23:45", "13:58:41")]
        [DataRow("23:59:59", "00:00:01", "24:00:00")]
        [DataRow("00:00:01", "00:00:02", "00:00:03")]
        public void Plus_TimePeriodAndTimePeriod_Addition(string timePeriod1Str, string timePeriod2Str, string expectedTimePeriod)
        {
            var timePeriod1 = new TimePeriod(timePeriod1Str);
            var timePeriod2 = new TimePeriod(timePeriod2Str);
            var expected = new TimePeriod(expectedTimePeriod);
            var actual = timePeriod1.Plus(timePeriod2);

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("12:34:56", "01:23:45", "11:11:11", true)]
        [DataRow("120:00:00", "60:00:10", "59:59:50", true)]
        [DataRow("21:37:00", "42:12:24", "44:44:44", false)]
        [DataRow("00:00:00", "00:00:01", "00:00:00", false)]
        public void Minus_TimePeriodAndTimePeriod_Subtraction(string timePeriod1Str, string timePeriod2Str, string expectedTimePeriod, bool expectedResult)
        {

            bool actualResult = false;
            bool exception = false;
            TimePeriod actual = new TimePeriod();
            var timePeriod1 = new TimePeriod(timePeriod1Str);
            var timePeriod2 = new TimePeriod(timePeriod2Str);
            var expected = new TimePeriod(expectedTimePeriod);
            try { actual = timePeriod1.Minus(timePeriod2); }
            catch (Exception) { exception = true; }
            if (exception == false)
            {
                if (expected == actual)
                    actualResult = true;
            }


            Assert.AreEqual(expectedResult, actualResult);
        }

        [DataTestMethod]
        [DataRow("12:34:56", "01:23:45", "13:58:41")]
        [DataRow("23:59:59", "00:00:01", "24:00:00")]
        [DataRow("23:59:59", "00:00:02", "24:00:01")]
        public void OperatorPlus_TimePeriodAndTimePeriod_Addition(string timePeriod1Str, string timePeriod2Str, string expectedTimePeriod)
        {
            var timePeriod1 = new TimePeriod(timePeriod1Str);
            var timePeriod2 = new TimePeriod(timePeriod2Str);
            var expected = new TimePeriod(expectedTimePeriod);

            var actual = timePeriod1 + timePeriod2;

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("12:34:56", "01:23:45", "11:11:11", true)]
        [DataRow("120:00:00", "60:00:10", "59:59:50", true)]
        [DataRow("21:37:00", "42:12:24", "44:44:44", false)]
        [DataRow("00:00:00", "12:00:00", "00:00:00", false)]
        public void OperatorMinus_TimePeriodAndTimePeriod_Subtraction(string timePeriod1Str, string timePeriod2Str, string expectedTimePeriod, bool expectedResult)
        {
            bool actualResult = false;
            bool exception = false;
            TimePeriod actual = new TimePeriod();
            var timePeriod1 = new TimePeriod(timePeriod1Str);
            var timePeriod2 = new TimePeriod(timePeriod2Str);
            var expected = new TimePeriod(expectedTimePeriod);
            try { actual = timePeriod1 - timePeriod2; }
            catch (Exception) { exception = true; }
            if (exception == false)
            {
                if (expected == actual)
                    actualResult = true;
            }


            Assert.AreEqual(expectedResult, actualResult);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [DataTestMethod]
        [DataRow(1, 0, 0, 2, "2:00:00")]
        [DataRow(0, 30, 0, 3, "1:30:00")]
        [DataRow(0, 0, 45, 4, "0:03:00")]
        [DataRow(2, 30, 45, 0, "0:00:00")]
        public void Multiply_ShouldReturnCorrectResult(long h, long m, long s, long multiplier, string expected)
        {
            var timePeriod = new TimePeriod(h, m, s);
            var multipliedTimePeriod = timePeriod.Multiply(multiplier);

            Assert.AreEqual(expected, multipliedTimePeriod.ToString());
        }

        [DataTestMethod]
        [DataRow(1, 0, 0, 2, "2:00:00")]
        [DataRow(0, 30, 0, 3, "1:30:00")]
        [DataRow(0, 0, 45, 4, "0:03:00")]
        [DataRow(2, 30, 45, 0, "0:00:00")]
        public void OperatorMultiply_ShouldReturnCorrectResult(long h, long m, long s, long multiplier, string expected)
        {

            var timePeriod = new TimePeriod(h, m, s);
            var multipliedTimePeriod = timePeriod * multiplier;

            Assert.AreEqual(expected, multipliedTimePeriod.ToString());



        }
    }
}
