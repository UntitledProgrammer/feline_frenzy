using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Inventory
{
    [CreateAssetMenu(fileName = "Appearance", menuName = "Appearance", order = 0)]
    public class Appearance : ScriptableObject
    {
        //Attributes:
        public Sprite[] components = new Sprite[(int)State.LENGTH];

        //Constructor:
        public Appearance() => components = new Sprite[(int)State.LENGTH];

        //Methods:
        public Sprite Get(State state) => components[(int)state];

        //Operators:
        public Sprite this[int key]
        {
            get => components[key];
            set => components[key] = value;
        }
    }
}