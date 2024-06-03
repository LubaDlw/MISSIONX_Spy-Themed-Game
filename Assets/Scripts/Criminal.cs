using System.Collections.Generic;
using UnityEngine;

public class Criminal
{
    public string Name;
    public List<GameObject> RequiredClues;
    public bool IsCaptured;

    public Criminal(string name, List<GameObject> requiredClues)
    {
        Name = name;
        RequiredClues = requiredClues;
        IsCaptured = false;
    }
}
