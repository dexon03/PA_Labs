namespace Lab5.CliqueProblem;

public class Graph
{
    public HashSet<Node> Nodes { get; }

    public Graph(int NodeCount, int NodeDegree)
    {
        Nodes = new HashSet<Node>(NodeCount);
        for (int i = 0; i < NodeCount; i++)
        {
            Nodes.Add(new Node(i));
        }
        GenerateConnections(NodeDegree);
        
    }

    private void GenerateConnections(int NodeDegreeCount)
    {
        foreach (var node in Nodes)
        {
            var nodeDegree = new Random().Next(1, NodeDegreeCount);
            for (int i = 0; i < nodeDegree; i++)
            {
                var randomNode = Nodes.ElementAt(new Random().Next(0, Nodes.Count));
                if (randomNode != node)
                {
                    node.Connect(randomNode);
                }
            }
        }
    }

    public void Append(Node node)
    {
        Nodes.Add(node);
        foreach (var nodeConnection in node.Connections)
        {
           nodeConnection.Connections.Add(node); 
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