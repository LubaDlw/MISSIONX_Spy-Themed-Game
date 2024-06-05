using UnityEngine;
using UnityEngine.UI;

public class PanelInteractivityControl : MonoBehaviour
{
    // Reference to the GraphicRaycaster component of the panel
    private GraphicRaycaster graphicRaycaster;

    private void Start()
    {
        // Get the GraphicRaycaster component attached to the panel
        graphicRaycaster = GetComponent<GraphicRaycaster>();
    }

    // Method to enable interaction with the panel
    public void EnablePanelInteractivity()
    {
        // Enable the GraphicRaycaster to allow interaction with the panel's objects
        graphicRaycaster.enabled = true;
    }

    // Method to disable interaction with the panel
    public void DisablePanelInteractivity()
    {
        // Disable the GraphicRaycaster to prevent interaction with the panel's objects
        graphicRaycaster.enabled = false;
    }
}
