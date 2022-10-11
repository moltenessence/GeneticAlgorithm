using GeneticAlgorithm.Parameters;

var algorithmParameters = new AlgorithmParameters(populationSize: 10, mutationRate: 0.01f, crossingOverOperatorRate: 0.01f, maxAmountOfGenerations: 10);

Algorithm algorithm = new Algorithm(algorithmParameters);

var result = algorithm.Run();

foreach (var population in result)
{
    Console.WriteLine(population.ToString());
}