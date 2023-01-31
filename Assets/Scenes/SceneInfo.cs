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

    //Properties:
    public string Name { get => sceneName; }

    //Methods:
    public void Awake()
    {
        unlocked = false;
        record = 0.0f;
        sceneName = Selection.activeObject is SceneAsset ? ((SceneAsset)Selection.activeObject).name : "empty";
    }
}