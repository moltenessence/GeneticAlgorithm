using GeneticAlgorithm.Parameters;

namespace GeneticAlgorithm
{
    public static class BitsConverter
    {
        public static bool[] ToBitsArray(int input, int numberOfBits)
        {
            return Enumerable.Range(0, numberOfBits)
                .Select(bitIndex => 1 << bitIndex)
                .Select(bitMask => (input & bitMask) == bitMask)
                .Reverse()
                .ToArray();
        }

        public static int ToInt(bool[] binaryValue)
        {
            var arrayOfBits = binaryValue.Reverse();

            var intResult = 0;

            for (int currentBitIndex = arrayOfBits.Count() - 1; currentBitIndex >= 0; currentBitIndex--)
            {
                var currentBitValue = arrayOfBits.ElementAt(currentBitIndex) ? 1 : 0;

                intResult += currentBitValue * PowsForNumberTwo.Array[currentBitIndex];
            }

            return intResult;
        }
    }
}
