using Lab5.CliqueProblem;

namespace Lab5.BeeAlgorithmm;

public class BeeAlgorithm
{
    private int ForagersCount { get; }
    private int ScoutsCount { get; }
    private int MaxIterations { get; } = 1000;
    private int EliteBeesCount { get; set; } = 10;
    private Graph Graph { get; }

    private List<Bee> ForagerBees { get; }
    private List<Bee> ScoutBees { get; }

    public BeeAlgorithm(int foragers, int scouts, Graph graph)
    {
        ForagersCount = foragers;
        ScoutsCount = scouts;
        Graph = graph;
        
        ForagerBees = new List<Bee>(ForagersCount);
        ScoutBees = new List<Bee>(ScoutsCount);
        
        InitializeBees();
    }
    
    public void Solve()
    {
        for (int i = 0; i < MaxIterations; i++)
        {
            foreach (var scout in ScoutBees)
            {
                scout.GenerateRandomSolution();
            }

            var EliteBees = GetBestSolutions();
            for (int j = 0; j < ForagersCount; j++)
            {
                ForagerBees[j].ModifySolution(EliteBees[j % EliteBeesCount].nodes);
            }
            replaceWorstSolution(EliteBees);
            var bestSolution = GetBestSolutions().First();
            Console.WriteLine($"Iteration {i} best solution: {bestSolution.nodes.Count} nodes, {bestSolution.Fitness} fitness");
        }
    }

    
    private List<Bee> GetBestSolutions()
    {
        var UnitedBees = ForagerBees.Concat(ScoutBees).ToList();
        return UnitedBees.OrderByDescending(u => u.Fitness).Take(EliteBeesCount).ToList();
    }
    
    private void replaceWorstSolution(List<Bee> eliteBees)
    {
        var orderedForagers = ForagerBees.OrderBy(u => u.Fitness).ToList();
        ForagerBees.Clear();
        ForagerBees.AddRange(orderedForagers);
        for (int i = 0; i < EliteBeesCount; i++)
        {
            ForagerBees[i] = eliteBees[i];
        }        
        
    }
    
    private void InitializeBees()
    {
        for (int i = 0; i < ForagersCount; i++)
        {
            ForagerBees.Add(new Bee(Graph));
        }
        
        for (int i = 0; i < ScoutsCount; i++)
        {
            ScoutBees.Add(new Bee(Graph));
        }
    }
    
}