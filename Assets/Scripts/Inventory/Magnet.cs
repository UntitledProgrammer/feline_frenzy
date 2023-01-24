using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Interaction
{
    public class Magnet : MonoBehaviour
    {
        //Attributes:
        public FelineFrenzy.Inventory.Inventory inventory;
        public float radius;
        public float attraction;
        public LayerMask mask;
        private List<GameObject> targets;
        private const float tolerance = 0.2f;

        //Methods:
        private void Start()
        {
            if (inventory == null) Destroy(this);
            targets = new List<GameObject>();
        }

        private void Update()
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, Vector2.one, 0.0f, mask);
            if (hit.collider != null)
            {
                hit.collider.enabled = false;
                targets.Add(hit.collider.gameObject);
            }
        }

        private void LateUpdate()
        {
            if (targets.Count <= 0) return;

            for (int i = targets.Count - 1; i >= 0; i--)
            {
                Vector2 direction = (transform.position - targets[i].transform.position).normalized;
                targets[i].transform.position += Time.deltaTime * attraction * (Vector3)direction;

                if (Vector2.Distance(targets[i].transform.position, transform.position) <= tolerance)
                {
                    //Update inventory.
                    inventory.Add(targets[i].GetComponent<Coin>().value);

                    //Remove gameObject from scene.
                    GameObject temp = targets[i];
                    targets.RemoveAt(i);
                    Destroy(temp);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}