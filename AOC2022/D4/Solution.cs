namespace AOC2022.D4;

internal class Solution
{
    public static string Sample => "D4/sample.txt";
    public static string Input => "D4/input.txt";

    private readonly List<string> _assignments = new();

    public Solution(string fileInput) => _assignments.AddRange(from assignment in File.ReadLines(fileInput)
                                                               select assignment);

    public int SolvePart1()
    {
        return (from assignment in _assignments
                let leftId1 = int.Parse(assignment.Split(',')[0].Split('-')[0])
                let leftId2 = int.Parse(assignment.Split(',')[0].Split('-')[1])
                let rightId1 = int.Parse(assignment.Split(',')[1].Split('-')[0])
                let rightId2 = int.Parse(assignment.Split(',')[1].Split('-')[1])
                select (leftId1 <= rightId1 && leftId2 >= rightId2) || (rightId1 <= leftId1 && rightId2 >= leftId2)
                    ? 1
                    : 0)
            .Sum();
    }
    
    public int SolvePart2()
    {
        return (from assignment in _assignments
            let leftId1 = int.Parse(assignment.Split(',')[0].Split('-')[0])
            let leftId2 = int.Parse(assignment.Split(',')[0].Split('-')[1])
            let rightId1 = int.Parse(assignment.Split(',')[1].Split('-')[0])
            let rightId2 = int.Parse(assignment.Split(',')[1].Split('-')[1])
            select leftId2 < rightId1 || rightId2 < leftId1 ? 0 : 1).Sum();
    }
}
