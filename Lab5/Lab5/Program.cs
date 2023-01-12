using System.Collections.Immutable;
using System.Threading.Channels;
using Lab5.BeeAlgorithmm;
using Lab5.CliqueProblem;

Console.WriteLine("Hi, I'm a bee algorithm solver for the clique problem");
Console.WriteLine("Please enter the number of vertices in the graph. It should be positive number. Minimum 3");
Console.WriteLine("Your number: ");
int countOfVertex;
while (!int.TryParse(Console.ReadLine(), out countOfVertex) || countOfVertex <= 3)
{
    Console.WriteLine("Please enter a valid number");
    Console.WriteLine("Your number: ");
}

int maximumDegree;
Console.WriteLine("Please enter the maximum degree of the graph. It should be positive number. Minimum 1. Maximum " + countOfVertex + ".");
while (!int.TryParse(Console.ReadLine(), out maximumDegree) || maximumDegree <= 1 || maximumDegree > countOfVertex)
{
    Console.WriteLine("Please enter a valid number");
    Console.WriteLine("Your number: ");
}

Console.WriteLine("Please enter the number of scouts. It should be positive number. Minimum 1.");
int countOfScouts;
while (!int.TryParse(Console.ReadLine(), out countOfScouts) || countOfScouts <= 1)
{
    Console.WriteLine("Please enter a valid number");
    Console.WriteLine("Your number: ");
}

Console.WriteLine("Please enter the number of foragers. It should be positive number. Minimum 1.");
int countOfForagers;
while (!int.TryParse(Console.ReadLine(), out countOfForagers) || countOfForagers <= 1)
{
    Console.WriteLine("Please enter a valid number");
    Console.WriteLine("Your number: ");
}


Graph graph = new Graph(countOfVertex,maximumDegree);

Console.WriteLine("Graph generated. Do you want to see best solutions on each iteration and the best solution in the end, or only the best solution?" +
                  "\n1. Best solutions on each iteration and the best solution in the end" +
                  "\n 2. Only the best solution");
Console.Write("Your choice: ");
int choice;
while (!int.TryParse(Console.ReadLine(), out choice) || choice != 1 && choice != 2)
{
    Console.WriteLine("Please enter a 1 or 2");
    Console.Write("Your choice: ");
}

Console.WriteLine("Algorithm searching.Please wait.");

var algo = new BeeAlgorithm(countOfForagers,countOfScouts,graph);
var solutions = algo.Solve();

switch (choice)
{
    case 1:
        PrintAllSolutions(solutions);
        break;
    case 2: 
        var bestSolution = GetBestSolution(solutions);
        Console.WriteLine("The best solution has " + bestSolution.Fitness + " fitness and nodes = [" + String.Join(", ",bestSolution.Nodes) + "]");
        break;
}

void PrintAllSolutions(List<Bee> solutions)
{
    for (int i = 0; i < solutions.Count; i++)
    {
        if (solutions[i].Nodes.Count == 0 || solutions[i].Fitness == 0)
        {
            Console.WriteLine("On iteration " + i + " algo couldn't find solution");
        }
        else
        {
            string Nodes = "";
            foreach (var node in solutions[i].Nodes)
            {
                Nodes += node.ToString() + ", ";
            }
            Console.WriteLine("On iteration " + i + " best solution has " + solutions[i].Fitness + " fitness and nodes = [" + Nodes + "]");
        }
            
    }

    var bestSolution = GetBestSolution(solutions);
    Console.WriteLine("The best solution has " + bestSolution.Fitness + " fitness and nodes = [" + String.Join(", ",bestSolution.Nodes) + "]");
}

Bee GetBestSolution(List<Bee> solutions)
{
    return solutions.MaxBy(u => u.Fitness);
}

