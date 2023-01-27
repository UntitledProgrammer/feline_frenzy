using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Core
{
    [System.Serializable]
    public struct Perimeter
    {
        //Attributes:
        public Vector2 size;
        public Vector2 position;
        public LayerMask mask;
        public float rotation;

        //Properties:
        public bool Overlap { get => Physics2D.OverlapBox(position, size, rotation); }
        public Collider2D[] OverlapAll { get => Physics2D.OverlapBoxAll(position, size, rotation); }
    }

    public class GameManager : MonoBehaviour
    {
        //Attributes:
        [Header("Collection Settings")]
        public Collection startCollection;
        public List<Collection> chunks = new List<Collection>();
        private Transform playerTransform;
        private const float tolerance = 1.0f;
        private const float delay = 8.0f;

        [Header("Main Settings")]
        public Vector3 direction;
        public float velocity;
        private uint health;
        private float timeActive;
        private static GameManager singleton;
        private uint attempts = 0;
        private const uint maxAttempts = 2;

        //Properties:
        public static GameManager Singleton
        {
            get
            {
                if(singleton == null)
                {
                    Debug.LogError("Game Manager is Null.");
                    Application.Quit();
                }

                return singleton;
            }
        }

        //Methods:
        private void Awake()
        {
            singleton = this;
            playerTransform = FindObjectOfType<Controller2D>().transform;

            if (playerTransform == null) Application.Quit();
        }

        private float Distance(Vector2 a, Vector2 b)
        {
            a.y = 0.0f;
            b.y = 0.0f;

            return Vector2.Distance(a, b);
        }

        private Collection NextChunk()
        {
            return chunks[Random.Range(0, chunks.Count)];
        }

        private void LateUpdate()
        {
            timeActive += Time.deltaTime;

            //Load next chunk?
            if (Distance(startCollection.FlagPosition, playerTransform.position) > tolerance) return;

            //Player has passed flag point.
            Collection temp = GameObject.Instantiate(NextChunk().gameObject).GetComponent<Collection>();
            temp.Left = startCollection.Right;
            temp.transform.SetParent(transform);

            Destroy(startCollection.gameObject, delay);
            startCollection = temp;
        }

        private void FixedUpdate()
        {
            transform.position += direction * velocity * Time.deltaTime;
        }

        public void OnPlayerExit(Controller2D controller)
        {
            //If the player has reached the max attempts permitted, return to the main menu.
            if(++attempts >= maxAttempts) { Debug.Log("Game End"); return; }



        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            //Gizmos.DrawWireCube()
        }
    }
}