namespace AOC2022.D3;

internal class Solution
{
    public static string Sample => "D3/sample.txt";
    public static string Input => "D3/input.txt";

    private readonly List<string> _ruckSacks = new();

    public Solution(string fileInput)
    {
        foreach (var rucksack in File.ReadLines(fileInput))
        {
            _ruckSacks.Add(rucksack);
        }
    }

    private static int GetPriorityFromChar(char priority)
    {
        return char.IsUpper(priority) ? priority - 'A' + 27 : priority - 'a' + 1;
    }

    public int SolvePart1()
    {
        var total = 0;
        foreach (var rucksack in _ruckSacks)
        {
            var length = rucksack.Length;
            var left = rucksack[..(length / 2)];
            var right = rucksack[(length / 2)..];
            HashSet<char> leftSet = new (left.ToCharArray());
            HashSet<char> rightSet = new (right.ToCharArray());
            var priority = leftSet.Intersect(rightSet).First();
            total += GetPriorityFromChar(priority);
        }
        return total;
    }
    
    public int SolvePart2()
    {
        var total = 0;
        var split = _ruckSacks.Select((x, i) => _ruckSacks.Skip(i * 3).Take(3));
        foreach (var ruckSack in split.Take(split.Count()/3))
        {
            HashSet<char> set1 = new(ruckSack.ElementAt(0));
            HashSet<char> set2 = new(ruckSack.ElementAt(1));
            HashSet<char> set3 = new(ruckSack.ElementAt(2));
            var priority = set1.Intersect(set2).Intersect(set3).First();
            total += GetPriorityFromChar(priority);
        }
        return total;
    }
}