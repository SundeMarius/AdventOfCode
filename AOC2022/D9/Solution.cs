namespace AOC2022.D9;

internal record struct Vec2(int X = 0, int Y = 0)
{
    public int X = X;
    public int Y = Y;
    public static Vec2 operator+(Vec2 left, Vec2 right) => new (left.X + right.X, left.Y + right.Y);
    public static Vec2 operator-(Vec2 left, Vec2 right) => new (left.X - right.X, left.Y - right.Y);
}

internal record struct Movement(Vec2 Direction, int Count)
{
    public Vec2 Direction = Direction;
    public int Count = Count;
    public static Movement FromInput(string dir, int count)
    {
        Vec2 direction = new();
        switch (dir)
        {
            case "L":
                direction = new Vec2(-1, 0);
                break;
            case "R":
                direction = new Vec2(1, 0);
                break;
            case "U":
                direction = new Vec2(0, 1);
                break;
            case "D":
                direction = new Vec2(0, -1);
                break;
        }
        return new Movement(direction, count);
    }
}

internal record Point(Vec2 Position = new())
{
    public Vec2 Position = Position;
    private HashSet<Vec2> PositionHistory = new();
    public Movement NextMove = new(new Vec2(), 0);
    public bool IsAdjacent(Point other) => Math.Abs(Position.X - other.Position.X) <= 1 && Math.Abs(Position.Y - other.Position.Y) <= 1;
    public void Move()
    {
        PositionHistory.Add(Position);
        Position += NextMove.Direction;
    }
    public int NumberOfSteps => PositionHistory.Count+1;
}


internal class Solution
{
    public static string Sample => "D9/sample.txt";
    public static string Input => "D9/input.txt";

    private readonly List<Movement> _moves = new();

    public Solution(string fileInput)
    {
        foreach (var line in File.ReadAllLines(fileInput))
        {
            var tokens = line.Split(' ');
            _moves.Add(Movement.FromInput(tokens[0],int.Parse(tokens[1])));
        }
    }

    public int SolvePart1()
    {
        var head = new Point();
        var tail = new Point();
        foreach (var move in _moves)
        {
            head.NextMove = move;
            tail.NextMove = move;
            for (int i = 0; i < move.Count; i++)
            {
                head.Move();
                if (tail.IsAdjacent(head))
                    continue;
                tail.NextMove.Direction = head.Position - tail.Position;
                if (Math.Abs(tail.NextMove.Direction.X) > 1) tail.NextMove.Direction.X /= Math.Abs(tail.NextMove.Direction.X);
                if (Math.Abs(tail.NextMove.Direction.Y) > 1) tail.NextMove.Direction.Y /= Math.Abs(tail.NextMove.Direction.Y);
                tail.Move();
            }
        }
        return tail.NumberOfSteps;
    }

    public int SolvePart2()
    {
        var points = new List<Point>(10);
        for (var index = 0; index < 10; index++) points.Add( new Point());
        foreach (var move in _moves)
        {
            foreach (var point in points) point.NextMove = move;
            var first = points[0];
            for (int i = 0; i < move.Count; i++)
            {
                first.Move();
                for (int p = 1; p < 10; p++)
                {
                    var point = points[p];
                    if (point.IsAdjacent(points[p - 1]))
                        continue;
                    point.NextMove.Direction = points[p-1].Position - point.Position;
                    if (Math.Abs(point.NextMove.Direction.X) > 1) point.NextMove.Direction.X /= Math.Abs(point.NextMove.Direction.X);
                    if (Math.Abs(point.NextMove.Direction.Y) > 1) point.NextMove.Direction.Y /= Math.Abs(point.NextMove.Direction.Y);
                    point.Move();
                }
            }
        }
        return points.Last().NumberOfSteps;
    }
}