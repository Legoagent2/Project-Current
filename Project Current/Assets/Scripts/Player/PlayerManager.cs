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

        private void Awake()
        {
            instance = this;
            Units.Unithandler.instance.SetBasicUnitStats(playerUnits);
            Units.Unithandler.instance.SetBasicUnitStats(enemyUnits);
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            InputHandler.instance.HandleUnitMovement();
        }
    }
}