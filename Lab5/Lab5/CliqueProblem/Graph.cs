namespace Lab5.CliqueProblem;

public class Graph
{
    public HashSet<Node> Nodes { get; }
    public int NodeDegree { get; }

    public Graph(int NodeCount, int NodeDegree)
    {
        this.NodeDegree = NodeDegree;
        Nodes = new HashSet<Node>(NodeCount);
        for (int i = 0; i < NodeCount; i++)
        {
            Nodes.Add(new Node(i));
        }
        GenerateConnections();
        
    }

    private void GenerateConnections()
    {
        foreach (var node in Nodes)
        {
            var nodeDegree = new Random().Next(2, NodeDegree);
            for (int i = 0; i < nodeDegree; i++)
            {
                var randomNode = Nodes.ElementAt(new Random().Next(0, Nodes.Count));
                while (randomNode == node)
                {
                    randomNode = Nodes.ElementAt(new Random().Next(0, Nodes.Count));
                }
                
                node.Connect(randomNode);
            }
        }
        
    }
}