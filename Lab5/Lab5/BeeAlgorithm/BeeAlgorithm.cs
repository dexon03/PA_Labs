using Lab5.CliqueProblem;

namespace Lab5.BeeAlgorithmm;

public class BeeAlgorithm
{
    private int ForagersCount { get; }
    private int ScoutsCount { get; }
    private int MaxIterations = 1000;
    private int EliteBeesCount { get; set; }
    private Graph Graph { get; }

    private List<Bee> ForagerBees { get; }
    private List<Bee> ScoutBees { get; }

    public BeeAlgorithm(int foragers, int scouts, Graph graph)
    {
        ForagersCount = foragers;
        ScoutsCount = scouts;
        Graph = graph;
        EliteBeesCount = (ForagersCount + ScoutsCount) / 10;
        ForagerBees = new List<Bee>(ForagersCount);
        ScoutBees = new List<Bee>(ScoutsCount);
        
        InitializeBees();
    }
    
    public List<Bee> Solve()
    {
        List<Bee> BestSolutions = new List<Bee>();
        
        for (int i = 0; i < MaxIterations; i++)
        {
            foreach (var scout in ScoutBees)
            {
                scout.GenerateRandomSolution();
            }
            
            var EliteBees = GetBestSolutions();
        
            for (int j = 0; j < ForagersCount; j++)
            {
                ForagerBees[j].ExploreNeighbourhood(EliteBees[j % EliteBeesCount].Nodes);
            }
            ReplaceWorstSolution(EliteBees);
            var bestSolution = GetBestSolutions().First();
            BestSolutions.Add(bestSolution);
        }
        return BestSolutions;
    }

    
    private List<Bee> GetBestSolutions()
    {
        var UnitedBees = ForagerBees.Concat(ScoutBees).ToList();
        return UnitedBees.OrderByDescending(u => u.Fitness).Take(EliteBeesCount).ToList();
    }
    
    private void ReplaceWorstSolution(List<Bee> eliteBees)
    {
        var orderedForagers = ForagerBees.OrderBy(u => u.Fitness).ToList();
        ForagerBees.Clear();
        ForagerBees.AddRange(orderedForagers);
        for (int i = 0; i < EliteBeesCount; i++)
        {
            if(ForagerBees[i].Fitness < eliteBees[i].Fitness)
            {
                ForagerBees[i] = eliteBees[i];
            }
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