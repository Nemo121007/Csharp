using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
    {
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            GC.Collect();                   // Эти две строчки нужны, чтобы уменьшить вероятность того,
            GC.WaitForPendingFinalizers();  // что Garbadge Collector вызовется в середине измерений
                                            // и как-то повлияет на них.

            task.Run();                     // Тестовый прогон

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < repetitionCount; i++)
                task.Run();
            
            var time = (double)stopwatch.ElapsedMilliseconds;
            return (time / repetitionCount);
        }
    }

    [TestFixture]



    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            var stringBuilder = new StringBuilderTest();
            var stringt = new StringTest();

            var benchMark = new Benchmark();
            var timeStringBilder = benchMark.MeasureDurationInMs(stringBuilder, 10000);
            var timeString = benchMark.MeasureDurationInMs(stringt, 10000);
            Assert.Less(timeString, timeStringBilder);
        }
    }

    public class StringBuilderTest : ITask
    {
        public void Run()
        {
            var str = new StringBuilder();

            for (var i = 0; i < 10000; i++)
                str.Append('a');

            str.ToString();
        }
    }

    public class StringTest : ITask
    {
        public void Run()
        {
            new string('a', 10000);
        }
    }
}
