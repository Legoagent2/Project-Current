using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JC.FDG.Units
{
    public class Unithandler : MonoBehaviour
    {
        public static Unithandler instance;
        [SerializeField] 
        BasicUnit worker, warrior, healer;

        private void Start()
        {
            instance = this;
        }

        public (int cost, int attack, int atkRange, int health, int armor) GetBasicUnitStats(string type)
        {
            BasicUnit unit;
            switch(type)
            {
                case "worker":
                    unit = worker;
                    break;
                case "warrior":
                    unit = worker;
                    break;
                case "healer":
                    unit = worker;
                    break;
                default:
                    Debug.Log($"Unit Type: {type} could not be found or doesn't exist.");
                    return (0, 0, 0, 0, 0);
            }
            return (unit.cost, unit.attack, unit.atkRange, unit.health, unit.armor);
        }

        public void SetBasicUnitStats(Transform type)
        {
            foreach (Transform child in type) 
            {
                foreach (Transform unit in child)
                {
                    string unitName = child.name.Substring(0, child.name.Length - 1).ToLower();
                    var stats = GetBasicUnitStats(unitName);
                    Player.PlayerUnits pU;
                    if (type == FDG.Player.PlayerManager.instance.playerUnits)
                    {
                        pU = unit.GetComponent<Player.PlayerUnits>();
                        // set unit stats in each unit
                        pU.cost = stats.cost;
                        pU.attack = stats.attack;
                        pU.atkRange = stats.atkRange;
                        pU.health = stats.health;
                        pU.armor = stats.armor;
                    } 
                    else if (type == FDG.Player.PlayerManager.instance.enemyUnits) 
                    {

                    }
                    // add potential upgrades to unit stats
                }
            }
        }
    }
}