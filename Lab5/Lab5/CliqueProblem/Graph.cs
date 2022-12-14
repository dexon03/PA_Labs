namespace Lab5.CliqueProblem;

public class Graph
{
    public List<List<int>> Vertexes { get; }

    public Graph(int count)
    {
        Vertexes = new List<List<int>>(count);
        for (int i = 0; i < count; i++)
        {
            Vertexes.Add(new List<int>());
        }
    }

    public void GenerateGraph()
    {
        int n, m;
        while (!CheckProp())
        {
            Random random = new Random();
            n = random.Next(0,300);
            do
            {
                m = random.Next(0, 300);
            } while (m == n);
            AddEdge(n,m);
        }
    }

    private void AddEdge(int u, int v)
    {
        if (!Vertexes[u].Contains(v))
        {
            Vertexes[u].Add(v);
            Vertexes[v].Add(u);
        }    
    }
    
    private bool CheckProp()
    {
        foreach (var vertex in Vertexes)
        {
            if (vertex.Count is < 2 or > 30)
            {
                return false;
            }
        }

        return true;
    }
}