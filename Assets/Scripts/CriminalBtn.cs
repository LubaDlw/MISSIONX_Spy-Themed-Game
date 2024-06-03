using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriminalBtn : MonoBehaviour
{
    // Start is called before the first frame update
    // declare all panels so thatg if one is active the other is not
    

    //criminals

    public GameObject PapitoPnl;
    public GameObject BulldogPnl;
    public GameObject GhostPnl;

    public void ToggleBulldog()
    {
        // Check if the panel is active or inactive and toggle its state accordingly
        BulldogPnl.SetActive(!BulldogPnl.activeSelf);
    }

    public void TogglePapito()
    {
        // Check if the panel is active or inactive and toggle its state accordingly
        PapitoPnl.SetActive(!PapitoPnl.activeSelf);
    }

    public void ToggleGhost()
    {
        // Check if the panel is active or inactive and toggle its state accordingly
        GhostPnl.SetActive(!GhostPnl.activeSelf);
    }




}
