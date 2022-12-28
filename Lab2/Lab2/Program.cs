using Lab2;

Board board = new Board();
Console.WriteLine("--------------------------");
ShowMenu();
string? option;
do
{
    Console.Write("\n Your variant: ");
    option = Console.ReadLine()!;

    option = String.Join("", option.Split())!;
    switch (option!)
    {
        case "1":
            board = new Board();
            board.GenerateBoard();
            board.OutPut();
            break;
        case "2":
            string input = UserInput();
            board = SetBoardFromInput(input);
            break;
        case "3":
            int[,] arr = 
            {
                {3, 6, 8},
                {5, 4, 1},
                {9, 2, 7},
            };
            PrintBoard(arr);
            board = new Board(arr);
            break;
        default:
            Console.WriteLine("Invalid option. Try 1,2 or 3.");
            break;
    }
    
} while (option != "1" && option != "2" && option != "3");

string? var;
do
{
    Console.Write("Choose algorithm 1 - LDFS, 2 - A* :");
    var = string.Join("",Console.ReadLine()?.Split("")!);
} while (var != "1" && var != "2");

var beginState = new State(board,null!,null!,0);


if (var == "1")
{
    DoLDFSAlgo();
}
else
{ 
   DoAStarAlgo();
}

void ShowMenu()
{
    Console.WriteLine("Select an option: ");
    Console.WriteLine("1: Generate random board");
    Console.WriteLine("2: Create board by user input");
    Console.WriteLine("3: Test verified board");

}

string UserInput()
{
    Console.WriteLine(
        "Write numbers 1-9 separated by a space without repetitions. Number 9 means '*'.\n Example: 1 2 3 4 5 6 7 8 9");
    Console.Write("Your data: ");
    string? input = Console.ReadLine();
    while (!UserValidInput(input))
    {
        Console.WriteLine("You wrote wrong data. Please write numbers 1-9 separated by a space without repetitions.\n Example: 1 2 3 4 5 6 7 8 9");
        Console.Write("Your data: ");
        input = Console.ReadLine();
    }

    return input!;
}

Board SetBoardFromInput(string input)
{
    var splitInput = input.Split(" ");
    int[,] boardMatrix = new int[3,3];
    int indexOfArr = 0;
    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 3; j++)
        {
            boardMatrix[i, j] = int.Parse(splitInput[indexOfArr]);
            indexOfArr++;
        }
    }

    Board result = new Board(boardMatrix);

    return result;
}

void PrintBoard(int[,] arr)
{
    for (int i = 0; i < arr.GetLength(0); i++)
    {
        for (int j = 0; j < arr.GetLength(1); j++)
        {
            Console.Write(arr[i,j] + " ");
        }

        Console.WriteLine();
    }
}

bool UserValidInput(string? userInput)
{
    if (userInput is null) return false;
    int[] validNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    var arrFromInput = userInput.Split(" ");
    if (arrFromInput.Length != 9) return false;
    foreach (var symbol in arrFromInput)
    {
        if (arrFromInput.Count(u => u == symbol) > 1)
        {
            return false;
        }
        bool isDigit = int.TryParse(symbol, out int n);
        if (!isDigit) return false;
        if (!validNumbers.Contains(n)) return false;
    }
    return true;
}

void DoLDFSAlgo()
{
    int iterations;
    int angles;
    int countOfState;
    Console.Write("Choose limit for LDFS: ");
    int limit = Int32.Parse(Console.ReadLine()!);
    Console.WriteLine("Algorithm started. Please wait...");
    State? state = LDFS.Solve(beginState,limit,out iterations,out angles,out countOfState);
    if (state != null)
    {
        Console.WriteLine("Solution:");
        state.Board.OutPut();
        var path = FunctionsAndConstants.GetPath(state);
        Console.WriteLine("Global count of states created during algorithm: " + path.Count);
    }
    else
    {
        Console.WriteLine("Cutoff/failure");
    }
    Console.WriteLine("Count of iterations: " + iterations);
    Console.WriteLine("Count of angles: " + angles);
    Console.WriteLine("Count of states: " + countOfState);
}



void DoAStarAlgo()
{
    int iterations;
    int angles;
    int countOfState;
    int stateInMemory;
    Console.WriteLine("Algorithm started. If it won't find solution in 1 minute, algorithm will be stopped.");
    State? state = AStar.Solve(beginState,out iterations,out angles,out countOfState,out stateInMemory);
    if (state != null)
    {
        Console.WriteLine("Solution:");
        state.Board.OutPut();
    }
    else
    {
        Console.Write("Not found");
    }
    Console.WriteLine("Count of states in memory: " + stateInMemory);
    Console.WriteLine("Count of iterations: " + iterations);
    Console.WriteLine("Count of deaf angles: " + angles);
    Console.WriteLine("Global count of states created during algorithm: " + countOfState);
}
