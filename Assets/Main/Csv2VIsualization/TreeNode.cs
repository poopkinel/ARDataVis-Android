using System.Collections.Generic;
using System.Linq;

public class TreeNode
{
    private string _name;
    private TreeNode _parent;
    private List<TreeNode> _children;

    public TreeNode(string name, TreeNode parent)
    {
        _name = name;
        _parent = parent;
        _children = new List<TreeNode>();
    }

    public TreeNode FindChild(string name)
    {
        return _children.FirstOrDefault(child => child._name == name);
    }

    public TreeNode AddChild(string name)
    {
        var childNode = new TreeNode(name, this);
        _children.Add(childNode);
        return childNode;
    }

    public List<string> GetPath()
    {
        var path = new List<string>();
        var currentNode = this;
        while (currentNode != null)
        {
            path.Insert(0, currentNode._name);
            currentNode = currentNode._parent;
        }
        return path;
    }

    public string Name => _name;

    public List<TreeNode> Children => _children;
}
