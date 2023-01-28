using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Core
{
    public class SpawnCollection : Collection
    {
        //Attributes:
        public Vector2 spawnPoint;
        
        //Properties:
        public Vector2 SpawnPoint { get => spawnPoint + (Vector2)transform.position; }

        //Methods:
        public override void Disable()
        {
            gameObject.SetActive(false);
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();

            //Draw spawn point;
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(SpawnPoint, radius);
        }
    }
}