using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Criminal Data", menuName = "Game/Criminal Data")]
public class CriminalData : ScriptableObject
{
    public string criminalName;
    public List<ClueData> requiredClues; // List of required clues to arrest the criminal
}
