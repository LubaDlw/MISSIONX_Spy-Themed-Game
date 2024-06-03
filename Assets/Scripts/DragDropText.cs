 using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class DragDropText : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string requiredText;
    public GameObject targetObject; // Reference to the target game object
    public GameObject initialPositionObject; // Reference to the GameObject representing the initial position

    private RectTransform rectTransform;
    private Canvas canvas;
    private bool isDragging = false;
    public Vector3 initialPosition; // Store the initial position of the text element
    public ListDisplay listDisplay;
    public List<DropSlot> dropPanels;
    public TMP_Text clue;

    public TMP_Text dragText;
    public GameObject outcome;


    public CriminalManager criminalInfo;

    public DropSlot dropSlot;

    /* public string height;
     public string hairColor;
     public string eyeColor;

     public string ClueTxt;
    */
    public string ClueTxt;

    private TMP_Text tmpTextComponent;
    private int outcomeActivationCount = 0; // number of times player gets wrong match

    // Add the GameObject you want to display when outcome is activated more than twice
    public GameObject endLevelPanel;



    public void Start()
    {
        tmpTextComponent = GetComponent<TMP_Text>();
    }

    void Update()
    {
        ClueTxt = clue.text;
        // Access the text of the TMP_Text component
        string currentText = tmpTextComponent.text;

        // Check if the text matches a specific string or condition
        if (currentText == "specific_string_or_condition")
        {
            // Perform an action if the text matches
            Debug.Log("Text matches the specific string or condition.");

            // Add the text to the player's deck
            listDisplay.playerDeck.Add(currentText);

            // Perform additional actions as needed
        }
    }



    // refernece SO

    void onDropOfMouse()
    {
        Debug.Log("On drop mouse works");
        if (ClueTxt == "hi")
        {
            Debug.Log("Height is a match");
        }
        else
        {
            Debug.Log("Text does not match slot requirements or did not collide with the target object.");
            // Reset to orginal position if it does not match
            rectTransform.position = initialPosition;
        }
    }




    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        initialPosition = initialPositionObject.transform.position; // Store the initial position
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        rectTransform.SetAsLastSibling(); // Move the text to the top of the hierarchy within the canvas
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }



    public void OnEndDrag(PointerEventData eventData)
    {
        // Stop dragging
        Debug.Log("Drag has ended");
        isDragging = false;

        // Calculate the RectTransform for the dragged object
        RectTransform draggedObjectRect = GetComponent<RectTransform>();

        // Iterate through each drop panel
        bool isDroppedOnPanel = false;
        foreach (DropSlot panel in dropPanels)
        {
            // Get the RectTransform for the current drop panel
            RectTransform dropPanelRect = panel.GetComponent<RectTransform>();

            // Check if the dragged object overlaps with the drop panel
            if (RectTransformUtility.RectangleContainsScreenPoint(dropPanelRect, draggedObjectRect.position))
            {
                Debug.Log($"Dragged object is within the drop panel area of {panel.name}.");

                // Check if the dragged text matches the drop slot requirements
                if (dragText.text == dropSlot.height || dragText.text == dropSlot.hairColor || dragText.text == dropSlot.eyeColor)
                {
                    Debug.Log($"Text matches slot requirements of {panel.name} and collided with the target object.");

                    // Perform desired actions, such as updating game state or changing text color
                    GetComponent<TMP_Text>().color = Color.green;
                    isDroppedOnPanel = true;
                    // Add other behavior as needed
                    break; // Exit loop once a match is found
                }
                else
                {
                    Debug.Log($"Text does not match slot requirements of {panel.name}.");
                    //DragText.text = " Does Not Match";

                }
            }
        }

        // If no match was found, reset the position of the dragged object
        if (!isDroppedOnPanel)
        {
            Debug.Log("Dragged object was not successfully dropped on any panel.");
            //DragText.text = " Does Not Match";
            rectTransform.position = initialPosition;
            ActivateGameObject();
           // dragText.text = " Does Not Match";




            // Clear the text


        }

        bool matchFound = false;

        // Iterate through the list of required TMP_Text elements
        foreach (TMP_Text clue in dropSlot.requiredCluess)
        {
            // Check if dragText.text matches the text of the TMP_Text element
            if (dragText == clue)
            {
                // Match found
                Debug.Log($"Text matches slot requirements of {gameObject.name} and collided with the target object.");

                // Perform desired actions, such as updating game state or changing text color
                GetComponent<TMP_Text>().color = Color.green;
                isDroppedOnPanel = true;
                matchFound = true;
                break; // Exit the loop once a match is found
            }
        }

        // If no match was found
        if (!matchFound)
        {
            Debug.Log($"Text does not match slot requirements of {gameObject.name}.");
            // Add additional code here for the case when there is no match, if necessary
        }
    }
    public void ActivateGameObject()
    {
        // Activate the GameObject
        outcome.SetActive(true);

        outcomeActivationCount++;

        // Start the coroutine to deactivate the GameObject after the duration
        StartCoroutine(DeactivateAfterDelay());

        if (outcomeActivationCount >= 3)
        {
            // Activate the special GameObject you want to display
            endLevelPanel.SetActive(true);
        }
    }

    // Coroutine that deactivates the GameObject after the specified duration
     IEnumerator DeactivateAfterDelay()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(3);

        // Deactivate the GameObject
        outcome.SetActive(false);
    }




    // Check if the dragged text collides with the target object
    private bool IsCollidingWithTarget()
    {
        if (targetObject == null)
            return false;

        RectTransform targetRectTransform = targetObject.GetComponent<RectTransform>();
        if (targetRectTransform == null)
            return false;

        Rect targetRect = new Rect(
            targetRectTransform.position.x - targetRectTransform.rect.width / 2,
            targetRectTransform.position.y - targetRectTransform.rect.height / 2,
            targetRectTransform.rect.width,
            targetRectTransform.rect.height);

        return targetRect.Contains(rectTransform.position);
    }
}
