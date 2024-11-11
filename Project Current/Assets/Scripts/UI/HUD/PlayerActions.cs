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
<<<<<<< HEAD
        public List<Units.BasicUnit> basicUnits = new List<Units.BasicUnit>();// all units than can be spawned
=======
        public List<Units.BasicUnit> basicUnits = new List<Units.BasicUnit>();// units that can be spawned
>>>>>>> f245704ba9ee64f7d17982df66e802ea633dd1ad

        [Space(15)]
        [Header("Buildings")]
        [Space(5)]
<<<<<<< HEAD
        public List<Buildings.BasicBuilding> basicBuildings = new List<Buildings.BasicBuilding>();// all units than can be spawned
=======
        public List<Buildings.BasicBuilding> basicBuildings = new List<Buildings.BasicBuilding>();// buildings that can be spawned
>>>>>>> f245704ba9ee64f7d17982df66e802ea633dd1ad
    }
}