namespace Lab2;

public class State : IComparable<State>
{
    private int outOfPlace;
    public int F;
    public Board Board { get; set; }
    public State? Parent { get; set; }
    public string? LastMove { get; set; }
    public int SearchDepth { get; set; }

    public State(Board board,State parent,string lastMove,int searchDepth)
    {
        this.Board = board;
        this.Parent = parent;
        this.LastMove = lastMove;
        this.SearchDepth = searchDepth;
        this.F = CalculateF();
    }

    public int OutOfPlace()
    {
        var board = Board.Matrix;
        int counter = 1;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (counter == 9) break;
                if (board[i, j] != counter)
                {
                    this.outOfPlace++;
                }

                counter++;
            }
        }
        return this.outOfPlace;
    }

    public int CalculateF()
    {
        outOfPlace = OutOfPlace();
        return SearchDepth + outOfPlace;
    }
    public State? MoveBlankToLeftState(int i,int j) 
    {
        if (j == 0 || this.LastMove == "right")
        {
            return null;
        }

        var newState = this.Clone();
        (newState.Board.Matrix[i, j - 1], newState.Board.Matrix[i, j]) = (newState.Board.Matrix[i, j], newState.Board.Matrix[i, j - 1]);
        newState.LastMove = "left";
        newState.Parent = this;
        newState.SearchDepth++;
        newState.F = newState.CalculateF();
        return newState;
    }
    public State? MoveBlankToRightState(int i,int j) 
    {
        if (j == 2 || this.LastMove == "left")
        {
            return null;
        }
        var newState = this.Clone();
        (newState.Board.Matrix[i, j + 1], newState.Board.Matrix[i, j]) = (newState.Board.Matrix[i, j], newState.Board.Matrix[i, j + 1]);
        newState.LastMove = "right";
        newState.Parent = this;
        newState.SearchDepth++;
        newState.F = newState.CalculateF();
        return newState;
    }
    public State? MoveBlankToUpState(int i,int j) 
    {
        if (i == 0 || this.LastMove == "down")
        {
            return null;
        }
        var newState = this.Clone();
        (newState.Board.Matrix[i-1, j], newState.Board.Matrix[i, j]) = (newState.Board.Matrix[i, j], newState.Board.Matrix[i-1, j]);
        newState.LastMove = "up";
        newState.Parent = this;
        newState.SearchDepth++;
        newState.F = newState.CalculateF();
        return newState;
    }
    public State? MoveBlankToDownState(int i,int j) 
    {
        if (i == 2 || this.LastMove == "up")
        {
            return null;
        }
        var newState = this.Clone();
        (newState.Board.Matrix[i+1, j], newState.Board.Matrix[i, j]) = (newState.Board.Matrix[i, j], newState.Board.Matrix[i+1, j]);
        newState.LastMove = "down";
        newState.Parent = this;
        newState.SearchDepth++;
        newState.F = newState.CalculateF();
        return newState;
    }

    private State Clone()
    {
        int[,] newMatrix = new int[3, 3];
        for (int i = 0; i < this.Board.Matrix.GetLength(0); i++)
        {
            for (int j = 0; j < this.Board.Matrix.GetLength(1); j++)
            {
                newMatrix[i, j] = this.Board.Matrix[i, j];
            }
        }

        Board newBoard = new Board(newMatrix);
        return new State(newBoard, this.Parent!, this.LastMove!, this.SearchDepth);
    }

    public int CompareTo(State? obj)
    {
        int current = this.outOfPlace;
        int? comparableObj = obj?.outOfPlace;
        if (current < comparableObj) return 1;
        if (current > comparableObj) return -1;
        return 0;
    }
    
}