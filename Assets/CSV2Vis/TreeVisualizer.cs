using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TreeVisualizer : MonoBehaviour
{
    public GameObject nodePrefab; // Assign a prefab for the nodes
    public Material lineMaterial; // Material for the lines connecting nodes

    private TreeNode rootNode; // The root of your tree

    // Adjust these values as needed
    public float horizontalSpacing = 0.25f;
    public float verticalSpacing = 0.25f;

    void Start()
    {
        // Assume rootNode is already populated
        rootNode = new TreeBuilder().BuildTreeFromCSV("ExampleData");
        Debug.Log($"rootNode: {rootNode}");
        VisualizeTree(rootNode, Vector3.zero, 1);
    }

    void VisualizeTree(TreeNode node, Vector3 position, int depth)
    {
        bool isRoot = node.Name == "Root"; // Skip root node

        // Create a GameObject for this node
        GameObject nodeObj = Instantiate(nodePrefab, position, Quaternion.identity);
        nodeObj.name = node.Name;
        nodeObj.GetComponentInChildren<TMP_Text>().text = node.Name;

        if (isRoot)
        {
            nodeObj.SetActive(false);
        }

        // Optionally set some data or label on the nodeObj here

        // Visualize children
        for (int i = 0; i < node.Children.Count; i++)
        {
            Vector3 childPosition = CalculateChildPosition(position, depth, i, node.Children.Count, node);
            VisualizeTree(node.Children[i], childPosition, depth + 1);

            if (!isRoot)
            {
                DrawLine(nodeObj, childPosition); // Draw line to child
            }
        }
    }

    Vector3 CalculateChildPosition(Vector3 parentPosition, int depth, int index, int totalChildren, TreeNode parentNode)
    {
        // Adjust these multipliers to scale the spacing dynamically
        float depthSpacing = 0.75f; // Increases the spacing with depth
        float siblingSpacing = 0.5f; // Spacing between nodes with the same parent
        float parentSpacing = depth * 0.5f; // Spacing based on the parent's position

        // Calculate angular spread based on depth and total children
        float angleDelta = 360.0f / Mathf.Max(totalChildren, parentNode.Parent != null ? parentNode.Parent.Children.Count : 1);
        float angle = angleDelta * index * Mathf.Deg2Rad; // Convert angle to radians

        // Position calculation using spherical coordinates
        float radius = depthSpacing * depth + parentSpacing;
        float x = parentPosition.x + index * depthSpacing; // Horizontal positioing
        float y = parentPosition.y - (depth * siblingSpacing); // Vertical positioning
        float z = parentPosition.z + radius * Mathf.Cos(angle);

        return new Vector3(x, y, z);
    }



    void DrawLine(GameObject startObj, Vector3 endPosition)
    {
        LineRenderer lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.SetPosition(0, startObj.transform.position);
        lineRenderer.SetPosition(1, endPosition);
    }
}
