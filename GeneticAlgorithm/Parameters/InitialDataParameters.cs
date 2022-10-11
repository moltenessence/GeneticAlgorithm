namespace GeneticAlgorithm.Parameters
{
    public static class InitialDataParameters
    {
        public const int SolutionAccuracy = 1;

        public const float LeftValueOfAllowedInterval = 0;
        public const float RightValueOfAllowedInterval = 100;

        public static int AmountOfIntervalParts = Convert.ToInt32((RightValueOfAllowedInterval - LeftValueOfAllowedInterval) * Math.Pow(10.0, SolutionAccuracy));

        public static readonly int BinaryVectorLength = GetBinaryVectorLength();
        private static int GetBinaryVectorLength()
        {
            var amountOfIntervalParts = AmountOfIntervalParts;

            var amountOfBits = 0;

            for (int i = 0; i < PowsForNumberTwo.Array.Length; i++)
            {
                if (amountOfIntervalParts >= PowsForNumberTwo.Array[i])
                    amountOfBits = i;
            }

            return amountOfBits;
        }
    }
}
