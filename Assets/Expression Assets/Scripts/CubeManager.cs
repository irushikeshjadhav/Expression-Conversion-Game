using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    private List<GameObject> cubes = new List<GameObject>(); // List to store cube GameObjects

    // Method to add a cube to the list
    public void AddCube(GameObject go)
    {
        cubes.Add(go); // Add the cube GameObject to the list
    }

    // Method to remove the last cube from the list
    public void RemoveCube()
    {
        // Check if the list is not empty
        if (cubes.Count > 0)
        {
            // Reactivate the last cube in the list
            cubes[cubes.Count - 1].SetActive(true);

            // Remove the last cube from the list
            cubes.RemoveAt(cubes.Count - 1);
        }
    }
}
