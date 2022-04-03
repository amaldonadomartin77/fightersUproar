using UnityEngine;
using System.Collections;
// Attach this to a gameobject that exists in the initial scene
public class Settings : MonoBehaviour
{
    [Tooltip("Choose which GameSettings asset to use")]
    public GameSettings _settings; // drag GameSettings asset here in inspector
    [SerializeField]
    public static GameSettings s;
    public static Settings instance;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Settings.instance == null)
        {
            Settings.instance = this;
        }
        else
        {
            Debug.LogWarning("A previously awakened Settings MonoBehaviour exists!", gameObject);
        }
        if (Settings.s == null)
        {
            Settings.s = _settings;
        }
    }
}