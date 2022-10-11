using GeneticAlgorithm.DNA;
using GeneticAlgorithm.Gens;
using GeneticAlgorithm.Parameters;
using GeneticAlgorithm.Population;

public class Algorithm
{
    public AlgorithmParameters AlgorithmParameters { get; set; }
    public Population Population { get; private set; }
    public Algorithm(AlgorithmParameters algorithmParameters)
    {
        AlgorithmParameters = algorithmParameters;
        Population = CreateInitialPopulation();
    }

    public IEnumerable<Population> Run()
    {
        for (int i = 0; i < AlgorithmParameters.MaxAmountOfGenerations; i++)
        {
            Population = Population.Populate();
            yield return Population;
        }
    }

    private Population CreateInitialPopulation()
    {
        var initialValues = new List<int>();

        for (int i = 0; i < AlgorithmParameters.PopulationSize; i++)
        {
            var rand = new Random();
            var index = rand.Next(0, InitialData.Data.Count - 1);

            if (!initialValues.Contains(InitialData.Data[index]))
            {
                initialValues.Add(InitialData.Data[index]);
            }
            else
            {
                i--;
            }
        }

        var dnas = new List<DNA>();

        foreach (var value in initialValues)
        {
            dnas.Add(new DNA(value));
        }

        return new Population(dnas, AlgorithmParameters);
    }
}