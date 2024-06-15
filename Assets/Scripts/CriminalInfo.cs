using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriminalInfo : MonoBehaviour
{
    public string CriminalName;
    public List<GameObject> blackSlots;
    public int TotalClues;
    public int cluesFound;
    public int points;
    public GameObject Silhouette;
    public GameObject ColorImage;

    public CriminalInfo(string name, List<GameObject> blackSlots)
    {
        this.name = name;
        this.blackSlots = blackSlots;
    }

    void Start()
    {
        // At the beginning of the game, set the silhouette to active and color image to inactive
        Silhouette.SetActive(true);
        ColorImage.SetActive(false);
    }

    void Update()
    {
        // Check if all clues have been found
        if (cluesFound >= TotalClues)
        {
            // Switch the active states of the silhouette and color image
            Silhouette.SetActive(false);
            ColorImage.SetActive(true);
        }
    }
}
