using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JC.FDG.Units
{
    public class UnitStatTypes : ScriptableObject
    {
        [System.Serializable]
        public class Base
        {
            public float cost, aggroRange, atkRange, atkSpeed, attack, health, armor;
            public bool canMine;
        }
    }
}