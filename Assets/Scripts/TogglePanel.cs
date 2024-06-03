using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TogglePanel : MonoBehaviour
{
    // Reference to the TMPro Text element
   // public TMP_Text textElement;

    //panel toggle
    public GameObject PannleToToggle;

   

    public void ToggleListDisplayPanel()
    {
        // Check if the panel is active or inactive and toggle its state accordingly
        PannleToToggle.SetActive(!PannleToToggle.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method to update the text content
   
}