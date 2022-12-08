namespace AOC2022.D8;

public record Tree(int Row, int Col, int Height, bool Visible = false)
{
    public int Row { get; } = Row;
    public int Col { get; } = Col;
    public int Height { get; } = Height;
    public bool Visible { get; set; } = Visible;
}

public class Grid
{
    public Grid(string file)
    {
        var lines = File.ReadAllLines(file);
        var nRows = lines.Length;
        var nCols = lines[0].Length;
        GridArray = new Tree[nRows, nCols];
        for (var row = 0; row < nRows; row++)
        {
            var tokens = lines[row].ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();
            for (var col = 0; col < nCols; col++)
                GridArray[row, col] = new Tree(row, col, tokens[col], IsEdge(row, col));
        }
    }
    public Tree[,] GridArray { get; }

    public int NumberRows => GridArray.GetLength(0);

    public int NumberCols => GridArray.GetLength(1);

    public int CountVisibleTrees()
    {
        var count = 0;
        for (var row = 0; row < GridArray.GetLength(0); row++)
            for (var col = 0; col < GridArray.GetLength(1); col++)
                count += GridArray[row, col].Visible ? 1 : 0;
        return count;
    }

    public bool IsEdge(int row, int col) => row == 0 || row == NumberRows - 1 || col == 0 || col == NumberCols - 1;
}