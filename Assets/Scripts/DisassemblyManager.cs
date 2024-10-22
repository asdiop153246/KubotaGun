using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisassemblyManager : MonoBehaviour
{
    public GameObject[] partsToDisassemble; // Objects to disassemble
    public GameObject[] partsDisassembled; // Disassembled parts
    public GameObject[] additionalSteps; // Additional objects for special steps
    public GameObject[] stepCheckmarks; // Checkmarks for completed steps
    public GameObject[] partDetails; // Detail objects to activate on part click
    public GameObject[] PartClick; // Clickable objects to show details
    public GameObject completeScene; // Complete scene object

    public AudioSource _sound;

    public int currentStep = 0; // Track the current step
    private int _additionalStepIndex = 0;

    void Start()
    {
        InitializeParts();
        EnableOutline(0); // Enable outline for the first part
        HideAllCheckmarks(); // Ensure all checkmarks are hidden initially
        HideAllDetails(); // Ensure all part details are hidden initially
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect left-click
        {
            DetectClick();
        }
    }

    void InitializeParts()
    {
        // Hide all disassembled parts and disable outlines
        for (int i = 0; i < partsToDisassemble.Length; i++)
        {
            partsDisassembled[i].SetActive(false);

            Outline outline = partsToDisassemble[i].GetComponent<Outline>();
            if (outline != null)
                outline.enabled = false;
        }
    }

    void HideAllCheckmarks()
    {
        // Ensure all checkmarks are inactive at the start
        foreach (var checkmark in stepCheckmarks)
        {
            checkmark.SetActive(false);
        }
    }

    void HideAllDetails()
    {
        // Ensure all part detail objects are inactive at the start
        foreach (var detail in partDetails)
        {
            detail.SetActive(false);
        }
    }

    void DetectClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            for (int i = 0; i < PartClick.Length; i++)
            {
                if (hit.transform.gameObject == PartClick[i])
                {
                    // Toggle the visibility of the detail for this part
                    bool isActive = partDetails[i].activeSelf;
                    partDetails[i].SetActive(!isActive); // If active, set inactive; if inactive, set active
                    return; // Exit since we only need to toggle the detail
                }
            }

            // Check if the clicked object is a part to disassemble
            for (int i = 0; i < partsToDisassemble.Length; i++)
            {
                if (hit.transform.gameObject == partsToDisassemble[i])
                {
                    ClickOnPart(i); // Disassemble the part and handle logic
                    return;
                }
            }
        }
    }

    void EnableOutline(int partIndex)
    {
        // Disable all outlines
        foreach (var part in partsToDisassemble)
        {
            Outline outline = part.GetComponent<Outline>();
            if (outline != null)
                outline.enabled = false;
        }

        // Enable outline for the selected part
        Outline selectedOutline = partsToDisassemble[partIndex].GetComponent<Outline>();
        if (selectedOutline != null)
            selectedOutline.enabled = true;
    }

    public void ClickOnPart(int partIndex)
    {
        if (partIndex == currentStep) // Correct part clicked
        {
            partsToDisassemble[partIndex].SetActive(false); // Hide original part
            partsDisassembled[partIndex].SetActive(true); // Show disassembled part

            // Special handling for different steps
            if (currentStep >= 7 && currentStep != 6 && currentStep != 9)
            {
                if (currentStep >= 10)
                {
                    ActivateCheckmark(currentStep - 2);
                }
                else
                {
                    ActivateCheckmark(currentStep - 1);
                }
            }
            else if (currentStep != 6 && currentStep < 7)
            {
                ActivateCheckmark(currentStep);
            }
            else if (currentStep != 9 && currentStep > 7)
            {
                ActivateCheckmark(currentStep);
            }

            currentStep++; // Move to the next step
            _sound.Play();
            HandleAdditionalSteps();
            CheckCompletion();

        }
    }

    void ShowDetail(int partIndex)
    {
        if (partIndex < partDetails.Length)
        {
            partDetails[partIndex].SetActive(true); // Show the detail object for the clicked part
        }
    }

    void ActivateCheckmark(int step)
    {
        if (step < stepCheckmarks.Length)
        {
            stepCheckmarks[step].SetActive(true); // Show the checkmark for the completed step
        }
    }

    void HandleAdditionalSteps()
    {
        // Activate additional steps based on the current step
        if (currentStep == 3 || currentStep == 6)
        {
            additionalSteps[_additionalStepIndex].SetActive(true);
            _additionalStepIndex++;
            additionalSteps[_additionalStepIndex].SetActive(false);
            _additionalStepIndex++;
        }

        if (currentStep < partsToDisassemble.Length)
        {
            EnableOutline(currentStep);
        }
    }

    void CheckCompletion()
    {
        if (currentStep >= partsDisassembled.Length)
        {
            completeScene.SetActive(true); // Show complete scene
        }
    }

    public void ResetDisassembly()
    {
        currentStep = 0;
        _additionalStepIndex = 0;

        // Reset all parts, details, and checkmarks
        foreach (var part in partsToDisassemble)
            part.SetActive(true);
        foreach (var disassembled in partsDisassembled)
            disassembled.SetActive(false);
        foreach (var step in additionalSteps)
            step.SetActive(false);
        HideAllCheckmarks(); // Hide all checkmarks
        HideAllDetails(); // Hide all details

        EnableOutline(0); // Enable outline for
    }
}
