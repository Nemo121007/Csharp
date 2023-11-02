using System.Collections.Generic;

namespace StructBenchmarking
{
    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            double time;
            for (var i = 16; i < 1024; i *= 2)
            {
                time = benchmark.MeasureDurationInMs(new ClassArrayCreationTask(i), repetitionsCount);
                classesTimes.Add(new ExperimentResult(i, time));

                time = benchmark.MeasureDurationInMs(new StructArrayCreationTask(i), repetitionsCount);
                structuresTimes.Add(new ExperimentResult(i, time));
            }

            return new ChartData
            {
                Title = "Create array",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            double time;
            for (var i = 16; i < 1024; i *= 2)
            {
                time = benchmark.MeasureDurationInMs(new MethodCallWithClassArgumentTask(i), repetitionsCount);
                classesTimes.Add(new ExperimentResult(i, time));

                time = benchmark.MeasureDurationInMs(new MethodCallWithStructArgumentTask(i), repetitionsCount);
                structuresTimes.Add(new ExperimentResult(i, time));
            }

            return new ChartData
            {
                Title = "Call method with argument",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }
    }
}
