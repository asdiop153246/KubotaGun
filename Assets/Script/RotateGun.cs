using UnityEngine;

public class RotateGun : MonoBehaviour
{
    public float rotationSpeed = 5f; // Rotation speed for the object

    public bool isDragging = false; // To track if the object is being dragged
    private Vector3 lastMousePosition; // To store the last mouse position

    void OnMouseDown()
    {
        // When the user clicks on the object, start dragging
        isDragging = true;

        // Store the initial mouse position when clicked
        lastMousePosition = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // When the user releases the mouse, stop dragging
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            // Calculate the difference in mouse position between frames
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;

            // Rotate the object based on horizontal mouse movement (x-axis)
            float rotationAmount = mouseDelta.x * rotationSpeed * Time.deltaTime;

            // Rotate the object around its Y axis (left and right)
            transform.Rotate(Vector3.up, -rotationAmount);

            // Update the last mouse position
            lastMousePosition = Input.mousePosition;
        }
    }
}
