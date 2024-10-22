using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;  // Needed for using the Image component

public class Clickableobject : MonoBehaviour, IPointerClickHandler
{
    public GameObject[] objects;  // Array of objects to be clicked
    public Sprite[] popups;       // Array of sprites to display (details as sprites)
    public Image image;           // Reference to the UI Image where the sprite will be displayed

    private bool[] isPopupActive; // Array to track whether each sprite is displayed

    void Start()
    {
        // Initialize the boolean array to track popup states
        isPopupActive = new bool[popups.Length];

        // Ensure no sprite is displayed initially
        image.gameObject.SetActive(false);  // Hide the Image component initially
        for (int i = 0; i < popups.Length; i++)
        {
            isPopupActive[i] = false;  // Initialize all as false
        }
    }

    // Implement IPointerClickHandler interface method
    public void OnPointerClick(PointerEventData eventData)
    {
        // Loop through the objects to check which one was clicked
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] == gameObject) // Check if the clicked object matches
            {
                // Toggle the sprite visibility for this object
                isPopupActive[i] = !isPopupActive[i];  // Toggle the boolean value

                if (isPopupActive[i])
                {
                    // Show the Image component and assign the corresponding sprite
                    image.gameObject.SetActive(true);
                    image.sprite = popups[i];  // Load the corresponding sprite from the array
                }
                else
                {
                    // Hide the Image component if toggled off
                    image.gameObject.SetActive(false);
                }
            }
        }
    }
}