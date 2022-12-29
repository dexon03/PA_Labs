namespace Lab5.BeeAlgorithmm;

public class BeeAlgorithm
{
    public int Foragers { get; set; }
    public int Scouts { get; set; }
    public int MaxIterations { get; set; }

    public BeeAlgorithm(int foragers, int scouts, int maxIterations)
    {
        Foragers = foragers;
        Scouts = scouts;
        MaxIterations = maxIterations;
    }
    
}