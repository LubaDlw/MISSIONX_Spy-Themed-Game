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

    public CriminalInfo(string name, List<GameObject> blackSlots)
    {
        this.name = name;
        this.blackSlots = blackSlots;
    }
}
