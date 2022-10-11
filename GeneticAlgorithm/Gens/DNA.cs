using GeneticAlgorithm.Parameters;
using System.Text;
using static GeneticAlgorithm.FitnessFunction.FitnessFunction;

namespace GeneticAlgorithm.Gens;
public class DNA
{
    public bool[] Genes { get; private set; }
    public int Value { get; private set; }
    public double Fitness { get; private set; }
    public double NormalizedMagnitude { get; set; }
    public int AmountOfDNACopies { get; set; }

    public DNA(int value)
    {
        Value = value;

        CalculateBinary();
        CalculateFitness();
    }

    public void CalculateFitness()
    {
        var result = CalculateFitnessFunctionResult(Value);

        Fitness = result;
    }

    public void CalculateBinary()
    {
        Genes = BitsConverter.ToBitsArray(Value, InitialDataParameters.BinaryVectorLength);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        foreach (var bit in Genes)
        {
            sb.Append(bit ? '1' : '0');
        }

        sb.Append($": {Value}");

        return sb.ToString();
    }
}