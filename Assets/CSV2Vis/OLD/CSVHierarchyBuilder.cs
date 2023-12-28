using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace Attempt1
{
    public class CSVHierarchyBuilder : MonoBehaviour
    {
        public GameObject nodePrefab;
        public Material lineMaterial;
        //private Dictionary<string, Node> allNodes = new Dictionary<string, Node>();
        private Dictionary<string, Node> roots = new();

        // Node definition
        private class Node
        {
            public GameObject gameObject;
            public Dictionary<string, Node> children = new();
            public int layer;
        }

        public void BuildHierarchyFromCSV(string fileName)
        {
            TextAsset csvFile = Resources.Load<TextAsset>(fileName);

            if (csvFile == null)
            {
                Debug.LogError("CSV file not found in Resources folder.");
                return;
            }

            string[] lines = csvFile.text.Split('\n');

            foreach (string line in lines)
            {
                int layer = 0;
                string[] values = line.Split(',');
                Node parentNode = null;

                foreach (string value in values)
                {
                    parentNode = FindPlaceInTree(parentNode, value);
                }
            }

            // Visualize the hierarchy
            foreach (var rootNode in roots.Values)
            {
                if (rootNode.gameObject.transform.parent == null) // Top level nodes
                {
                    VisualizeHierarchy(rootNode);
                }
            }
        }

        private Node FindPlaceInTree(Node parentNode, string value)
        {
            Node currentNode;
            if (!parentNode.children.TryGetValue(value, out currentNode))
            {
                // Create new node if it doesn't exist
                //GameObject nodeObj = CreateNode(new Vector3(0.7f, layer * 1f, 0f), value).gameObject;
                //nodeObj.name = value;
                currentNode = new Node();//nodeObj);
                parentNode.children[value] = currentNode;

                if (parentNode != null)
                {
                    parentNode.children.Add(value, currentNode); // Add as child to parent
                }
            }

            parentNode = currentNode; // Set current node as the next parent
            return parentNode;
        }

        private Node CreateNode(Vector3 position, string name)
        {
            GameObject nodeObj = Instantiate(nodePrefab, position, Quaternion.identity);
            nodeObj.name = name;
            return new Node();//nodeObj);
        }

        // Method to visualize the hierarchy
        private void VisualizeHierarchy(Node node)
        {
            foreach (Node child in node.children.Values)
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
}