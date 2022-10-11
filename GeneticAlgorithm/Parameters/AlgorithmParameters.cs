namespace GeneticAlgorithm.Parameters
{
    public class AlgorithmParameters
    {
        public int PopulationSize { get; private set; }
        public float MutationRate { get; private set; }
        public float CrossingOverOperatorRate { get; private set; }
        public int MaxAmountOfGenerations { get; private set; }

        public AlgorithmParameters(int populationSize = 12,
            float mutationRate = 0.01f,
            float crossingOverOperatorRate = 0.01f,
            int maxAmountOfGenerations = 50)
        {
            PopulationSize = populationSize;
            MutationRate = mutationRate;
            CrossingOverOperatorRate = crossingOverOperatorRate;
            MaxAmountOfGenerations = maxAmountOfGenerations;
        }

    }
}
