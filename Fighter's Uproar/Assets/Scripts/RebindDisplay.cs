using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class RebindDisplay : MonoBehaviour
{

    public InputActionReference inputAction;
    public GameObject rebindButton, waitingForInput;
    public TMP_Text displayText;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    public void StartRebinding()
    {
        rebindButton.SetActive(false);
        waitingForInput.SetActive(true);

        rebindingOperation = inputAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete())
            .Start();
    }

    private void RebindComplete()
    {
        int bindingIndex = inputAction.action.GetBindingIndexForControl(inputAction.action.controls[0]);
        displayText.text = InputControlPath.ToHumanReadableString(
            inputAction.action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        rebindingOperation.Dispose();

        rebindButton.SetActive(true);
        waitingForInput.SetActive(false);
    }
}
