namespace AOC2022.D1;

internal static class Solution
{
    private static string Sample => "D1/sample.txt";
    private static string Input => "D1/input.txt";

    private static readonly List<int> TotalCaloriesForEachElf = new();
    
    static Solution()
    {
        var totalCalories = 0;
        foreach (var calories in File.ReadLines(Input))
        {
            totalCalories += int.Parse("0" + calories);
            if (calories != string.Empty) continue;
            TotalCaloriesForEachElf.Add(totalCalories);
            totalCalories = 0;
        }
        TotalCaloriesForEachElf.Sort();
        TotalCaloriesForEachElf.Reverse();
    }

    public static int SolvePart1()
    {
        return TotalCaloriesForEachElf.First();
    }
    
    public static int SolvePart2()
    {
        return TotalCaloriesForEachElf.Take(3).Sum();
    }
}