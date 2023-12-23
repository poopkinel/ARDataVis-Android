using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPositionManager : MonoBehaviour
{
    public GameObject prefab; // Assign a prefab in the inspector

    // Adjust these values as needed
    public float horizontalSpacingMultiplier = 1.5f;
    public float verticalSpacingMultiplier = 2.0f;
    public float depthSpacingMultiplier = 1.5f;

    void Start()
    {
        // Example of generating objects - replace this with your own hierarchy logic
        int totalElements = 10; // Example total elements
        for (int i = 0; i < totalElements; i++)
        {
            Vector3 position = CalculatePosition(i, totalElements);
            Instantiate(prefab, position, Quaternion.identity);
        }
    }

    Vector3 CalculatePosition(int index, int totalElements)
    {
        // Get the size of the prefab
        Vector3 prefabSize = prefab.GetComponentInChildren<Renderer>().bounds.size;

        // Calculate spacing based on prefab size and multipliers
        float x = index * (prefabSize.x * horizontalSpacingMultiplier);
        float y = prefabSize.y * verticalSpacingMultiplier; // You can adjust 'y' based on hierarchy level if needed
        float z = totalElements * (prefabSize.z * depthSpacingMultiplier);

        return new Vector3(x, y, z);
    }
}

