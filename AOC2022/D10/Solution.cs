namespace AOC2022.D10;

public record Instruction(string Type, int Value);

public class Processor
{
    private int X { get; set; } = 1;
    private int Cycles { get; set; }
    private int Signal => X * Cycles;
    public int ProcessInstruction(Instruction instruction)
    {
        var debug = 0;
        switch (instruction.Type)
        {
            case "addx":
                debug = AddX(instruction.Value);
                break;
            case "noop":
                Cycles += 1;
                if (Cycles % 40 == 20) debug = Signal;
                break;
        }
        return debug;
    }

    private int AddX(int value = 0)
    {
        var debugSignal = 0;
        Cycles += 1;
        if (Cycles % 40 == 20) debugSignal = Signal;
        Cycles += 1;
        if (Cycles % 40 == 20) debugSignal = Signal;
        X += value;
        return debugSignal;
    }
}

internal class Solution
{
    public static string Sample => "D10/sample.txt";
    public static string Input => "D10/input.txt";

    private readonly List<Instruction> _instructions = new();
    
    public Solution(string fileInput)
    {
        foreach (var instruction in File.ReadAllLines(fileInput))
        {
            var tokens = instruction.Split(' ');
            var type = tokens[0];
            var value = type == "noop" ? 0 : int.Parse(tokens[1]);
            _instructions.Add(new Instruction(type, value));
        }
    }

    public int SolvePart1()
    {
        Processor cpu = new();
        var signalStrengths = _instructions.Select(instruction => cpu.ProcessInstruction(instruction)).Where(debug => debug != 0).ToList();
        return signalStrengths.Sum();
    }

    private static char ComputePixel(int x, int cycle)
    {
        var list = Enumerable.Range(x-1, 3).ToList();
        for (var index = 0; index < list.Count; index++) list[index] %= 40;
        return list.Contains(cycle % 40) ? '#' : '.';
    }
    
    public int SolvePart2()
    {
        var cycle = 0;
        var x = 1;
        var output = new char[40];
        foreach (var instruction in _instructions)
        {
            switch (instruction.Type)
            {
                case "addx":
                    output[cycle % 40] = ComputePixel(x, cycle++);
                    if (cycle % 40 == 0) Console.WriteLine(output);
                    output[cycle % 40] = ComputePixel(x, cycle++);
                    if (cycle % 40 == 0) Console.WriteLine(output);
                    x += instruction.Value;
                    break;
                
                case "noop":
                    output[cycle % 40] = ComputePixel(x, cycle++);
                    if (cycle % 40 == 0) Console.WriteLine(output);
                    break;
            }
        }
        return 0;
    }
}