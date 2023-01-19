using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    //Attributes:
    public float velocity;
    public Vector3 direction;

    //Methods:
    private void FixedUpdate()
    {
        transform.position += direction *  velocity * Time.deltaTime;
    }
}
