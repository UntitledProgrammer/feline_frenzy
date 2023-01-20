using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Core
{
    /**
    *  A chunk is a collection of gameObjects that collectively produce a segment of an overall level.
    * 
    *  @project Feline Frenzy.
    *  @owner Thomas Jacobs | S212046.
    */
    public class Chunk : MonoBehaviour
    {
        //Attributes:
        public float flag;
        public float extent;
        public Vector2 pivot;
        private const float flag_length = 1.0f;

        //Properties:
        public Vector2 Left { get => ((Vector2)transform.position + pivot) + Vector2.left * extent; }
        public Vector2 FlagPosition { get => Left + Vector2.right * flag; }
        public float Length { get => extent + extent; }
        public Vector2 Right { get => ((Vector2)transform.position + pivot) + Vector2.right * extent; }
        public Vector2 Offset { get => (Vector2)transform.position + pivot; }
        public Vector2 Centre { get => ((Vector2)transform.position + pivot); }

        //Methods:
        public virtual void Spawn() { }
        public virtual void Despawn() { }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Left, Right);
            Gizmos.DrawLine(FlagPosition, FlagPosition + Vector2.up * flag_length);
        }
    }
}