using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JC.FDG.Units.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerUnits : MonoBehaviour
    {
        private NavMeshAgent navAgent;

        public UnitStatTypes.Base baseStats;

        public void OnEnable()
        {
            navAgent = GetComponent<NavMeshAgent>();
        }


        public void MoveUnit(Vector3 _destination)
        {
            navAgent.SetDestination(_destination);
        }
    }
}

