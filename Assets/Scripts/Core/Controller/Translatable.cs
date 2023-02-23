using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Translatable : MonoBehaviour
    {
        //Attributes:
        private List<ITranslator> translators = new List<ITranslator>();
        protected Rigidbody2D m_rigidbody;

        //Methods:
        public virtual void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        public virtual void SubscribeTranslator(ITranslator translator)
        {
            if (translators.Contains(translator)) return;

            translators.Add(translator);
        }

        public virtual void RemoveTranslator(ITranslator translator)
        {
            if (!translators.Contains(translator)) return;

            translators.Remove(translator);
        }

        public virtual Vector3 ProcessTranslators()
        {
            Vector3 position = Vector3.zero;

            for (int i = 0; i < translators.Count; i++) translators[i].Process(m_rigidbody);

            return position;
        }
    }
}
