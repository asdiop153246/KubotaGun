using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisassemblyManager : MonoBehaviour
{
    // Array to hold all parts to disassemble
    public GameObject[] partsToDisassemble;  // Objects that are disassembled
    public GameObject[] partsDisassembled;   // Objects that appear after disassembling (e.g., slug on the table)

    private int currentStep = 0;  // Track which step we are on

    void Start()
    {
        // Ensure only initial parts are active, the disassembled parts are inactive
        for (int i = 0; i < partsToDisassemble.Length; i++)
        {
            partsToDisassemble[i].SetActive(true);   // Initial parts are visible
            partsDisassembled[i].SetActive(false);   // Disassembled parts are hidden
        }
    }

    // Method to proceed to the next step of disassembly
    public void ClickOnPart(int partIndex)
    {
        if (partIndex == currentStep)
        {
            // Deactivate the clicked part
            partsToDisassemble[partIndex].SetActive(false);

            // Activate the corresponding disassembled part
            partsDisassembled[partIndex].SetActive(true);

            // Move to the next step
            currentStep++;
        }
    }

    // Optional: You can also reset the disassembly process
    public void ResetDisassembly()
    {
        currentStep = 0;

        for (int i = 0; i < partsToDisassemble.Length; i++)
        {
            partsToDisassemble[i].SetActive(true);   // Reset initial parts
            partsDisassembled[i].SetActive(false);   // Hide disassembled parts
        }
    }
}
