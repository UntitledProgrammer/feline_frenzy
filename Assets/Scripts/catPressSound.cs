using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource),typeof(Animator))]
public class catPressSound : MonoBehaviour
{
    [Range(0.0f, 1.0f)] public float volume;
    private AudioSource audioCat;
    private Animator animCatMenu;
    public Rect bounds;
    public AudioClip catClip;
    private const int LEFT = 0;

    // Start is called before the first frame update
    void Awake ()
    {
        audioCat = GetComponent<AudioSource>();
        animCatMenu = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bounds.Contains(Input.mousePosition) && Input.GetMouseButtonDown(LEFT)) audioCat.PlayOneShot(catClip, volume); 
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, bounds.size);
    }
}
