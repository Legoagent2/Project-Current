using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JC.FDG.InputManager;

namespace JC.FDG.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance;

        public Transform playerUnits;
        public Transform enemyUnits;

        public Transform playerBuildings;

        private void Awake()
        {
            instance = this;
            SetBasicStats(playerUnits);
            SetBasicStats(enemyUnits);
            SetBasicStats(playerBuildings);
        }

        private void Update()
        {
            InputHandler.instance.HandleUnitMovement();
        }

        public void SetBasicStats(Transform type)
        {
            foreach (Transform child in type)
            {
                foreach (Transform tf in child)
                {
                    //Transform pUnits = PlayerManager.instance.playerUnits;
                    //Transform eUnits = PlayerManager.instance.enemyUnits;
                    string name = child.name.Substring(0, child.name.Length - 1).ToLower();
                    var stats = Units.Unithandler.instance.GetBasicUnitStats(name);
                    if (type == playerUnits)
                    {
                        Units.Player.PlayerUnits pU = tf.GetComponent<Units.Player.PlayerUnits>();
                        pU.baseStats = Units.Unithandler.instance.GetBasicUnitStats(name);
                    }
                    else if (type == enemyUnits)
                    {
                        Units.Enemy.EnemyUnit eU = tf.GetComponent<Units.Enemy.EnemyUnit>();
                        eU.baseStats = Units.Unithandler.instance.GetBasicUnitStats(name);
                    }
                    else if(type == playerBuildings)
                    {
                        Buildings.Player.PlayerBuilding pB = tf.GetComponent<Buildings.Player.PlayerBuilding>();
                        pB.baseStats = Buildings.BuildingHandler.instance.GetBasicBuildingStats(name);
                    }
                    // add potential upgrades to unit stats
                }
            }
        }
    }
}