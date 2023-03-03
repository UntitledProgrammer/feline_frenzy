using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Obstacles
{
    [RequireComponent(typeof(Collider2D))]
    public class Spike : MonoBehaviour
    {
        //Methods:
        private void Awake() => GetComponent<Collider2D>().isTrigger = true;    

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent<Game.Respawnable>(out Game.Respawnable respawnable))
            {
                respawnable.Respawn();
            }
        }
    }
}
