using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FelineFrenzy.Game
{
    public class Boundary : MonoBehaviour
    {
        //Attributes:
        private PlayerController player;

        //Methods:
        private void Start() => player = FindObjectOfType<PlayerController>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent<PlayerController>(out PlayerController player))
            {
                Core.GameManager.Singleton.OnPlayerExit(player);
            }
        }
    }
}