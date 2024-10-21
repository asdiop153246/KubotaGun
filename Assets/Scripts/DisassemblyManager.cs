using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisassemblyManager : MonoBehaviour
{
    public GameObject[] partsToDisassemble; // Objects that are disassembled
    public GameObject[] partsDisassembled;
    public GameObject[] AdditionalStep; // Objects that appear after disassembling

    public GameObject Completescene;

    public int currentStep = 0; // Track which step we are on
    private int _additionstep = 0;

    void Start()
    {
        for (int i = 0; i < partsToDisassemble.Length; i++)
        {
            partsDisassembled[i].SetActive(false); // Disassembled parts are hidden

            // Ensure outline component is disabled at the start
            Outline outlineComponent = partsToDisassemble[i].GetComponent<Outline>();
            if (outlineComponent != null)
            {
                outlineComponent.enabled = false; // Disable outline initially
            }
        }

        // Enable the outline for the first object in the array (index 0)
        Outline firstOutline = partsToDisassemble[0].GetComponent<Outline>();
        if (firstOutline != null)
        {
            firstOutline.enabled = true; // Enable outline for the first part at the start
            Debug.Log("Outline enabled for the first part: " + partsToDisassemble[0].name);
        }
        else
        {
            Debug.LogWarning(
                "No outline component found on the first part: " + partsToDisassemble[0].name
            );
        }
    }

    void Update()
    {
        // Check for mouse click and raycast to detect what part was clicked
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            Debug.Log("Mouse click detected");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Raycast hit object: " + hit.transform.gameObject.name);

                // Check if the clicked object is part of the disassembly parts
                for (int i = 0; i < partsToDisassemble.Length; i++)
                {
                    if (hit.transform.gameObject == partsToDisassemble[i])
                    {
                        Debug.Log("Clicked on part: " + partsToDisassemble[i].name);
                        ClickOnPart(i);
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Raycast did not hit any object");
            }
        }
    }

    // Method to enable outline for a selected part
    void EnableOutline(int partIndex)
    {
        Debug.Log("Enabling outline for part: " + partsToDisassemble[partIndex].name);

        // Disable all outlines first
        for (int i = 0; i < partsToDisassemble.Length; i++)
        {
            Outline outlineComponent = partsToDisassemble[i].GetComponent<Outline>();
            if (outlineComponent != null)
            {
                outlineComponent.enabled = false; // Disable outline for all parts
            }
        }

        // Enable outline for the selected part
        Outline selectedOutline = partsToDisassemble[partIndex].GetComponent<Outline>();
        if (selectedOutline != null)
        {
            selectedOutline.enabled = true; // Enable outline for the clicked part
            Debug.Log("Outline enabled for: " + partsToDisassemble[partIndex].name);
        }
        else
        {
            Debug.LogWarning(
                "No outline component found on: " + partsToDisassemble[partIndex].name
            );
        }
    }

    // Method to proceed to the next step of disassembly
    public void ClickOnPart(int partIndex)
    {
        Debug.Log("Processing partIndex: " + partIndex);

        if (partIndex == currentStep)
        {
            Debug.Log("Correct part clicked, proceeding with disassembly for step: " + currentStep);

            // Handle specific steps


            // Deactivate the clicked part
            partsToDisassemble[partIndex].SetActive(false);
            Debug.Log("Deactivated part: " + partsToDisassemble[partIndex].name);

            // Activate the corresponding disassembled part
            partsDisassembled[partIndex].SetActive(true);
            Debug.Log("Activated disassembled part: " + partsDisassembled[partIndex].name);
            if (currentStep == 2)
            {
                Debug.Log("Activating additional step object at index: " + _additionstep);
                AdditionalStep[_additionstep].SetActive(true);
                _additionstep++;
                AdditionalStep[_additionstep].SetActive(false);
                _additionstep++;
            }
            else if (currentStep == 5)
            {
                Debug.Log("Handling additional step object activation for step 6");
                AdditionalStep[_additionstep].SetActive(false);
                _additionstep++;
                AdditionalStep[_additionstep].SetActive(true);
                _additionstep++;
            }
            // Move to the next step
            currentStep++;
            Debug.Log("Moved to next step: " + currentStep);

            // Enable the outline for the next part in the array, if available
            if (currentStep < partsToDisassemble.Length)
            {
                EnableOutline(currentStep); // Enable outline for the next part
            }
            else if (currentStep == partsDisassembled.Length)
            {
                Completescene.SetActive(true);
            }
            else
            {
                Debug.Log("No more parts to highlight.");
            }
        }
        else
        {
            Debug.Log("Clicked on incorrect part for the current step: " + currentStep);
        }
    }

    // Optional: Reset the disassembly process
    public void ResetDisassembly()
    {
        Debug.Log("Resetting disassembly process");

        currentStep = 0;
        _additionstep = 0;

        for (int i = 0; i < partsToDisassemble.Length; i++)
        {
            partsToDisassemble[i].SetActive(true); // Reset initial parts
            partsDisassembled[i].SetActive(false); // Hide disassembled parts

            // Ensure outline component is disabled when resetting
            Outline outlineComponent = partsToDisassemble[i].GetComponent<Outline>();
            if (outlineComponent != null)
            {
                outlineComponent.enabled = false; // Disable outline
            }
        }

        for (int i = 0; i < AdditionalStep.Length; i++)
        {
            AdditionalStep[i].SetActive(false); // Hide all additional steps
        }

        Debug.Log("Disassembly reset complete");
    }
}
