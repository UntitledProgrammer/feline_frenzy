using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Core
{
    /**
    *   The chunk loader handles the continual, random progression of a level.
    * 
    *   @owner Thomas Jacobs | S212046.
    *   @project Feline Frenzy.
    */
    public class ChunkLoader : MonoBehaviour
    {
        //Attributes:
        public Chunk start;
        public Transform player_transform;
        public List<Chunk> chunks = new List<Chunk>();
        private const float tolerance = 1.0f;
        private const float delay = 8.0f;

        //Methods:
        private float Distance(Vector2 a, Vector2 b)
        {
            a.y = 0.0f;
            b.y = 0.0f;

            return Vector2.Distance(a, b);
        }

        private Chunk NextChunk()
        {
            return chunks[Random.Range(0, chunks.Count)];
        }

        private void Update()
        {
            if (Distance(start.FlagPosition, player_transform.position) > tolerance) return;

            //Player has passed flag...
            Chunk temp = GameObject.Instantiate(NextChunk().gameObject).GetComponent<Chunk>();
            temp.transform.position = start.Right + Vector2.right * temp.extent;
            temp.transform.SetParent(transform);

            Destroy(start.gameObject, delay);

            start = temp;
        }
    }
}