using Lab4;

Population population = Solver.Solve();

for (int i = 0; i < population.Chromosomes.Count; i++)
{
    Console.WriteLine("Chomosome: " + i + "  Weight: " + population.Chromosomes[i].GetWeight + "  Value: " + population.Chromosomes[i].GetValue);
}
