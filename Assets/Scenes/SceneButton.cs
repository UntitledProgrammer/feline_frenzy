using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneButton : Button
{
    //Attributes:
    public SceneInfo sceneInfo;

    //Methods:
    private void LoadScene()
    {
        Debug.Log("Click");

        if (sceneInfo == null || !sceneInfo.unlocked) return;

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneInfo.Name);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        onClick.AddListener(LoadScene);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SceneButton))]
public class SceneButtonEditor : Editor
{
    //Attributes:
    private SceneButton self;

    //Methods:
    private void OnEnable() => self = (SceneButton)target;

    public override void OnInspectorGUI()
    {
        self.sceneInfo = (SceneInfo)EditorGUILayout.ObjectField(self.sceneInfo, typeof(SceneInfo));
        DrawDefaultInspector();
    }
}
#endif