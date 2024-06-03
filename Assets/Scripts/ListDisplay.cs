using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListDisplay : MonoBehaviour
{
    // Reference to the TEXT
    public TMP_Text textElement;

    public TMP_Text[] textElements;

    //panel 
    public GameObject PlayerDeckPanel;

    // List of players clues that player has
    public List<string> playerDeck = new List<string>()
    { };

    public void ToggleListDisplayPanel()
    {
        // TO toGGLE THE PANELS
        PlayerDeckPanel.SetActive(!PlayerDeckPanel.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        // Update the text content whenever the list changes
        UpdateTextContent();
    }

    // Method to update the text content
    void UpdateTextContent()
    {
        // Clear the existing text IN INSOPECTOR
        foreach (TMP_Text textElement in textElements)
        {
            textElement.text = "";
        }
        // Iterate through the player deck and add each ITEM name to the text
        for (int i = 0; i < playerDeck.Count && i < textElements.Length; i++)
        {
            // Update the text element at index i with the playerDeck item at index i
            textElements[i].text = playerDeck[i];
        }
    }
}
