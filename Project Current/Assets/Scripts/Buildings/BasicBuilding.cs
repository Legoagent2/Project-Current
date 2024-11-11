using UnityEngine;
using System.Collections.Generic;

namespace JC.FDG.Buildings
{
    [CreateAssetMenu(fileName = "Building", menuName = "New Building/Basic")]
    public class BasicBuilding : ScriptableObject//holds all editable stats for buildings
    {
        public enum buildingType
        {
            HQ,//types of buildings
            Barracks
        }

        [Space(15)]
        [Header("Building Settings")]

        public buildingType type;
        public new string name;//name of building
        public GameObject buildingPrefab;//Instantiated model
        public GameObject icon;// icon for spawning
        public float spawnTime;// spawn time if being spawned

        [Space(15)]
        [Header("Building Base Stats")]
        [Space(40)]

        public BuildingStatTypes.Base baseStats;//get stats of buildings
    }
}