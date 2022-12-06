namespace AOC2022.D6;

internal class Solution
{
    public static string Sample => "D6/sample.txt";
    public static string Input => "D6/input.txt";

    private readonly char[] _chars;

    public Solution(string fileInput)
    {
        _chars = File.ReadAllText(fileInput).ToCharArray();
    }

    private int GetStartOfPacketCount(int capacity)
    {
        var index = 0;
        HashSet<char> window = new(capacity);
        while (window.Count != capacity)
        {
            window.Clear();
            for (var i = 0; i < capacity; i++) window.Add(_chars[index+i]);
            index++;
        }
        return index+capacity-1;
    }

    public int SolvePart1()
    {
        return GetStartOfPacketCount(4);
    }

    public int SolvePart2()
    {
        return GetStartOfPacketCount(14);
    }
}