using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "SceneInfo", menuName = "SceneInfo", order = 0)]
public class SceneInfo : ScriptableObject
{
    //Attributes:
    public string sceneName;
    public bool unlocked;
    public float record;
    public float limit;

    //Properties:
    public string Name { get => sceneName; }
}