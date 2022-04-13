using UnityEngine;
using UnityEngine.InputSystem;

public class RebindSaveLoad : MonoBehaviour
{
    public InputActionAsset actions;

    private Object[] rebindDisplays;
    private InputActionMap playerOneBinds, playerTwoBinds;

    public void OnEnable()
    {
        rebindDisplays = Object.FindObjectsOfType<RebindDisplay>();

        playerOneBinds = actions.FindActionMap("PlayerOne");
        playerTwoBinds = actions.FindActionMap("PlayerTwo");

        var rebindsP1 = PlayerPrefs.GetString("rebindsP1");
        var rebindsP2 = PlayerPrefs.GetString("rebindsP2");

        if (!string.IsNullOrEmpty(rebindsP1))
            playerOneBinds.LoadBindingOverridesFromJson(rebindsP1);

        if (!string.IsNullOrEmpty(rebindsP2))
            playerTwoBinds.LoadBindingOverridesFromJson(rebindsP2);

    }

    public void OnDisable()
    {
        var rebindsP1 = playerOneBinds.SaveBindingOverridesAsJson();
        var rebindsP2 = playerTwoBinds.SaveBindingOverridesAsJson();

        PlayerPrefs.SetString("rebindsP1", rebindsP1);
        PlayerPrefs.SetString("rebindsP2", rebindsP2);
    }

    public void ResetControls()
    {
        playerOneBinds.RemoveAllBindingOverrides();
        playerTwoBinds.RemoveAllBindingOverrides();

        foreach (RebindDisplay r in rebindDisplays)
        {
            r.ReloadButton();
        }
    }
}
