using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Inventory
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory", order = 0)]
    public class Inventory : ScriptableObject
    {
        //Attributes:
        public List<Appearance> appearances;
        private string m_name;
        public uint balance;

        //Properties:
        public uint Balance { get => balance; }

        //Methods:
        public bool Spend(uint amount)
        {
            if (amount > balance) return false;
            balance -= amount;
            return true;
        }

        public void Add(uint amount) => balance += amount;
    }
}