using Lab4;



Console.WriteLine("Do you want to see all the solutions or just the best one? \n 1 - All solutions \n 2 - Only the best solution(best by value). \n 3 - All solutions and in the end best solution(best by value)." );
Console.Write("Your choice: ");
string answer = Console.ReadLine();
answer = string.Join("", answer.Split());
while (answer != "1" && answer != "2" && answer != "3")
{
    Console.WriteLine("You write wrong answer. Try again.");
    Console.Write("Your choice: ");
    answer = Console.ReadLine();
    answer = string.Join("", answer.Split());
}

Population population = Solver.Solve();

switch (answer)
{
    case "1":
        for (int i = 0; i < population.Chromosomes.Count; i++)
        {
            Console.WriteLine("Chomosome: " + i + "  Weight: " + population.Chromosomes[i].GetWeight() + "  Value: " + population.Chromosomes[i].GetValue());
        }

        break;
    case "2":
        var bestChromosome = population.GetBestChromosome();
        Console.WriteLine("Best solution: Value:" + bestChromosome.GetValue() + "  Weight:" + bestChromosome.GetWeight());
        break;
    case "3":
        for (int i = 0; i < population.Chromosomes.Count; i++)
        {
            Console.WriteLine("Chomosome: " + i + "  Weight: " + population.Chromosomes[i].GetWeight() + "  Value: " + population.Chromosomes[i].GetValue());
        }

        var bestChromosome1 = population.GetBestChromosome();
        Console.WriteLine(" \nBest solution: Value:" + bestChromosome1.GetValue() + "  Weight:" + bestChromosome1.GetWeight());
        break;
}

