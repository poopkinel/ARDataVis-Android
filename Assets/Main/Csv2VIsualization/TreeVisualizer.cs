using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TreeVisualizer : MonoBehaviour
{
    public GameObject nodePrefab;
    public Material lineMaterial;
    private TreeNode rootNode;

    public float horizontalSpacing = 0.25f;
    public float verticalSpacing = 0.25f;

    public void BuildFromCSV(string csvFileName)
    {
        Debug.Log($"BuildFromCSV(string csvFileName): {csvFileName}");
        rootNode = new TreeBuilder().BuildTreeFromCSV(csvFileName);
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
        float depthSpacing = 2f; // Increases the spacing with depth
        float siblingSpacing = 0.5f; // Spacing between nodes with the same parent

        float x = parentPosition.x + index * depthSpacing; // Horizontal positioing
        float y = parentPosition.y - (depth * siblingSpacing); // Vertical positioning
        float z = parentPosition.z + depth * index * totalChildren; // Depth positioning

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
