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
    public bool axisPositive;
    public bool axisNegative;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    public void Start()
    {
        ReloadButton();
    }

    public void StartRebinding()
    {
        rebindButton.SetActive(false);
        waitingForInput.SetActive(true);
        
        if (!axisPositive && !axisNegative)
        {
            rebindingOperation = inputAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .WithCancelingThrough("<Keyboard>/escape")
            .OnComplete(operation => RebindComplete(true))
            .OnCancel(operation => RebindComplete(false))
            .Start();
        }
        else
        {
            if (axisPositive)
            {
                rebindingOperation = inputAction.action.PerformInteractiveRebinding(2)
                .WithControlsExcluding("Mouse")
                .OnMatchWaitForAnother(0.1f)
                .WithCancelingThrough("<Keyboard>/escape")
                .OnComplete(operation => RebindComplete(true))
                .OnCancel(operation => RebindComplete(false))
                .Start();
            }
            else
            {
                rebindingOperation = inputAction.action.PerformInteractiveRebinding(1)
                .WithControlsExcluding("Mouse")
                .OnMatchWaitForAnother(0.1f)
                .WithCancelingThrough("<Keyboard>/escape")
                .OnComplete(operation => RebindComplete(true))
                .OnCancel(operation => RebindComplete(false))
                .Start();
            }
        }
    }

    private void RebindComplete(bool complete)
    {
        if (complete)
            ReloadButton();

        rebindingOperation.Dispose();

        rebindButton.SetActive(true);
        waitingForInput.SetActive(false);
    }

    public void ReloadButton()
    {
        if (axisPositive)
        {
            displayText.text = InputControlPath.ToHumanReadableString(
                inputAction.action.bindings[2].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);
        }
        else if (axisNegative)
        {
            displayText.text = InputControlPath.ToHumanReadableString(
                inputAction.action.bindings[1].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);
        }
        else
        {
            displayText.text = InputControlPath.ToHumanReadableString(
                inputAction.action.bindings[0].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);
        }
    }
}
