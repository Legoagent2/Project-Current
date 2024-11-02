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
        private BasicUnit worker, warrior;

        public LayerMask pUnitLayer, eUnitLayer;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            pUnitLayer = LayerMask.NameToLayer("Interactables");
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
                    unit = warrior;
                    break;
                default:
                    Debug.Log($"Unit Type: {type} could not be found or doesn't exist.");
                    return null;
            }
            return unit.baseStats;
        }
    }
}