namespace Lab5.CliqueProblem;

public class Node
{
    public int Id { get; }
    public HashSet<Node> Connections { get; set; } = new();

    public Node(int id)
    {
        Id = id;
    }
    
    public bool IsConnected(Node node)
    {
        return Connections.Contains(node);
    }
    
    public void Connect(Node node)
    {
        Connections.Add(node);
        node.Connections.Add(this);
    }

    public override string ToString()
    {
        return "Node: " + Id;
    }
}