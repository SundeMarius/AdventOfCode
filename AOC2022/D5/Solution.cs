namespace AOC2022.D5;

internal class Solution
{
    public static string Sample => "D5/sample.txt";
    public static string Input => "D5/input.txt";

    private readonly string _fileInput;
    private readonly List<string> _instructions = new();
    private List<Stack<char>> _stacks = new();

    private void CreateInitialConfigurationSample()
    {
        // 1
        _stacks.Add(new Stack<char>());
        _stacks[0].Push('Z'); _stacks[0].Push('N');
        // 2
        _stacks.Add(new Stack<char>());
        _stacks[1].Push('M'); _stacks[1].Push('C'); _stacks[1].Push('D');
        // 3
        _stacks.Add(new Stack<char>());
        _stacks[2].Push('P');
    }
    private void CreateInitialConfigurationInput()
    {
        // 1
        _stacks.Add(new Stack<char>());
        _stacks[0].Push('F'); _stacks[0].Push('C'); _stacks[0].Push('P');
        _stacks[0].Push('G'); _stacks[0].Push('Q'); _stacks[0].Push('R');
        // 2
        _stacks.Add(new Stack<char>());
        _stacks[1].Push('W'); _stacks[1].Push('T'); _stacks[1].Push('C');
        _stacks[1].Push('P');
        // 3
        _stacks.Add(new Stack<char>());
        _stacks[2].Push('B'); _stacks[2].Push('H'); _stacks[2].Push('P');
        _stacks[2].Push('M'); _stacks[2].Push('C');
        // 4
        _stacks.Add(new Stack<char>());
        _stacks[3].Push('L'); _stacks[3].Push('T'); _stacks[3].Push('Q');
        _stacks[3].Push('S'); _stacks[3].Push('M'); _stacks[3].Push('P');
        _stacks[3].Push('R');
        // 5
        _stacks.Add(new Stack<char>());
        _stacks[4].Push('P'); _stacks[4].Push('H'); _stacks[4].Push('J');
        _stacks[4].Push('Z'); _stacks[4].Push('V'); _stacks[4].Push('G');
        _stacks[4].Push('N');
        // 6
        _stacks.Add(new Stack<char>());
        _stacks[5].Push('D'); _stacks[5].Push('P'); _stacks[5].Push('J');
        // 7
        _stacks.Add(new Stack<char>());
        _stacks[6].Push('L'); _stacks[6].Push('G'); _stacks[6].Push('P');
        _stacks[6].Push('Z'); _stacks[6].Push('F'); _stacks[6].Push('J');
        _stacks[6].Push('T'); _stacks[6].Push('R');
        // 8
        _stacks.Add(new Stack<char>());
        _stacks[7].Push('N'); _stacks[7].Push('L'); _stacks[7].Push('H');
        _stacks[7].Push('C'); _stacks[7].Push('F'); _stacks[7].Push('P');
        _stacks[7].Push('T'); _stacks[7].Push('J');
        // 9
        _stacks.Add(new Stack<char>());
        _stacks[8].Push('G'); _stacks[8].Push('V'); _stacks[8].Push('Z');
        _stacks[8].Push('Q'); _stacks[8].Push('H'); _stacks[8].Push('T');
        _stacks[8].Push('C'); _stacks[8].Push('W');
    }

    private void CreateInitialConfiguration()
    {
        _stacks = new List<Stack<char>>();
        if (_fileInput == Sample)
        {
            CreateInitialConfigurationSample();
        }
        else
        {
            CreateInitialConfigurationInput();
        }
        
    }
    
    public Solution(string fileInput)
    {
        _fileInput = fileInput;
        CreateInitialConfiguration();
        foreach (var instruction in File.ReadLines(fileInput))
        {
            _instructions.Add(instruction);
        }
    }

    public string SolvePart1()
    {
        CreateInitialConfiguration();
        foreach (var instruction in _instructions)
        {
            var tokens = instruction.Split(' ');
            var amount = int.Parse(tokens[1]);
            var from = int.Parse(tokens[3]);
            var to = int.Parse(tokens[5]);
            for (var i = 0; i < amount; i++)
            {
               _stacks[to-1].Push(_stacks[from-1].Pop()); 
            }
        }
        return _stacks.Aggregate(string.Empty, (current, stack) => current + stack.Pop());
    }
    
    public string SolvePart2()
    {
        CreateInitialConfiguration();
        foreach (var instruction in _instructions)
        {
            var tokens = instruction.Split(' ');
            var amount = int.Parse(tokens[1]);
            var from = int.Parse(tokens[3]);
            var to = int.Parse(tokens[5]);
            var movement = string.Empty;
            for (var i = 0; i < amount; i++)
            {
                movement += _stacks[from - 1].Pop();
            }
            for (var i = amount-1; i >= 0; i--)
            {
                _stacks[to-1].Push(movement[i]); 
            }
        }
        return _stacks.Aggregate(string.Empty, (current, stack) => current + stack.Pop());
    }
}
