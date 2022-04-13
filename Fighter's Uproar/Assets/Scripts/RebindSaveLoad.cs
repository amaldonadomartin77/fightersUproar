using UnityEngine;
using UnityEngine.InputSystem;

public class RebindSaveLoad : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions = null;

    private Object[] rebindDisplays;

    public void OnEnable()
    {
        rebindDisplays = Object.FindObjectsOfType<RebindDisplay>();

        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
            actions.LoadBindingOverridesFromJson(rebinds);

    }

    public void OnDisable()
    {
        var rebinds = actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
    }

    public void ResetControls()
    {
        actions.RemoveAllBindingOverrides();

        foreach (RebindDisplay r in rebindDisplays)
        {
            r.ReloadButton();
        }
    }
}
