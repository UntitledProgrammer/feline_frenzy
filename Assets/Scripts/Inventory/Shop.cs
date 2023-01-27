using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Inventory
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Shop", order = 0)]
    public class Shop : ScriptableObject
    {
        //Attributes:
        public List<Appearance> appearances;
        private const uint price = 10;

        //Constructor:
        public Shop() => appearances = new List<Appearance>();

        //Methods:
        public Appearance Get(uint index)
        {
            Debug.Assert(index < appearances.Count);
            return appearances[(int)index];
        }

        public bool PurchaseRandom(Inventory inventory)
        {
            if(inventory.appearances.Count <= 0 || !inventory.Spend(price)) { return false; }
            inventory.appearances.Add(appearances[Random.Range(0, appearances.Count)]);
            return true;
        }
    }
}