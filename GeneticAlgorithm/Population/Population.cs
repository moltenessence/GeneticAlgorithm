using GeneticAlgorithm.Parameters;

namespace GeneticAlgorithm.Population
{
    public class Population
    {
        public static int GenerationNumber = 0;

        public List<Gens.DNA> DNAs { get; set; }

        public double SummaryFitness { get; private set; }

        public double AverageFitness { get; private set; }

        public int IntermediateCopiesSummary { get; private set; }

        private AlgorithmParameters AlgorithmParameters { get; set; }

        public double MaxFitness { get; private set; }

        public Population(List<Gens.DNA> DNAs, AlgorithmParameters algorithmParameters)
        {
            this.DNAs = DNAs;
            AlgorithmParameters = algorithmParameters;
        }

        public Population Populate()
        {
            CalculateSummaryFitness();
            CalculateAverageFitness();
            CalculateNormalizedMagnitude();
            CalculateAmountOfDNACopies();
            GenerateIntermediatePopulation();
            GeneratePairsForCrossOver();
            Mutate(AlgorithmParameters.MutationRate);
            CalculateMaxFitness();
            GenerationNumber++;
            return this;
        }

        private void CalculateMaxFitness()
        {
            var max = DNAs.Max(x => x.Fitness);

            MaxFitness = max;
        }
        public void CalculateSummaryFitness()
        {
            var countedSum = DNAs.Sum(x => x.Fitness);

            SummaryFitness = countedSum;
        }

        public void CalculateAverageFitness()
        {
            var countedAvg = DNAs.Average(x => x.Fitness);

            AverageFitness = countedAvg;
        }

        private void CalculateNormalizedMagnitude()
        {
            foreach (var DNA in DNAs)
            {
                DNA.NormalizedMagnitude = DNA.Fitness / SummaryFitness;
                Console.WriteLine($"{DNA.ToString()} : NormalizedMagnitude = {DNA.NormalizedMagnitude}");
            }

        }

        private void CalculateAmountOfDNACopies()
        {
            foreach (var DNA in DNAs)
            {
                var awaitableAmountOfCopies = DNA.NormalizedMagnitude * DNAs.Count;
                var realAmountOfCopies = (int)Math.Round(awaitableAmountOfCopies);
                DNA.AmountOfDNACopies = realAmountOfCopies;

                Console.WriteLine($"{DNA.ToString()} : Awaitable Amount of copies = {awaitableAmountOfCopies} : " +
                                  $" Real Amount of copies = {realAmountOfCopies}");
            }

            IntermediateCopiesSummary = DNAs.Sum(x => x.AmountOfDNACopies);
            Console.Write($"Summary copies = {IntermediateCopiesSummary}\n");
        }

        private void GenerateIntermediatePopulation()
        {
            var intermediatePopulation = new List<Gens.DNA>();
            var populationCount = DNAs.Count;
            var random = new Random();

            DNAs = DNAs.OrderBy(x => x.NormalizedMagnitude).ToList();

            if (IntermediateCopiesSummary < AlgorithmParameters.PopulationSize)
            {
                int amountOfCopiesToAdd = AlgorithmParameters.PopulationSize - IntermediateCopiesSummary;

                DNAs.LastOrDefault().AmountOfDNACopies += amountOfCopiesToAdd;
            }

            for (int i = 0; i < populationCount;)
            {
                var randomValue = Math.Abs(random.NextDouble());

                Gens.DNA dnaForIntermediatePopulation = null;

                for (int j = 0; j < DNAs.Count - 1; j++)
                {
                    if (DNAs[j].NormalizedMagnitude > randomValue)
                    {
                        dnaForIntermediatePopulation = DNAs[j];
                        break;
                    }
                }

                dnaForIntermediatePopulation ??= DNAs.LastOrDefault();

                DNAs.Remove(dnaForIntermediatePopulation);

                if (dnaForIntermediatePopulation == null || dnaForIntermediatePopulation.AmountOfDNACopies == 0)
                    continue;

                for (int j = 0; j < dnaForIntermediatePopulation.AmountOfDNACopies; j++)
                {
                    intermediatePopulation.Add(dnaForIntermediatePopulation);
                    i++;
                }
            }

            DNAs.Clear();
            DNAs = new List<Gens.DNA>(intermediatePopulation);

            Console.WriteLine("Intermediate population:");
            foreach (var dna in DNAs)
            {
                Console.WriteLine(dna.ToString());
            }
        }

        private List<Gens.DNA> GeneratePairsForCrossOver()
        {
            for (int i = 0; i < DNAs.Count / 2; i++)
            {
                var DNAsToCrossOver = new List<Gens.DNA>();
                DNAsToCrossOver.Add(DNAs[i]);
                DNAsToCrossOver.Add(DNAs[^(i + 1)]);

                var (updatedFirst, updatedSecond) = CrossOver(DNAsToCrossOver[0], DNAsToCrossOver[1]);

                DNAs[i] = updatedFirst;
                DNAs[^(i + 1)] = updatedSecond;
            }

            Console.WriteLine("Intermediate population after crossing over:");
            foreach (var dna in DNAs)
            {
                Console.WriteLine(dna.ToString());
            }

            return DNAs;
        }

        private (Gens.DNA first, Gens.DNA decond) CrossOver(Gens.DNA first, Gens.DNA second)
        {
            Random random = new Random();
            var randValue = random.Next(1, InitialDataParameters.BinaryVectorLength - 1);
            var amountOfBitsToReplace = InitialDataParameters.BinaryVectorLength - randValue;

            var firstSubstring = first.Genes.TakeLast(amountOfBitsToReplace).ToArray();
            var secondSubstring = second.Genes.TakeLast(amountOfBitsToReplace).ToArray();

            var initialIndex = InitialDataParameters.BinaryVectorLength - amountOfBitsToReplace;

            for (int i = initialIndex, x = 0; i < InitialDataParameters.BinaryVectorLength - 1; i++, x++)
            {
                first.Genes[i] = secondSubstring[x];
                second.Genes[i] = firstSubstring[x];
            }

            var firstNewValue = BitsConverter.ToInt(first.Genes);
            var secondNewValue = BitsConverter.ToInt(second.Genes);

            return (new Gens.DNA(firstNewValue), new Gens.DNA(secondNewValue));
        }

        private void Mutate(float mutationRate)
        {
            var random = new Random();
            var randomIndex = random.Next(0, DNAs.Count - 1);

            if (random.NextDouble() <= mutationRate)
            {
                var i = random.Next(DNAs[randomIndex].Genes.Length - 1);
                DNAs[randomIndex].Genes[i] = !DNAs[randomIndex].Genes[i];
            }
        }

        public override string ToString()
        {
            return $"-------------------------------------------" +
                   $"Generation number {GenerationNumber}\n" +
                   $"Summary fitness: {SummaryFitness}\n" +
                   $"Max fitness: {MaxFitness}" +
                   $"\n\n";
        }
    }
}

