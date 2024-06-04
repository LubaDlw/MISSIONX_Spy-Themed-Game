using System.Collections.Generic;
using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    public GameObject[] prefabs;  // Array to hold the prefabs
    public Transform parentPanel; // The parent panel where the prefabs will be instantiated

    void Start()
    {
        // Ensure there are exactly 5 prefabs
        if (prefabs.Length != 5)
        {
            Debug.LogError("There should be exactly 5 prefabs assigned to the array.");
            return;
        }

        // Create a list to hold indices of the prefabs
        List<int> indices = new List<int> { 0, 1, 2, 3, 4 };

        // Shuffle the list
        for (int i = 0; i < indices.Count; i++)
        {
            int temp = indices[i];
            int randomIndex = Random.Range(i, indices.Count);
            indices[i] = indices[randomIndex];
            indices[randomIndex] = temp;
        }

        // Instantiate the first 3 prefabs from the shuffled list
        for (int i = 0; i < 3; i++)
        {
            Instantiate(prefabs[indices[i]], parentPanel);
        }
    }
}
