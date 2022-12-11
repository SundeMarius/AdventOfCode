namespace AOC2022.D11;

internal record Dispatch(int MonkeyId, long WorryLevel);

internal record Monkey(IEnumerable<long> items, Func<long, long> Operation, long divisor, int monkeyTrue, int monkeyFalse)
{
    private long _worryLevel;
    private readonly int _monkeyTrue = monkeyTrue;
    private readonly int _monkeyFalse = monkeyFalse;

    public readonly Queue<long> Items = new (items);
    public long Divisor { get; } = divisor;
    public long ItemsInspected { get; private set; }
    public IEnumerable<Dispatch> DoTurn(bool reduceWorry = false)
    {
        List<Dispatch> dispatches = new();
        while (Items.Count > 0)
        {
            ItemsInspected++;
            _worryLevel = Operation(Items.Dequeue());
            if (reduceWorry) _worryLevel /= 3;
            dispatches.Add(new Dispatch(_worryLevel % Divisor == 0 ? _monkeyTrue : _monkeyFalse, _worryLevel));
        }
        return dispatches;
    }
}

internal class Solution
{
    public static string Sample => "D11/sample.txt";
    public static string Input => "D11/input.txt";

    private readonly string _fileName;

    private readonly List<Monkey> _monkeys = new();

    private void Initialize(string filename)
    {
        _monkeys.Clear();
        if (filename == Sample)
        {
            _monkeys.Add(new Monkey(new List<long> { 79, 98 }, x => x * 19, 23, 2, 3));
            _monkeys.Add(new Monkey(new List<long> { 54, 65, 75, 74 }, x => x + 6, 19, 2, 0));
            _monkeys.Add(new Monkey(new List<long> { 79, 60, 97 }, x => x * x, 13, 1, 3));
            _monkeys.Add(new Monkey(new List<long> { 74 }, x => x + 3, 17, 0, 1));
        }
        else
        {
            _monkeys.Add(new Monkey(new List<long> { 61 }, x => x * 11, 5, 7, 4));
            _monkeys.Add(new Monkey(new List<long> { 76, 92, 53, 93, 79, 86, 81 }, x => x + 4, 2, 2, 6));
            _monkeys.Add(new Monkey(new List<long> { 91, 99 }, x => x * 19, 13, 5, 0));
            _monkeys.Add(new Monkey(new List<long> { 58, 67, 66 }, x => x * x, 7, 6, 1));
            _monkeys.Add(new Monkey(new List<long> { 94, 54, 62, 73 }, x => x + 1, 19, 3, 7));
            _monkeys.Add(new Monkey(new List<long> { 59, 95, 51, 58, 58 }, x => x + 3, 11, 0, 4));
            _monkeys.Add(new Monkey(new List<long> { 87, 69, 92, 56, 91, 93, 88, 73 }, x => x + 8, 3, 5, 2));
            _monkeys.Add(new Monkey(new List<long> { 71, 57, 86, 67, 96, 95 }, x => x + 7, 17, 3, 1));
        }
    }

    public Solution(string fileInput)
    {
        _fileName = fileInput;
    }

    private long GetMonkeyBusiness(int rounds, bool reduceWorry)
    {
        Initialize(_fileName);
        var lcm = _monkeys.Select(x => x.Divisor).Aggregate(1L, (x, y) => x * y);
        for (var i = 0; i < rounds; ++i)
        {
            foreach (var dispatch in _monkeys.Select(monkey => monkey.DoTurn(reduceWorry)).SelectMany(recipients => recipients))
            {
                _monkeys[dispatch.MonkeyId].Items.Enqueue(dispatch.WorryLevel % lcm);
            }
        }
        var topTwo = _monkeys.OrderBy(x => x.ItemsInspected).Reverse().ToList();
        return topTwo[0].ItemsInspected * topTwo[1].ItemsInspected;
    }

    public long SolvePart1() => GetMonkeyBusiness(20, true);

    public long SolvePart2() => GetMonkeyBusiness(10_000, false);
}