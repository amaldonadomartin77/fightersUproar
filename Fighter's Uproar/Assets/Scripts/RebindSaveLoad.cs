using UnityEngine;
using UnityEngine.InputSystem;

public class RebindSaveLoad : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions = null;

    private Object[] rebindDisplays;

    public void Start()
    {
        Load();
    }

    public void Save()
    {
        rebindDisplays = Object.FindObjectsOfType<RebindDisplay>();

        var controls = PlayerPrefs.GetString("controls");
        if (!string.IsNullOrEmpty(controls))
            actions.LoadBindingOverridesFromJson(controls);

    }

    public void Load()
    {
        var controls = actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("controls", controls);
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
