using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : MonoBehaviour
{
    //Attributes:
    public KeyCode startKey;
    public List<GameObject> targets;

    //Methods:
    private void Awake() => SetActive(false);
    private void Update() { if (Input.GetKeyDown(startKey)) { SetActive(true); Destroy(this); } }
    private void SetActive(bool state) { for (int i = 0; i < targets.Count; i++) { targets[i].SetActive(state); } }
}
