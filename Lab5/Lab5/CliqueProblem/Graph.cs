namespace Lab5.CliqueProblem;

public class Graph
{
    public List<Node> Nodes { get; } = new List<Node>();

    public Graph(int count, int NodeDegreeCount)
    {
        Nodes = new List<Node>(count);
        for (int i = 0; i < count; i++)
        {
            Nodes.Add(new Node(i));
        }
        GenerateConnections(NodeDegreeCount);
    }

    private void GenerateConnections(int NodeDegreeCount)
    {
        var avgNodeDegree = NodeDegreeCount / 2;
        var avgEdgeCount = avgNodeDegree * Nodes.Count;
        
        int counter = 0;
        Random random = new Random();
        while (counter < avgEdgeCount)
        {
            var node1 = Nodes[random.Next(Nodes.Count)];
            var node2 = Nodes[random.Next(Nodes.Count)];
            
            if (node1 == node2)
                continue;
            if (!node1.IsConnected(node2))
            {
                node1.Connect(node2);
            }
            counter++;
        }
    }

    public bool isClique()
    {
        foreach (var node in Nodes)
        {
            if(node.Connections.Count != Nodes.Count - 1)
                return false;
        }
        return true;
    }
    
    public int fitness()
    {
        return Nodes.Count * Convert.ToInt32(isClique());
    }
}