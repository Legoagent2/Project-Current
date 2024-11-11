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
            public float cost, aggroRange, atkRange, atkSpeed, attack, health, armor;// contains spawn crystal cost, object detection, attack range, attack cooldown, handles health, attack resistance, and damage output
            public bool canMine;//distinguishes between workers and warriors
        }
    }
}