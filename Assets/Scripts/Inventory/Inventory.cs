using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Inventory
{
    public class Inventory : ScriptableObject
    {
        //Attributes:
        private uint balance;

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