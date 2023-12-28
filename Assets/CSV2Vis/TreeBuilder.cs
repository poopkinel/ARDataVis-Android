using System;
using UnityEngine;

public class TreeBuilder
{
    private TreeNode _root;

    public TreeBuilder()
    {
        _root = new TreeNode("Root"); // Assuming a single root for the tree
    }

    public TreeNode BuildTreeFromCSV(string fileName)
    {
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);

        if (csvFile == null)
        {
            Debug.LogError("CSV file not found in Resources folder.");
            return null;
        }

        string[] lines = csvFile.text.Split('\n');

        // Assuming the first line contains column headers
        string[] columnNames = lines[0].Split(',');

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');
            TreeNode currentNode = _root;

            for (int j = 0; j < values.Length; j++)
            {
                if (String.IsNullOrWhiteSpace(values[j]))
                {
                    continue;
                }
                // Create node name with column name and value
                string nodeName = columnNames[j] + ": " + values[j];
                TreeNode nextNode = currentNode.FindChild(nodeName);

                if (nextNode == null)
                {
                    nextNode = currentNode.AddChild(nodeName);
                }

                currentNode = nextNode;
            }
        }

        return _root;
    }
}
