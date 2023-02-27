using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    //Attributes:
    public Vector2 offset;
    public Transform target;

    //Methods:
    private void Awake()
    {
        target = FindObjectOfType<Issue.PlayerController>().transform;
        if (target == null) { Debug.LogError("Player could not be located."); Destroy(this); }
    }

    public void Update()
    {
        if (target == null) { Debug.LogError("Player could not be located."); Destroy(this); }
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z) + (Vector3)offset;
    }

    public void Centre()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z) + (Vector3)offset;
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(Follow))]
public class FollowEditor : UnityEditor.Editor
{
    //Attributes:
    private Follow self;

    //Methods:
    private void OnEnable()
    {
        self = (Follow)target;
        if(self.target == null) self.target = FindObjectOfType<Issue.PlayerController>().transform;
    }

    public override void OnInspectorGUI()
    {
        if (UnityEngine.GUILayout.Button("Centre")) { self.Centre(); }
        DrawDefaultInspector();
    }
}
#endif