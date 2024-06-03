using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    // Singleton instance of the InventoryManager
    public static InventoryManager instance;

    // Dictionary to hold the player's inventory
    private Dictionary<string, bool> playerInventory = new Dictionary<string, bool>();

    private void Awake()
    {
        // Singleton pattern 
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep the inventory persistent between scenes
        }
        else
        {
            Destroy(gameObject); // If another instance exists, destroy this one
        }
    }

    // Method to add an item to the player's inventory
    public void AddToInventory(string item)
    {
        if (!playerInventory.ContainsKey(item))
        {
            playerInventory.Add(item, true); // Add the item to the inventory with a value of true (collected)
        }
        else
        {
            Debug.LogWarning("Item " + item + " already exists in inventory.");
        }
    }

    // Method to check if an item is in the player's inventory
    public bool IsInInventory(string item)
    {
        return playerInventory.ContainsKey(item) && playerInventory[item];
    }
}
 