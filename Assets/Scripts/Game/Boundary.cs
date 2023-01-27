using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FelineFrenzy.Game
{
    [System.Serializable]
    public struct Laser
    {
        //Attributes:
        public Vector2 direction;
        public float distance;

        //Constructors:
        public Laser(Vector2 direction, float distance) { this.direction = direction; this.distance = distance; }
    }

    public class Boundary : MonoBehaviour
    {
        //Attributes:
        public Laser[] directions;
        public LayerMask mask;

        //Methods:
        private void Start() { for (int i = 0; i < directions.Length; i++) directions[i].direction.Normalize(); }

        private void Update()
        {
            for (int i = 0; i < directions.Length; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i].direction, directions[i].distance, mask);

                if(hit.collider == null && TryGetComponent<Controller2D>(out Controller2D player))
                {
                   // Core.GameManager.Singleton
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < directions.Length; i++) { Gizmos.DrawLine(transform.position, (Vector2)transform.position + directions[i].direction * directions[i].distance); }
        }
    }
}