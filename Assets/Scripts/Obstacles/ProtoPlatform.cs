using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Obstacles
{
    [System.Serializable]
    public struct WorldRect
    {
        //Attributes:
        public Vector2 centre;
        public Vector2 extents;
        public LayerMask mask;
    }

    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class ProtoPlatform : MonoBehaviour
    {
        #region Attributes
        [SerializeField] private WorldRect triggerArea;
        [SerializeField] private KeyCode escapeKey;
        [SerializeField] private float delay;
        [SerializeField] private Vector2 end;
        private Vector2 start;
        private Vector2 targetOffset;
        public float time;
        private Rigidbody2D target;
        private Animator animator;
        private bool activated = false;
        private const float TOLERANCE = 1.0f;
        private RaycastHit2D hit;
        #endregion

        #region Properties
        public Vector2 StartPoint { get => transform.position; }
        public Vector2 EndPoint { get => transform.position + (Vector3)end; }
        #endregion

        #region Methods
        private void Awake()
        {
            time = default;
            start = transform.position;
            end += (Vector2)transform.position;
        }

        private void Update()
        {
            //Trigger logic.
            if (!activated)
            {
                hit = Physics2D.BoxCast(triggerArea.centre + (Vector2)transform.position, triggerArea.extents, 0.0f, Vector2.zero, 0.0f, triggerArea.mask);
                if (hit.collider != null) Activate(hit.collider.gameObject);
            }

            if (!activated) return;

            //If the platform reaches the end position.
            if(time/delay > TOLERANCE)
            {
                Deactivate();
                return;
            }

            //If the player tries to exit the platform.
            else if(Input.GetKeyDown(escapeKey)) { Deactivate(); return; }

            //Move platform according to time.
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, time / delay);
            if(target) target.transform.position = transform.position + (Vector3)targetOffset;
        }

        private void Deactivate()
        {
            if(target != null)
            {
                target.isKinematic = false;
                target = null;
            }
            if (animator) { animator.enabled = true; animator = null; }

            time = default;
            activated = false;

            Destroy(this);
        }

        private void Activate(GameObject collision)
        {
            if(collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D target))
            {
                this.target = target;
                target.isKinematic = true;
                activated = true;
                targetOffset = target.transform.position - transform.position;
                animator = target.GetComponent<Animator>();
                if(animator) animator.enabled = false;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(StartPoint, EndPoint);
            Gizmos.DrawWireSphere(StartPoint, TOLERANCE);
            Gizmos.DrawWireSphere(EndPoint, TOLERANCE);

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + (Vector3)triggerArea.centre, triggerArea.extents);
        }
        #endregion
    }
}