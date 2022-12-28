using System.Text.Json;
using Lab3.Models;

namespace Lab3.Data;

public class ApplicationDbContext
{
    public List<NodeValue>? NodeValues { get; set; } = new List<NodeValue>();
    private readonly string _path = Environment.CurrentDirectory + "DB.json";
    
    public ApplicationDbContext()
    {
        using FileStream file = new FileStream(_path, FileMode.OpenOrCreate);
        if (file.Length != 0)
        {
            NodeValues = JsonSerializer.Deserialize<List<NodeValue>>(file);
        }
    }
    
    

    public void SaveChanges()
    {
        using (FileStream file = new FileStream(_path, FileMode.Create)) 
        {
            JsonSerializer.Serialize(file, NodeValues);
        }
    }

}