using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Obstacles
{
    [RequireComponent(typeof(Collider2D))]
    public class MeshTrigger2D : MonoBehaviour
    {
        //Attributes:
        public UnityEngine.Events.UnityEvent onEnter;
        public UnityEngine.Events.UnityEvent onExit;
        private Collider2D m_collider;

        //Methods:
        private void Awake()
        {
            m_collider = GetComponent<Collider2D>();
            m_collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision) => onEnter.Invoke();

        private void OnTriggerExit2D(Collider2D collision) => onExit.Invoke();
    }
}