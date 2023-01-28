using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Core
{
    /**
    *  A collection is a group of gameObjects that collectively produce a segment of an overall level.
    * 
    *  @project Feline Frenzy.
    *  @owner Thomas Jacobs | S212046.
    */
    public class Collection : MonoBehaviour
    {
        //Attributes:
        public float extent;
        public Vector2 offset;
        public Vector2 spawnPoint;
        protected const float radius = 0.2f;
        private const float flag = 1.0f;
        private const float delay = 2.0f;

        //Properties:
        public Vector2 Centre { get => (Vector2)transform.position + offset; }
        public Vector2 Left
        {
            get => Centre + Vector2.left * extent;
            set => transform.position = value + (Vector2.right * extent) + (offset * -1);
        }

        public Vector2 Right
        {
            get => Centre + Vector2.right * extent;
            set => transform.position = value + (Vector2.left * extent) + offset;
        }

        public Vector2 SpawnPoint { get => spawnPoint + (Vector2)transform.position; }

        //Methods:
        public virtual void Disable()
        {
            Destroy(gameObject, delay);
        }

        protected virtual void OnDrawGizmosSelected()
        {
            //Centre:
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Centre, radius);

            //Left:
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(Left, radius);

            //Right:
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Right, radius);

            //Length:
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(Left, Right);

            //Spawn Point:
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(SpawnPoint, radius);
        }
    }
}