using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptables/GameSettings", order = 2)]

public class GameSettings : ScriptableObject
{
    [SerializeField] internal float time = 180;
    //[SerializeField] internal int imageCount = 36;
}
