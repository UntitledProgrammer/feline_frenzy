using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    //Attributes:
    private Transform target;

    //Methods:
    private void Awake()
    {
        target = FindObjectOfType<PlayerController>().transform;
        if (target == null) { Debug.LogError("Player could not be located."); Destroy(this); }
    }

    public void Update()
    {
        if (target == null) { Debug.LogError("Player could not be located."); Destroy(this); }
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
    }
}
