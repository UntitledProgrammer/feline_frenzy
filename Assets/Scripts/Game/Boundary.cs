using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FelineFrenzy.Game
{
    public class Boundary : MonoBehaviour
    {
        //Attributes:
        private Issue.PlayerController player;

        //Methods:
        private void Start() => player = FindObjectOfType<Issue.PlayerController>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent<Issue.PlayerController>(out Issue.PlayerController player))
            {
                Core.GameManager.Singleton.OnPlayerExit(player);
            }
        }
    }
}