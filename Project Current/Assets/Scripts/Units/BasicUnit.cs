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

        [Header("Unit Settings")]
        [Space(15)]
        public bool isPlayerUnit;

        public unitType type;

        public new string name;

        public GameObject playerPrefab;
        public GameObject enemyPrefab;
        public GameObject icon;
        public float spawnTime;

        [Space(15)]
        [Header("Unit Stats")]
        [Space(40)]
        public UnitStatTypes.Base baseStats;
    }
}