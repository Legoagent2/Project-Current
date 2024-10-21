using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JC.FDG.UI.HUD
{
    [CreateAssetMenu(fileName = "NewPlayerActions", menuName = "PlayerActions")]

    public class PlayerActions : ScriptableObject
    {
        [Space(5)]
        [Header("Units")]
        public List<Units.BasicUnit> basicUnits = new List<Units.BasicUnit>();

        [Space(15)]
        [Header("Buildings")]
        [Space(5)]
        public List<Buildings.BasicBuilding> basicBuildings = new List<Buildings.BasicBuilding>();
    }
}