using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public CriminalManager criminalInfo;
    public string height;
    public string hairColor;
    public string eyeColor;

    public TMP_Text hairColorText;
    public TMP_Text eyeColorText;
    public TMP_Text heightText;

    public DragDropText dragText;
    public List<ClueData> requiredClues; // List of required clues to arrest the criminal
    public List<TMP_Text> requiredCluess; // strings requiredto arrest criminal


    // Start is called before the first frame update

    //We can either give the SO TMP attributes or access the clues from so as is
    void Start()
    {
        // Populate the UI text elements with the information from the ScriptableObject
        if (criminalInfo != null)
        {
            eyeColorText.text =  criminalInfo.eyeColor;
            hairColorText.text =  criminalInfo.hairColor;
            heightText.text = criminalInfo.height;

            /*
             * 
             
            eyeColorText.text = "Eye Color: " + criminalInfo.eyeColor;
            hairColorText.text = "Hair Color: " + criminalInfo.hairColor;
            heightText.text = "Height: " + criminalInfo.height;
             */
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop method called");
        Debug.Log("Something Was Dropped on me");
        Debug.Log("Item was dropped on the panel.");
        // Get the dropped item
        //meObject droppedItem = eventData.pointerDrag;

       
    }

  

    // Called when another Collider2D makes contact with this Collider2D
    

    // Update is called once per frame
    void Update()
    {
        height = criminalInfo.height;
        hairColor = criminalInfo.hairColor;
        eyeColor = criminalInfo.eyeColor;
    }
}
