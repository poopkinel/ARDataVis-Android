using UnityEngine;
using System.Collections.Generic;

public class HierarchyVisualizer : MonoBehaviour
{
    public GameObject nodePrefab; // Prefab for nodes
    public Material lineMaterial; // Material for the connection lines

    // Example class to represent a node
    private class Node
    {
        public GameObject gameObject;
        public List<Node> children = new List<Node>();

        public Node(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }

    void Start()
    {
        // Example: Create a root node and its children
        Node rootNode = CreateNode(Vector3.zero, "Root");
        rootNode.children.Add(CreateNode(transform.position + new Vector3(0.7f, -1, 0), "Child 1"));
        rootNode.children.Add(CreateNode(new Vector3(-0.7f, -1, 0), "Child 2"));

        // Visualization
        VisualizeHierarchy(rootNode);
    }

    // Method to create a node
    private Node CreateNode(Vector3 position, string name)
    {
        GameObject nodeObj = Instantiate(nodePrefab, position, Quaternion.identity);
        nodeObj.name = name;
        return new Node(nodeObj);
    }

    // Method to visualize the hierarchy
    private void VisualizeHierarchy(Node node)
    {
        foreach (Node child in node.children)
        {
            DrawLine(node.gameObject, child.gameObject);
            VisualizeHierarchy(child); // Recursive call for deeper layers
        }
    }

    // Method to draw a line between two GameObjects
    private void DrawLine(GameObject start, GameObject end)
    {
        LineRenderer lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.SetPosition(0, start.transform.position);
        lineRenderer.SetPosition(1, end.transform.position);
    }
}
