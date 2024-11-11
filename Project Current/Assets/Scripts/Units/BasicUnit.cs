using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JC.FDG.Units
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "New Unit/Basic")]
    public class BasicUnit : ScriptableObject//holds all editable stats for units
    {
        public enum unitType//types of units
        {
            Worker,
            Warrior
        };

        [Header("Unit Settings")]
        [Space(15)]
        public bool isPlayerUnit;

        public unitType type;

        public new string name;

        public GameObject playerPrefab;//instantiated model and functionality
        public GameObject enemyPrefab;//what will be recognised as an enemy
        public GameObject icon;//HUD icon for being spawned
        public float spawnTime;// delay for being spawned

        [Space(15)]
        [Header("Unit Stats")]
        [Space(40)]
        public UnitStatTypes.Base baseStats;//get unit stats
    }
}