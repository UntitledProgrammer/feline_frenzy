using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  A simple script for following a target transform.
 *  The script targets a transform to make it usable for any gameObject
 *  since all gameObjects must have a transform.
 * 
 *  @owner Thomas Jacobs.
 *  @project Feline Frenzy.
 *  @date 10/3/23.
 */
public class Follow : MonoBehaviour
{
    //Attributes:
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 offset;
    [SerializeField] private Vector2 axis;
    [SerializeField] private float speed;
    private const float TOLERANCE = 0.1f;

    //Properties:
    public Transform Target { get => target; }
    private Vector3 TargetPosition { get => target == null ? transform.position : target.transform.position + (Vector3)offset; }

    //Methods:
    private Vector3 EraseDepth(Vector2 vector) => new Vector3(vector.x * axis.x, vector.y * axis.y, default);

    private void Awake()
    {
        if (target == null) { Debug.LogError("Player could not be located."); Destroy(this); }
    }

    public void LateUpdate()
    {
        if (Vector2.Distance(transform.position, TargetPosition) <= TOLERANCE) return;

        transform.position += speed * Time.deltaTime * (EraseDepth((TargetPosition - transform.position).normalized));
    }

    public void Centre()
    {
        if (target == null) return;
        transform.position = new Vector3(TargetPosition.x - transform.position.x, TargetPosition.y - transform.position.y, default) + (Vector3)offset;
    }

    public void SetOffset(Vector2 offset) => this.offset = offset;
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
    }

    public override void OnInspectorGUI()
    {
        if (UnityEngine.GUILayout.Button("Centre")) { self.Centre(); }

        if(UnityEngine.GUILayout.Button("Set Offset") && self.Target != null) self.SetOffset(self.transform.position - self.Target.position);
        DrawDefaultInspector();
    }
}
#endif