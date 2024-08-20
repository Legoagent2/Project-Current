using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JC.FDG.InputManager;

namespace JC.FDG.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance;
        void Start()
        {
            instance = this;
        }

        void Update()
        {
            InputHandler.instance.HandleUnitMovement();
        }
    }
}