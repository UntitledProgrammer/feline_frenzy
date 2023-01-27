using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Core
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

    public class Respawnable : MonoBehaviour
    {
        //Attributes:
        public Laser[] directions;
        public LayerMask mask;
        private Vector2 origin;

        //Methods:
        protected virtual void Awake() => origin = transform.position;
        public virtual void Respawn() => transform.position = origin;
    }

}