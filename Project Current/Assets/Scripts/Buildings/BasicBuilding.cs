using UnityEngine;
using System.Collections.Generic;

namespace JC.FDG.Buildings
{
    [CreateAssetMenu(fileName = "Building", menuName = "New Building/Basic")]
    public class BasicBuilding : ScriptableObject
    {
        public enum buildingType
        {
            HQ,
            Barracks
        }

        [Space(15)]
        [Header("Building Settings")]

        public buildingType type;
        public new string name;
        public GameObject buildingPrefab;
        public GameObject icon;
        public List<Units.BasicUnit> spawnUnits;
        public float spawnTime;

        [Space(15)]
        [Header("Building Base Stats")]
        [Space(40)]

        public BuildingStatTypes.Base baseStats;
    }
}