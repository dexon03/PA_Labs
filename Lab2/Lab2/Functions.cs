namespace Lab2;

public static class FunctionsAndConstants
{
    public static readonly int[,] goalState = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } }; 
    public static List<State> FindPath(State state)
    {
        var result = new List<State>();
        result.Add(state);
        AddStatesToPath(result,state);
        return result;
    }

    private static void AddStatesToPath(List<State> pathList,State state)
    {
        if (state.Parent != null)
        {
            pathList.Add(state.Parent);
            AddStatesToPath(pathList,state.Parent);
        }   
    }
    public static List<State> GenerateChildren(State state)
    {
        (int x, int y) = state.Board.IndexOfBlank();
        var children = new List<State>();

        var rightState = state.MoveBlankToRight(x, y);
        if (rightState != null)
        {
            children.Add(rightState);
        }

        var leftState = state.MoveBlankToLeft(x, y);
        if (leftState != null)
        {
            children.Add(leftState);
        }

        var downState = state.MoveBlankToDown(x, y);
        if (downState != null)
        {
            children.Add(downState);
        }

        var upState = state.MoveBlankToUp(x, y);
        if (upState != null)
        {
            children.Add(upState);
        }
        return children;
    }
}