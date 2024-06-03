using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Clue Data", menuName = "Game/Clue Data")]
public class ClueData : ScriptableObject
{
    public string clueName;
    public TMP_Text clueTxt;
}
