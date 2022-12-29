namespace Lab5.CliqueProblem;

public class CliqueProblem
{
    Graph graph;

    public CliqueProblem(int countOfNodes,int NodesDegree)
    {
        graph = new Graph(countOfNodes, NodesDegree);
    }

    public Graph GetBee(Node node = null)
    {
        Random random = new Random();
        if (node is null)
        {
            node = graph.Nodes[random.Next(0, graph.Nodes.Count)];
        }
        var neighbourNode = node.Connections[random.Next(0, node.Connections.Count)];
        
        
        
        
    }
    
}