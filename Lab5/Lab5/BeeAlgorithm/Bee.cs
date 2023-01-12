using Lab5.CliqueProblem;

namespace Lab5.BeeAlgorithmm;

public class Bee
{
    private Graph Graph { get; }

    public HashSet<Node> Nodes = new();

    public int Fitness => IsClique(Nodes) ? Nodes.Count : 0;

    public Bee(Graph graph)
    {
        Graph = graph;
    }
    
    public void GenerateRandomSolution()
    {
        Nodes.Clear();
        var random = new Random();
        var nodesCount = Graph.Nodes.Count;
        var randomSolutionNodesCount = random.Next(2,Graph.NodeDegree+1);
        for (int i = 0; i < randomSolutionNodesCount; i++)
        {
            var randomNode = Graph.Nodes.ElementAt(random.Next(0, nodesCount));
            while (Nodes.Contains(randomNode))
            {
                randomNode = Graph.Nodes.ElementAt(random.Next(0, nodesCount));
            }
            Nodes.Add(randomNode);
        }
    }

    public void ExploreNeighbourhood(HashSet<Node> newNodes)
    {
        Nodes.Clear();
        foreach (var node in newNodes)
        {
            Nodes.Add(node);
        }

        if (newNodes.Count != 0)
        {
            var neighbourNodes = newNodes.Select(node => new HashSet<Node> (node.Connections )).ToList();
            HashSet<Node> neighbourNodesMutual = neighbourNodes.Aggregate((x, y) => x.Intersect(y).ToHashSet());
            if (neighbourNodesMutual.Count != 0)
            {
                var listNeighbourNodesMutual = neighbourNodesMutual.ToList();
                var randomMutualNode = listNeighbourNodesMutual[new Random().Next(0, listNeighbourNodesMutual.Count)];
                Nodes.Add(randomMutualNode);
            }
        }
        
    }
    private bool IsClique(HashSet<Node> nodes)
    {
        foreach (var node in nodes)
        {
            foreach (var otherNode in nodes)
            {
                if(node != otherNode && !node.IsConnected(otherNode))
                {
                    return false;
                }
            }
        }
        return true;
    }
}