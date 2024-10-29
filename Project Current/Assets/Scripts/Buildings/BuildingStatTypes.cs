using UnityEngine;
using System.Collections.Generic;

namespace JC.FDG.Buildings
{
    public class BuildingStatTypes : ScriptableObject
    {
        [System.Serializable]
        public class Base
        {
            public float health, armor, attack;
            public UI.HUD.PlayerActions buildingActions;
        }
    }
}

