using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisassemblyManager : MonoBehaviour
{
    public GameObject[] partsToDisassemble;  // Objects that are disassembled
    public GameObject[] partsDisassembled;
    public GameObject[] AdditionalStep;      // Objects that appear after disassembling

    private int currentStep = 0;             // Track which step we are on
    private int _additionstep = 0;

    void Start()
    {
        for (int i = 0; i < partsToDisassemble.Length; i++)
        {
            partsDisassembled[i].SetActive(false);   // Disassembled parts are hidden
        }
    }

    void Update()
    {
        // Check for mouse click and raycast to detect what part was clicked
        if (Input.GetMouseButtonDown(0))  // Left mouse button click
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
                        ClickOnPart(i);  // Call the disassembly method for the clicked part
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

    // Method to proceed to the next step of disassembly
    public void ClickOnPart(int partIndex)
    {
        Debug.Log("Processing partIndex: " + partIndex);

        if (partIndex == currentStep)
        {
            Debug.Log("Correct part clicked, proceeding with disassembly for step: " + currentStep);

            // Handle specific steps
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

            // Deactivate the clicked part
            partsToDisassemble[partIndex].SetActive(false);
            Debug.Log("Deactivated part: " + partsToDisassemble[partIndex].name);

            // Activate the corresponding disassembled part
            partsDisassembled[partIndex].SetActive(true);
            Debug.Log("Activated disassembled part: " + partsDisassembled[partIndex].name);

            // Move to the next step
            currentStep++;
            Debug.Log("Moved to next step: " + currentStep);
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
            partsToDisassemble[i].SetActive(true);   // Reset initial parts
            partsDisassembled[i].SetActive(false);   // Hide disassembled parts
        }

        for (int i = 0; i < AdditionalStep.Length; i++)
        {
            AdditionalStep[i].SetActive(false);      // Hide all additional steps
        }

        Debug.Log("Disassembly reset complete");
    }
}
