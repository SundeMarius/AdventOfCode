namespace AOC2022.D1;

internal class Solution
{
    public static string Sample => "D1/sample.txt";
    public static string Input => "D1/input.txt";

    private readonly List<int> _totalCaloriesForEachElf = new();
    
    public Solution(string fileInput)
    {
        var totalCalories = 0;
        foreach (var calories in File.ReadLines(fileInput))
        {
            totalCalories += int.Parse("0" + calories);
            if (calories != string.Empty) continue;
            _totalCaloriesForEachElf.Add(totalCalories);
            totalCalories = 0;
        }
        _totalCaloriesForEachElf.Sort();
        _totalCaloriesForEachElf.Reverse();
    }

    public int SolvePart1() => _totalCaloriesForEachElf.First();

    public int SolvePart2() => _totalCaloriesForEachElf.Take(3).Sum();
}