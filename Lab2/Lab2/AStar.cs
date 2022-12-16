using System.Diagnostics;

namespace Lab2;

public static class AStar
{
    public static State? Solve(State state,out int iterations,out int angles,out int countOfState,out int stateInMemory)
    {
        iterations = 0;
        angles = 0;
        countOfState = 1;
        stateInMemory = 0;
        DateTime data1 = DateTime.Now;
        var OpenList = new PriorityQueue<State, int>();
        var ClosedList = new List<State>();
        OpenList.Enqueue(state,state.F);
        while (OpenList.Count != 0)
        {
            iterations++;
            var current = OpenList.Dequeue();
            ClosedList.Add(current);
            if (current.Board.isEqual(FunctionsAndConstants.goalState))
            {
                stateInMemory = OpenList.Count;
                return current;
            }
            
            var children = FunctionsAndConstants.GenerateChildren(current);
            if (children.Count == 0)
            {
                angles++;
            }
            foreach (var child in children)
            {
                if (!ClosedList.Contains(child))
                {
                    OpenList.Enqueue(child,child.F);
                    countOfState++;
                }
            }

            if (!CheckStopCondition(data1))
            {
                stateInMemory = OpenList.Count;
                return null;
            }
            
        }
        stateInMemory = OpenList.Count;
        return null;
    }

    private static bool CheckStopCondition(DateTime data1)
    {
        long memoryUsed = Process.GetCurrentProcess().PrivateMemorySize64/1000000000;
        if (memoryUsed > 1)
        {
            Console.WriteLine("Memory used is out of available");
            return false;
        }
        DateTime data2 = DateTime.Now;
        if ((data2 - data1).Minutes == 1)
        {
            Console.WriteLine("Timeout");
            return false;
        }

        return true;
    }
}