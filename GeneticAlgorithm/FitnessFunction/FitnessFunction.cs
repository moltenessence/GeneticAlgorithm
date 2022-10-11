namespace GeneticAlgorithm.FitnessFunction
{
    public static class FitnessFunction
    {
        private static readonly Func<int, double> Function = (t) =>
          (t + 1.3f) * Math.Sin(0.5f * Math.PI * t + 1);

        public static double CalculateFitnessFunctionResult(int argument) => Math.Abs(Function.Invoke(argument));
    }
}
