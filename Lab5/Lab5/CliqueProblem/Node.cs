﻿namespace Lab5.CliqueProblem;

public class Node
{
    public int Id { get; set; }
    public List<Node> Connections { get; set; }

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
}