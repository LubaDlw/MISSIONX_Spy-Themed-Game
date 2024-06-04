using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject red, blue, green, yellow, orange;
    public List<GameObject> redBlackList = new List<GameObject>();
    public List<GameObject> blueBlackList = new List<GameObject>();
    public List<GameObject> greenBlackList = new List<GameObject>();
    public List<GameObject> yellowBlackList = new List<GameObject>();
    public List<GameObject> orangeBlackList = new List<GameObject>();

    public CriminalInfo criminal;
    public List<CriminalInfo> criminals = new List<CriminalInfo>();

    //testList
    public List<GameObject> panelPos; //this is where they are shown on the panel
    public Transform panelToDisplay;
    public GameObject panel;
    public GameObject NPCinteraction;
    public GameObject CriminalDisplay;

    Vector2 redInitialPos, blueInitialPos, greenInitialPos, yellowInitialPos, orangeInitialPos;

    bool redCorrect, blueCorrect, greenCorrect, yellowCorrect, orangeCorrect = false;
    public GameObject cluePanel; // this is where the clues are showed

    public List<GameObject> colorGameObjects; //this the list used

    // Player Score System
    public int player1Score;
    public int player2Score;
    public TMP_Text player1ScoreTxt;
    public TMP_Text player2ScoreTxt;
    public TMP_Text feedbacktxt;
    public string feedback;

    public int currentPlayer;


    //
    public static Manager Instance { get; private set; }

    private void Awake()
    {
        
        player1Score = PlayerPrefs.GetInt("Player1Score", 0);
        player2Score = PlayerPrefs.GetInt("Player2Score", 0);
    }
    void Start()
    {

        NPCinteraction.SetActive(true);
        panel.SetActive(false);
        cluePanel.SetActive(false);
        CriminalDisplay.SetActive(false);
        feedbacktxt.text = " ";

       /* redInitialPos = red.transform.position;
        blueInitialPos = blue.transform.position;
        greenInitialPos = green.transform.position;
        yellowInitialPos = yellow.transform.position;
        orangeInitialPos = orange.transform.position;
       */

        // Initialize the list of color game objects
      //  colorGameObjects = new List<GameObject> { red, blue, green, yellow, orange };
        
        // Randomize which three objects are visible
      //  SetRandomVisibleColors();
    }

   public  void NPCDone()
    {
        NPCinteraction.SetActive(false);
        panel.SetActive(true);
        cluePanel.SetActive(true);
        CriminalDisplay.SetActive(true);

        // Initialize the list of color game objects
       // colorGameObjects = new List<GameObject> { red, blue, green, yellow, orange };

        // Randomize which three objects are visible
        SetRandomVisibleColors();

    }


    public void SetRandomVisibleColors()
    {
        // Shuffle the list of color game objects
        for (int i = 0; i < colorGameObjects.Count; i++)
        {
            GameObject temp = colorGameObjects[i];
            int randomIndex = Random.Range(i, colorGameObjects.Count);
            colorGameObjects[i] = colorGameObjects[randomIndex];
            colorGameObjects[randomIndex] = temp;
        }

        // Set the first three objects as active and the rest as inactive
        for (int i = 0; i < colorGameObjects.Count; i++)
        {
            if (i < 3)
            {
                colorGameObjects[i].SetActive(true);
                redInitialPos = red.transform.position;
                blueInitialPos = blue.transform.position;
                greenInitialPos = green.transform.position;
                yellowInitialPos = yellow.transform.position;
                orangeInitialPos = orange.transform.position;


                if (i < panelPos.Count)
                {
                    colorGameObjects[i].transform.SetParent(panel.transform, false);
                    colorGameObjects[i].transform.position = panelPos[i].transform.position;
                }
                else
                {
                    Debug.LogWarning("Not enough panel positions defined for the number of active objects.");
                }
            }
            else
            {
                colorGameObjects[i].SetActive(false);
            }
        }
    }

    

   

    

    public void DragOrange()
    {
        orange.transform.position = Input.mousePosition;
    }

    public void DragYellow()
    {
        yellow.transform.position = Input.mousePosition;
    }

    public void DragGreen()
    {
        green.transform.position = Input.mousePosition;
    }

    public void DragBlue()
    {
        blue.transform.position = Input.mousePosition;
    }

    public void DragRed()
    {
        red.transform.position = Input.mousePosition;
    }

    public void DropOrange()
    {
        DropObject(orange, orangeBlackList, ref orangeCorrect, orangeInitialPos);
    }



                             







    public void DropYellow()
    {
        DropObject(yellow, yellowBlackList, ref yellowCorrect, yellowInitialPos);
    }

    public void DropGreen()
    {
        DropObject(green, greenBlackList, ref greenCorrect, greenInitialPos);
    }

    public void DropBlue()
    {
        DropObject(blue, blueBlackList, ref blueCorrect, blueInitialPos);
    }

    public void DropRed()
    {
        DropObject(red, redBlackList, ref redCorrect, redInitialPos);
    }

    private void DropObject(GameObject obj, List<GameObject> blackList, ref bool correctFlag, Vector2 initialPos)
    {
        foreach (GameObject blackSlot in blackList)
        {
            float distance = Vector3.Distance(obj.transform.position, blackSlot.transform.position);

            if (distance < 50)
            {
                obj.transform.position = blackSlot.transform.position;
                correctFlag = true;
                foreach (CriminalInfo criminal in criminals)
                {
                    foreach (GameObject slot in criminal.blackSlots)
                    {
                        if (slot == blackSlot)
                        {
                            Debug.Log($"The {obj.name} was dropped on {criminal.CriminalName}");
                            feedback = ($"The {obj.name} was dropped on {criminal.CriminalName}");

                            //replace slot wt GameObject and make it there permenantly
                            //Disable drag
                            obj.GetComponent<EventTrigger>().enabled = false;
                            //ReplaceSlot wt Game Object
                            ReplaceSlotWithObject(blackSlot, obj);
                            checkScore(criminal);
                            colorGameObjects.Remove(obj);
                            return;
                        }
                    }
                }
                return;
            }
        }

        obj.transform.position = initialPos;
    }

    private void ReplaceSlotWithObject(GameObject slot, GameObject obj)
    {
        slot.SetActive(false); // Hide the slot object
        obj.transform.SetParent(slot.transform.parent, false); // Make the dropped object a child of the slot's parent
        obj.transform.position = slot.transform.position; // Position the dropped object at the slot's position
    }

    void checkScore(CriminalInfo criminal)
    {
        criminal.cluesFound++;
        if (criminal.cluesFound == criminal.TotalClues)
        {
            Debug.Log("All clues found for " + criminal.CriminalName);
            feedback = ("All clues found for " + criminal.CriminalName);
            if(currentPlayer == 0)
            {
                player1Score += criminal.points;
            }
            else if (currentPlayer == 1)
            {
                player2Score += criminal.points;
            }
            //player1Score += criminal.points;
        }
    }

    public void UpdatePlayerScoreText()
    {
        if (player1ScoreTxt != null && player2ScoreTxt != null)
        {
            player1ScoreTxt.text = "Player 1 Score: " + player1Score.ToString(); // Update text with player's score
            player2ScoreTxt.text = "Player 2 Score: " + player2Score.ToString();
        }
        else
        {
            Debug.LogWarning("Text element reference is not set!");
        }
    }

    private void Update()
    {
        if (redCorrect && blueCorrect && greenCorrect && yellowCorrect && orangeCorrect)
        {
            Debug.Log("You Win");
        }
        UpdatePlayerScoreText();
        feedbacktxt.text = feedback;
    }


}
