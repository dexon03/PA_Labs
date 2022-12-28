using Lab3.Data;

namespace Lab3.Models;

public static class SeedData
{
    public static void Generate(IApplicationBuilder app)
    {
        var serviceScope = app.ApplicationServices.CreateScope();
        ApplicationDbContext? dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        var nodeValues = dbContext!.NodeValues;
        if (nodeValues != null && !nodeValues.Any())
        {
            for (int i = 0; i < 10000; i++)
            {
                dbContext.NodeValues?.Add(new NodeValue{NodeValueId = i+1, Value = Guid.NewGuid().ToString()});
            }
        }
        dbContext.SaveChanges();
    }
}