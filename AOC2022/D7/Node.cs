namespace AOC2022.D7;

internal abstract class Node
{
    protected Node(string name) => Name = name;
    public List<Node> Children { get; } = new();
    public Node? Parent { get; set; } = null; 
    public string Name { get; }
    public abstract int Size { get; }

    public override int GetHashCode() => Name.GetHashCode();
}