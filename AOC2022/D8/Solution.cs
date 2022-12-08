namespace AOC2022.D8;

internal class Solution
{
    public static string Sample => "D8/sample.txt";
    public static string Input => "D8/input.txt";

    private readonly Grid _grid;

    public Solution(string fileInput)
    {
        _grid = new Grid(fileInput);
    }

    public int SolvePart1()
    {
        for (var treeRow = 1; treeRow < _grid.GridArray.GetLength(0)-1; treeRow++)
            for (var treeCol = 1; treeCol < _grid.GridArray.GetLength(1)-1; treeCol++)
            {
                var targetTree = _grid.GridArray[treeRow, treeCol];
                var visibleFromDir = new bool[4]  { true, true, true, true };
                // From left
                for (int col = 0; col < targetTree.Col; col++)
                {
                    if (_grid.GridArray[treeRow, col].Height >= targetTree.Height) visibleFromDir[0] = false;
                }
                // From top
                for (int row = 0; row < targetTree.Row; row++)
                {
                    if (_grid.GridArray[row, treeCol].Height >= targetTree.Height) visibleFromDir[1] = false;
                }
                // From right
                for (int col = _grid.NumberCols-1; col > targetTree.Col; col--)
                {
                    if (_grid.GridArray[treeRow, col].Height >= targetTree.Height) visibleFromDir[2] = false;
                }
                // From bottom
                for (int row = _grid.NumberRows-1; row > targetTree.Row; row--)
                {
                    if (_grid.GridArray[row, treeCol].Height >= targetTree.Height) visibleFromDir[3] = false;
                }
                targetTree.Visible = visibleFromDir.Any(l => l);
            }
        return _grid.CountVisibleTrees();
    }

    public int SolvePart2()
    {
        var maxViewScore = 1;
        for (var treeRow = 1; treeRow < _grid.GridArray.GetLength(0) - 1; treeRow++)
        {
            for (var treeCol = 1; treeCol < _grid.GridArray.GetLength(1) - 1; treeCol++)
            {
                var targetTree = _grid.GridArray[treeRow, treeCol];
                var viewingDistances = new[] { 1, 1, 1, 1 };
                // To left
                for (int col = targetTree.Col-1; col >= 0; col--)
                {
                    if (_grid.GridArray[treeRow, col].Height >= targetTree.Height || _grid.IsEdge(treeRow, col))
                    {
                        viewingDistances[0] = targetTree.Col - col;
                        break;
                    }
                }
                // To top
                for (int row = targetTree.Row-1; row >= 0; row--)
                {
                    if (_grid.GridArray[row, treeCol].Height >= targetTree.Height || _grid.IsEdge(row, treeCol))
                    {
                        viewingDistances[1] = targetTree.Row - row;
                        break;
                    }
                }
                // To right
                for (int col = targetTree.Col+1; col < _grid.NumberCols; col++)
                {
                    if (_grid.GridArray[treeRow, col].Height >= targetTree.Height || _grid.IsEdge(treeRow, col))
                    {
                        viewingDistances[2] = col - targetTree.Col;
                        break;
                    }
                }
                // To bottom
                for (int row = targetTree.Row+1; row < _grid.NumberRows; row++)
                {
                    if (_grid.GridArray[row, treeCol].Height >= targetTree.Height || _grid.IsEdge(row, treeCol))
                    {
                        viewingDistances[3] = row - targetTree.Row;
                        break;
                    }
                }
                var viewScore = viewingDistances.Aggregate(1, (a, b) => a * b);
                if (viewScore > maxViewScore) maxViewScore = viewScore;
            }
        }
        return maxViewScore;
    }
}