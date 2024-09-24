using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JC.FDG.Player; 

namespace JC.FDG.Units
{
    public class Unithandler : MonoBehaviour
    {
        public static Unithandler instance;

        [SerializeField] 
        private BasicUnit worker, warrior, healer;

        public LayerMask pUnitLayer, eUnitLayer;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            pUnitLayer = LayerMask.NameToLayer("PlayerUnits");
            eUnitLayer = LayerMask.NameToLayer("EnemyUnits");
        }

        public UnitStatTypes.Base GetBasicUnitStats(string type)
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
                    return null;
            }
            return unit.baseStats;
        }

        public void SetBasicUnitStats(Transform type)
        {
            Transform pUnits = PlayerManager.instance.playerUnits;
            Transform eUnits = PlayerManager.instance.enemyUnits;
            foreach (Transform child in type) 
            {
                foreach (Transform unit in child)
                {
                    string unitName = child.name.Substring(0, child.name.Length - 1).ToLower();
                    var stats = GetBasicUnitStats(unitName);
                    if (type == pUnits)
                    {
                        Player.PlayerUnits pU = unit.GetComponent<Player.PlayerUnits>();
                        pU.baseStats = GetBasicUnitStats(unitName);
                    } 
                    else if (type == eUnits) 
                    {
                        Enemy.EnemyUnit eU = unit.GetComponent<Enemy.EnemyUnit>();
                        eU.baseStats = GetBasicUnitStats(unitName);
                    }
                    // add potential upgrades to unit stats
                }
            }
        }
    }
}