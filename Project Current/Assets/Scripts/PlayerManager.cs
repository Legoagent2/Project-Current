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

        void Start()
        {
            instance = this;
            Units.Unithandler.instance.SetBasicUnitStats(playerUnits);
            //Units.UnitHandler.instance.SetBasicUnitStats(enemyUnits);
        }

        void Update()
        {
            InputHandler.instance.HandleUnitMovement();
        }
    }
}