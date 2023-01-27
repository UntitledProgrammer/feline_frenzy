using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelineFrenzy.Game
{
    struct PlayerRunTimeStatistics
    {
        //Attributes:
        public uint attempts;
        public float timeActive;
    }

    struct PlayerCoreStatistics
    {
        //Attributes:
        public uint overallAttempts;
        public float overallTimeActive;
        public int accountBalance;
    }
}