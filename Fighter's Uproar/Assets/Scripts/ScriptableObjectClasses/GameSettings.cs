using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class GameSettings : ScriptableObject
{

    [Header("Game")]
    public int roundTime;
    public int numberOfRounds;

    [Header("Audio")]
    public bool musicEnabled;
    public bool soundEnabled;

    [Header("Characters")]
    public string playerOneCharacter;
    public string playerTwoCharacter;
}