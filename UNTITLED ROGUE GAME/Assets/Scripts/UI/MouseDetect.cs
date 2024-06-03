using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseDetect : MonoBehaviour
{
    // Reference to the button component
    private Button button;

    void Start()
    {
        // Get the button component attached to this GameObject
        button = GetComponent<Button>();

        // Check if a button component is found
        if (button != null)
        {
            // Subscribe to the button's click event
            button.onClick.AddListener(OnClick);
        }
        else
        {
            Debug.LogWarning("Button component not found on GameObject: " + gameObject.name);
        }
    }

    // Method to handle button click event
    private void OnClick()
    {
        // Print the name of the clicked button
        Debug.Log("Clicked button: " + gameObject.name);
    }
}
