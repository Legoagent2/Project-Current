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

        public (int cost, int attack, int atkRange, int health, int armor) GetBasicUnit(string type)
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
    }
}