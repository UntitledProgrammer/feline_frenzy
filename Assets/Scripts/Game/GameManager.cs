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
        public Vector2 spawnPoint;
        public List<Collection> chunks = new List<Collection>();
        private Collection previous, current;
        [SerializeField] private Transform playerTransform;
        private const float tolerance = 8.0f;
        private const float delay = 2.0f;

        [Header("Main Settings")]
        private uint health;
        private float timeActive;
        private static GameManager singleton;
        private uint attempts = 0;
        private const uint maxAttempts = 2;
        private Vector2 previousPosition;

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
            //playerTransform = FindObjectOfType<Issue.PlayerController>().transform;

            if (playerTransform == null) Application.Quit();
            current = LoadNextChunk();
            playerTransform.position = current.SpawnPoint;

        }

        private float Distance(Vector2 goal, Vector2 target)
        {
            return goal.x - target.x;
        }

        private Collection LoadNextChunk()
        {
            Collection temp = Instantiate(chunks[Random.Range(0, chunks.Count)]).GetComponent<Collection>();

            if(current == null)
            {
                temp.Left = (Vector3)spawnPoint + transform.position;
            }
            else
            {
                temp.Left = current.Right;
            }

            temp.transform.SetParent(transform);
            current = temp;

            return temp;
        }

        private void LateUpdate()
        {
            timeActive += Time.deltaTime;

            //Load next chunk?
            if (previous != null && Distance(current.Left, playerTransform.position) < -1.0f) { Destroy(previous.gameObject); }
            if (Distance(current.Right, playerTransform.position) > current.extent/1.5f) return;

            //Player has passed flag point.
            previous = current;
            current = LoadNextChunk();
        }

        public void OnPlayerExit(Issue.PlayerController controller)
        {
            /*
            //If the player has reached the max attempts permitted, return to the main menu.
            if(++attempts >= maxAttempts) { Debug.Log("Game End"); return; }

            if (current != null) Destroy(current);
            current = origin;
            origin.gameObject.SetActive(true);
            */

            playerTransform.position = current.SpawnPoint;
        }

        public void RespawnPlayer()
        {
            if (playerTransform == null) return;

            playerTransform.position = current.SpawnPoint;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere((Vector2)transform.position + spawnPoint, 1.0f);
            //Gizmos.DrawWireCube()
        }
    }
}