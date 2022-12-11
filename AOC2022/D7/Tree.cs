namespace AOC2022.D7;

internal class File : Node
{
    public File(string name, int size) : base(name) => Size = size;
    public override int Size { get; }
    public override string ToString() => $"{Name} (file, size={Size})";
}

internal class Directory : Node
{
    public Directory(string name) : base(name) {}
    public void Add(Node node) => Children.Add(node);
    public override int Size => Children!.Sum(child => child.Size);
    public override string ToString() => $"{Name} (dir, size={Size})";
}

internal class FileTree
{
    public FileTree(Directory root)
    {
        Root = root;
        CurrentDirectory = root;
    }

    public readonly Directory Root;
    private Directory CurrentDirectory { get; set; }
    public List<Directory> Directories { get; } = new();
    
    public void Add(Node node)
    {
        if (Find(CurrentDirectory, node.Name) != null) return;
        CurrentDirectory.Add(node);
        node.Parent = CurrentDirectory;
        if (node is Directory directory) Directories.Add(directory);
    }

    private static Node? Find(Node start, string targetName)
    {
        foreach (var node in start.Children)
        {
            if (node.Name == targetName)
                return node;
            var result = Find(node, targetName);
            if (result != null)
                return result;
        }
        return null;
    }
    
    public void ChangeDirectory(string name)
    {
        if (name == "..")
        {
            CurrentDirectory = (Directory)CurrentDirectory.Parent!;
            return;
        }
        var dir = Find(CurrentDirectory, name);
        if (dir is File or null) return;
        CurrentDirectory = (Directory)dir;
    }

    public static void Print(Node start, string sep)
    {
        foreach (var child in start.Children)
        {
            Console.WriteLine($"{sep}- {child}");
            if (child is Directory)
                Print(child, sep+"\t");
        }
    }
}