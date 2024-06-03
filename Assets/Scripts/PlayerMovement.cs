using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public GameObject losePanel;// NPC INT
    public GameObject criminalPanel;
    public TMP_Text button1Text;
    public TMP_Text button2Text;
    public TMP_Text button3Text;

    public GameObject CriminalCards;// Crminal Scroll 
    public GameObject EvidencePanel; // player deck or clues player has
   // public TMP_Text Option1;
   // public TMP_Text Option2;
   // public TMP_Text Option3;

    public ListDisplay listDisplay;
    // need to figure out how to maintain SO

    // List of Clue ScriptableObjects for buttons
    public List<ClueData> clueOptions;


    // This is like all the cards

    // References to the buttons
    public Button button1;
    public Button button2;
    public Button button3;

    // Count of buttons clicked by the player
    private int buttonsClicked = 0;

    void Start()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;
        SetButtonText();
    }

    // Handle button click event
    void OnButtonClick(TMP_Text buttonText)
    {
        if (buttonText != null)
        {
            string optionText = buttonText.text;
            listDisplay.playerDeck.Add(optionText);
            Debug.Log("Added " + optionText + " to playerDeck");
        }
    }

    void Update()
    {
        // Your update code here
    }

    // Method to set text for buttons
    void SetButtonText()
    {
        // Shuffle the list of clues
        ShuffleList(clueOptions);

        // Assign clues to button text components
        button1Text.text = clueOptions[0].clueName;
        button2Text.text = clueOptions[1].clueName;
        button3Text.text = clueOptions[2].clueName;

       // Option1.text = clueOptions[0].clueName;
       // Option2.text = clueOptions[1].clueName;
      //  Option3.text = clueOptions[2].clueName;
    }

    // Shuffle the list
    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void OnButtonClick(GameObject button)
    {
        // Check if the button clicked is one of the three buttons
        if (button == button1.gameObject || button == button2.gameObject || button == button3.gameObject)
        {
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();

            listDisplay.playerDeck.Add(buttonText.text);
            buttonsClicked++;

            Debug.Log("Button " + button.name + " is clicked");

            if (buttonsClicked >= 2)
            {
                Time.timeScale = 1f;
                losePanel.SetActive(false);
                buttonsClicked = 0;
                // set the drag and drop system
                CriminalCards.SetActive(true);
                EvidencePanel.SetActive(true);

            }
        }
    }
}
