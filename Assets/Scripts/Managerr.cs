using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    public TMP_Text turnText;

    //HAIR
    [Header("HAIR")]
    public GameObject blackHair;
    public GameObject blondeHair;
    public GameObject red, red1, red2;

    //Eyes
    [Header("Eyes")]
    public GameObject blueEyes;
    public GameObject greenEyes;
    public GameObject brownEyes;

    //Weight
    [Header("Weight")]
    public GameObject sixty;
    public GameObject eighty;
    public GameObject hundred;

    //Height
    [Header("Height")]
    public GameObject oneSixty;
    public GameObject oneEighty;
    public GameObject twoHundred;

    //SuitColor
    [Header("Suit Color")]
    public GameObject blackSuit;
    public GameObject greySuit;
    public GameObject stripedSuit;



    public GameObject blue, green, yellow, orange;

    public List<GameObject> colorPrefabs;

    //HAIR
    public List<GameObject> redBlackList = new List<GameObject>();
    public List<GameObject> blackHairList = new List<GameObject>(); // Black Hair
    public List<GameObject> blondeHairList = new List<GameObject>(); // Blonde Hair

    //Eyes
    public List<GameObject> brownEyesList = new List<GameObject>(); // Brown Eyes
    public List<GameObject> blueBlackList = new List<GameObject>();//Blue Eyes
    public List<GameObject> greenBlackList = new List<GameObject>(); //GreenEyes

    //Weight
    public List<GameObject> sixtyKG = new List<GameObject>(); // 60 KG's
    public List<GameObject> eightyKG = new List<GameObject>(); // 80 KG's
    public List<GameObject> hundredKG = new List<GameObject>(); // 100 KG's


    //Height
    public List<GameObject> oneSixtyM = new List<GameObject>(); // 160 metres
    public List<GameObject> oneEightyM = new List<GameObject>(); // 180 metres
    public List<GameObject> twoHundredM = new List<GameObject>(); // 200 metres


    // Suit Color
    public List<GameObject> blackSuitList = new List<GameObject>(); // Black Suit
    public List<GameObject> greySuitList = new List<GameObject>(); // Grey Suit
    public List<GameObject> stripedSuitList = new List<GameObject>(); // Striped Suit


    public List<GameObject> yellowBlackList = new List<GameObject>();
    public List<GameObject> orangeBlackList = new List<GameObject>();

    public CriminalInfo criminal;
    public List<CriminalInfo> criminals = new List<CriminalInfo>(); //player1 criminals
    public List<CriminalInfo> player2Criminals = new List<CriminalInfo>(); // player 2's criminals

    public List<GameObject> panelPos; // this is where the clues are generated forPl1
    public List<GameObject> panelPosPlayer2; // this is where the clues are generaed for PL2
    public Transform panelToDisplay;
    public GameObject panelCl1; // player 1's clues
    public GameObject panelCl2; //Player2 clue panel
    public GameObject NPCinteraction;
    public GameObject CriminalDisplay;
    public GameObject player2CriminalDisplay; //player 2 criminals
    

    Vector2 redInitialPos, red1InitialPos, red2InitialPos, blueInitialPos, greenInitialPos, yellowInitialPos, orangeInitialPos;

    bool redCorrect, red1Correct, red2Correct, blueCorrect, greenCorrect, yellowCorrect, orangeCorrect = false;
    public GameObject cluePanel;

    public List<GameObject> colorGameObjects;

    public int player1Score;
    public int player2Score;
    public TMP_Text player1ScoreTxt;
    public TMP_Text player2ScoreTxt;
    public TMP_Text feedbacktxt;
    public string feedback;

    public int currentPlayer = 0;

    public GameObject panelPlayer1;
    public GameObject panelPlayer2;
    // int roundsPlayed = 0;
    //int currentPlayer = 1;

    int successfulDropsCount = 0;


    public static Manager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        NPCinteraction.SetActive(true); // first game screen that explains game
       // panel.SetActive(false);
        cluePanel.SetActive(false);
        //CriminalDisplay.SetActive(false);
        panelPlayer1.SetActive(false);
        feedbacktxt.text = " ";

        colorGameObjects = new List<GameObject> { red, red1, red2, blue, green, yellow, orange };

      // SetRandomVisibleColors();
    }

    public void NPCDone() // this the button after the panel; on player 1
    {
        NPCinteraction.SetActive(false);

       // panel.SetActive(true); //Player 1 
      //  CriminalDisplay.SetActive(true); //player 1 criminal

        cluePanel.SetActive(false); // where clues are
        currentPlayer = 0;
        successfulDropsCount = 0;
        SetPlayerPanels(currentPlayer);


        Debug.Log("This code is running");

        SetRandomVisibleColors();
    }
    void SetPlayerPanels(int player)
    {
        if (player == 0)
        {
            SetRandomVisibleColors(); // set new clues after player turn
            // new clues 
            panelPlayer1.SetActive(true); Debug.Log("Player 1 panel is activated");
            panelPlayer2.SetActive(false);
            panelPlayer1.GetComponent<PanelInteractivityControl>().EnablePanelInteractivity();
            panelPlayer2.GetComponent<PanelInteractivityControl>().DisablePanelInteractivity();
        }
        else
        {
            panelPlayer1.SetActive(false);
            panelPlayer2.SetActive(true);
            Debug.Log("Player 2 panel is activated");
            panelPlayer1.GetComponent<PanelInteractivityControl>().DisablePanelInteractivity();
            panelPlayer2.GetComponent<PanelInteractivityControl>().EnablePanelInteractivity();
        }
    }

    void SetRandomVisibleColors()
    {

        ClearPanelClues(panelCl1);
        ClearPanelClues(panelCl2);
        ShuffleColorGameObjects();

        for (int i = 0; i < colorGameObjects.Count; i++)
        {
            if (i < 3)
            {
                colorGameObjects[i].SetActive(true);
                SetInitialPosition(colorGameObjects[i]);

                // for player 1
                if (i < panelPos.Count)
                {
                    colorGameObjects[i].transform.SetParent(panelCl1.transform, false);
                    colorGameObjects[i].transform.position = panelPos[i].transform.position;
                }
                else
                {
                    Debug.LogWarning("Not enough panel positions defined for the number of active objects.");
                }

                // forplayer 2

                if (i < panelPosPlayer2.Count)
                {
                    GameObject clone = Instantiate(colorGameObjects[i]);
                    clone.transform.SetParent(panelCl2.transform, false);
                    clone.transform.position = panelPosPlayer2[i].transform.position;

                    EventTrigger trigger = clone.GetComponent<EventTrigger>();
                    if (trigger != null)
                    {
                        trigger.triggers.Clear(); // Clear existing triggers if any
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.Drag;
                        entry.callback.AddListener((eventData) => { DragObject(clone); });
                        trigger.triggers.Add(entry);

                        // Add other event type
                        // ok so we need to figure out how to get correctdrag and drop logic forclones 

                        EventTrigger dropTrigger = clone.GetComponent<EventTrigger>();
                        if (dropTrigger != null)
                        {
                            //dropTrigger.triggers.Clear(); // Clear existing triggers if any
                            EventTrigger.Entry dropEntry = new EventTrigger.Entry();
                            dropEntry.eventID = EventTriggerType.Drop; // Use appropriate event type for drop
                            string tag = clone.tag;

                            switch (tag)
                            {
                                case "Red":
                                    dropEntry.callback.AddListener((eventData) => { DropObject(clone, redBlackList, ref redCorrect, redInitialPos); });
                                    break;
                                case "Blue":
                                    dropEntry.callback.AddListener((eventData) => { DropObject(clone, blueBlackList, ref blueCorrect, blueInitialPos); });
                                    break;
                                case "Green":
                                    dropEntry.callback.AddListener((eventData) => { DropObject(clone, greenBlackList, ref greenCorrect, greenInitialPos); });
                                    break;
                                case "Yellow":
                                    dropEntry.callback.AddListener((eventData) => { DropObject(clone, yellowBlackList, ref yellowCorrect, yellowInitialPos); });
                                    break;
                                case "Orange":
                                    dropEntry.callback.AddListener((eventData) => { DropObject(clone, orangeBlackList, ref orangeCorrect, orangeInitialPos); });
                                    break;
                                // Add cases for other tags
                                default:
                                    Debug.LogWarning("Unhandled tag: " + tag);
                                    break;
                            }
                            dropTrigger.triggers.Add(dropEntry);
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("Not enough panel positions defined for Player 2.");
                }
            }
            else
            {
                colorGameObjects[i].SetActive(false);
            }
        }
    }

    void ClearPanelClues(GameObject panel)
    {
        foreach (Transform child in panel.transform)
        {
            child.gameObject.SetActive(false); //This is to fix the bug where more previoyus clues still appear on panel
        }
    }

    void ShuffleColorGameObjects()
    {
        for (int i = 0; i < colorGameObjects.Count; i++)
        {
            GameObject temp = colorGameObjects[i];
            int randomIndex = Random.Range(i, colorGameObjects.Count);
            colorGameObjects[i] = colorGameObjects[randomIndex];
            colorGameObjects[randomIndex] = temp;
        }
    }

    void SetInitialPosition(GameObject obj)
    {
        if (obj == red)
            redInitialPos = obj.transform.position;
        else if (obj == red1)
            red1InitialPos = obj.transform.position;
        else if (obj == red2)
            red2InitialPos = obj.transform.position;
        else if (obj == blue)
            blueInitialPos = obj.transform.position;
        else if (obj == green)
            greenInitialPos = obj.transform.position;
        else if (obj == yellow)
            yellowInitialPos = obj.transform.position;
        else if (obj == orange)
            orangeInitialPos = obj.transform.position;
    }

    public void DragObject(GameObject obj)
    {
        obj.transform.position = Input.mousePosition;
    }

    public void DragRed()
    {
        DragObject(red);
    }

    public void DragRed1()
    {
        DragObject(red1);
    }

    public void DragRed2()
    {
        DragObject(red2);
    }

    public void DragBlue()
    {
        DragObject(blue);
    }

    public void DragGreen()
    {
        DragObject(green);
    }

    public void DragYellow()
    {
        DragObject(yellow);
    }

    public void DragOrange()
    {
        DragObject(orange);
    }

    public void DropRed()
    {
        DropObject(red, redBlackList, ref redCorrect, redInitialPos);
    }

    public void DropRed1()
    {
        DropObject(red1, redBlackList, ref red1Correct, red1InitialPos);
    }

    public void DropRed2()
    {
        DropObject(red2, redBlackList, ref red2Correct, red2InitialPos);
    }

    public void DropBlue()
    {
        DropObject(blue, blueBlackList, ref blueCorrect, blueInitialPos);
    }

    public void DropGreen()
    {
        DropObject(green, greenBlackList, ref greenCorrect, greenInitialPos);
    }

    public void DropYellow()
    {
        DropObject(yellow, yellowBlackList, ref yellowCorrect, yellowInitialPos);
    }

    public void DropOrange()
    {
        DropObject(orange, orangeBlackList, ref orangeCorrect, orangeInitialPos);
    }

    private void DropObject(GameObject obj, List<GameObject> blackList, ref bool correctFlag, Vector2 initialPos)
    {
        // Determine the active panel based on the current player
        GameObject activePanel = currentPlayer == 0 ? panelPlayer1 : panelPlayer2;

        foreach (GameObject blackSlot in blackList)
        {
            // Check if the blackSlot is part of the active panel
            if (!blackSlot.transform.IsChildOf(activePanel.transform)) // fix raycast issue
            {
                continue;
            }

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
                            feedback = $"The {obj.name} was dropped on {criminal.CriminalName}";
                            // keep track of turn moves
                            obj.GetComponent<EventTrigger>().enabled = false;
                            ReplaceSlotWithObject(blackSlot, obj);
                            checkScore(criminal);
                            colorGameObjects.Remove(obj);

                            if (correctFlag)
                            {
                                successfulDropsCount++;

                                if (successfulDropsCount >= 2)
                                {
                                    Debug.Log("Next Players Turn");
                                    // Switch turns after two successful drops
                                    currentPlayer = (currentPlayer + 1) % 2; // Switch between 0 and 1
                                    successfulDropsCount = 0; // Reset successful drops count
                                    SetPlayerPanels(currentPlayer);
                                    if (currentPlayer == 0)
                                    {
                                        Debug.Log("The generate clue code is running");
                                        //SetRandomVisibleColors(); //new clues on next turn 
                                        // add method to keep track of how many rounds have passed
                                    }
                                    // Generate clues for the next player
                                    //SetRandomVisibleColors();
                                }
                            }
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
        slot.SetActive(false);
        obj.transform.SetParent(slot.transform.parent, false);
        obj.transform.position = slot.transform.position;
    }

    void checkScore(CriminalInfo criminal)
    {
        criminal.cluesFound++;
        if (criminal.cluesFound == criminal.TotalClues)
        {
            Debug.Log("All clues found for " + criminal.CriminalName);
            feedback = "All clues found for " + criminal.CriminalName;
            if (currentPlayer == 0)
            {
                player1Score += criminal.points;
            }
            else if (currentPlayer == 1)
            {
                player2Score += criminal.points;
            }
        }
    }

    public void UpdatePlayerScoreText()
    {
        if (player1ScoreTxt != null && player2ScoreTxt != null)
        {
            player1ScoreTxt.text = "Player 1 Score: " + player1Score.ToString();
            player2ScoreTxt.text = "Player 2 Score: " + player2Score.ToString();
        }
        else
        {
            Debug.LogWarning("Text element reference is not set!");
        }
    }

    private void Update()
    {
        UpdateTurnText();
        if (redCorrect && red1Correct && red2Correct && blueCorrect && greenCorrect && yellowCorrect && orangeCorrect)
        {
            Debug.Log("You Win");
        }
        UpdatePlayerScoreText();
        feedbacktxt.text = feedback;
    }

    void UpdateTurnText()
    {
        if (turnText != null)
        {
            if (currentPlayer == 0)
            {
                turnText.text = "Player 1's Turn";
            }
            else if (currentPlayer == 1)
            {
                turnText.text = "Player 2's Turn";
            }
        }
        else
        {
            Debug.LogWarning("Turn Text reference is not set!");
        }
    }
}
