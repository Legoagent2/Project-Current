using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JC.FDG.Units
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "New Unit/Basic")]
    public class BasicUnit : ScriptableObject
    {
        public enum unitType
        {
            Worker,
            Warrior,
            Healer
        };

        public bool isPlayerUnit;

        public unitType type;

        public new string name;

        public GameObject playerPrefab;
        public GameObject enemyPrefab;

        public int cost;
        public int attack;
        public int health;
        public int armor;
    }
}