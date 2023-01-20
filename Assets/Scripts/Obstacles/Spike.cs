using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Obstacles
{
    [RequireComponent(typeof(Animator))]
    public class Spike : MonoBehaviour
    {
        //Attributes:
        private Animator animator;
        private const string key = "Close";
        private const float min = 0.0f, max = 2.0f, animation_delay = 2.0f;

        //Properties:
        float RandomTime { get => Random.Range(min, max); }

        //Methods:
        private void Start()
        {
            animator = GetComponent<Animator>();
            StartCoroutine(Trigger(RandomTime));
        }

        private IEnumerator Trigger(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            animator.SetTrigger(key);

            yield return StartCoroutine(Trigger(RandomTime + animation_delay));
        }
    }
}
