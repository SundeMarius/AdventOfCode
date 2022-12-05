namespace AOC2022.D2;

internal class Solution
{
    public static string Sample => "D2/sample.txt";
    public static string Input => "D2/input.txt";

    private record Outcome(char Opponent, char Player);
    private readonly List<Outcome> _outcomes = new();

    public Solution(string fileInput)
    {
        foreach (var outcome in File.ReadLines(fileInput))
        {
            var chars = outcome.ToCharArray();
            _outcomes.Add(new Outcome(chars.First(), chars.Last()));
        }
    }

    private static int ShapeScore(char player) =>
        player switch
        {
            'X' => 1,
            'Y' => 2,
            'Z' => 3,
            _ => 0
        };

    private static int OutcomeScore(Outcome outcome) =>
        outcome switch
        {
            { Player: 'X', Opponent: 'C' } => 6,
            { Player: 'Y', Opponent: 'A' } => 6,
            { Player: 'Z', Opponent: 'B' } => 6,
            { Player: 'X', Opponent: 'A' } => 3,
            { Player: 'Y', Opponent: 'B' } => 3,
            { Player: 'Z', Opponent: 'C' } => 3,
            _ => 0
        };
    private static int TotalScore(Outcome outcome) => OutcomeScore(outcome) + ShapeScore(outcome.Player);

    public int SolvePart1()
    {
        return _outcomes.Sum(TotalScore);
    }

    public int SolvePart2()
    {
        var total = 0;
        foreach (var (opponent, player) in _outcomes)
        {
            var draw = player switch
            {
                'X' => opponent switch
                {
                    'A' => 'Z',
                    'B' => 'X',
                    'C' => 'Y',
                },
                'Y' => opponent switch
                {
                    'A' => 'X',
                    'B' => 'Y',
                    'C' => 'Z'
                },
                'Z' => opponent switch
                {
                    'C' => 'X',
                    'A' => 'Y',
                    'B' => 'Z'
                },
                _ => '0'
            };
            total += TotalScore(new Outcome(opponent, draw));
        }
        return total;
    }
}