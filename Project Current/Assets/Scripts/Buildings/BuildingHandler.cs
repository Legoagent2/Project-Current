using UnityEngine;

namespace JC.FDG.Buildings
{
    public class BuildingHandler : MonoBehaviour
    {
        public static BuildingHandler instance;

        [SerializeField]
        private BasicBuilding Barracks;

        private void Awake()
        {
            instance = this;
        }

        public BuildingStatTypes.Base GetBasicBuildingStats(string type)
        {
            BasicBuilding building;
            switch (type)
            {
                case "barracks":
                    building = Barracks;
                    break;
                default:
                    Debug.Log($"Unit Type: {type} could not be found or doesn't exist.");
                    return null;
            }
            return building.baseStats;
        }
    }
}
