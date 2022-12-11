namespace AOC2022.D3;

internal class Solution
{
    public static string Sample => "D3/sample.txt";
    public static string Input => "D3/input.txt";

    private readonly List<string> _ruckSacks = new();

    public Solution(string fileInput)
    {
        foreach (var rucksack in File.ReadLines(fileInput)) _ruckSacks.Add(rucksack);
    }

    private static int GetPriorityFromChar(char priority) => char.IsUpper(priority) ? priority - 'A' + 27 : priority - 'a' + 1;

    public int SolvePart1() =>
        (from rucksack in _ruckSacks
            let length = rucksack.Length
            let left = rucksack[..(length / 2)]
            let right = rucksack[(length / 2)..]
            let leftSet = new HashSet<char>(left.ToCharArray())
            let rightSet = new HashSet<char>(right.ToCharArray())
            select leftSet.Intersect(rightSet).First()
            into priority
            select GetPriorityFromChar(priority)).Sum();

    public int SolvePart2()
    {
        var split = _ruckSacks.Select((x, i) => _ruckSacks.Skip(i * 3).Take(3));
        return (from ruckSack in split.Take(split.Count() / 3)
            let set1 = new HashSet<char>(ruckSack.ElementAt(0))
            let set2 = new HashSet<char>(ruckSack.ElementAt(1))
            let set3 = new HashSet<char>(ruckSack.ElementAt(2))
            select set1.Intersect(set2).Intersect(set3).First()
            into priority
            select GetPriorityFromChar(priority)).Sum();
    }
}