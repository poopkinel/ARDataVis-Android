using System.Collections.Generic;
using System.Linq;

public class TreeNode
{
    public string Name;
    public TreeNode Parent;
    public List<TreeNode> Children;

    public TreeNode(string name)
    {
        this.Name = name;
        this.Children = new List<TreeNode>();
    }

    public TreeNode FindChild(string name)
    {
        return Children.FirstOrDefault(child => child.Name == name);
    }

    public TreeNode AddChild(string name)
    {
        var childNode = new TreeNode(name) { Parent = this };
        Children.Add(childNode);
        return childNode;
    }

    public List<string> GetPath()
    {
        var path = new List<string>();
        var currentNode = this;
        while (currentNode != null)
        {
            path.Insert(0, currentNode.Name);
            currentNode = currentNode.Parent;
        }
        return path;
    }
}
