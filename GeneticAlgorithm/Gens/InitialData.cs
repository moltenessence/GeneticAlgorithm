using GeneticAlgorithm.Parameters;

namespace GeneticAlgorithm.DNA
{
    public static class InitialData
    {
        public static readonly IList<int> Data = GetInitialArray();

        public static readonly List<bool[]> DataInBinary = ConvertDataToBits();

        private static List<int> GetInitialArray() => Enumerable.Range(0, 100).ToList();

        private static List<bool[]> ConvertDataToBits()
        {
            List<bool[]> list = new List<bool[]>();

            foreach (var value in Data)
            {
                var bitArray = BitsConverter.ToBitsArray(value, InitialDataParameters.BinaryVectorLength);

                list.Add(bitArray);
            }

            return list;
        }

    }
}
