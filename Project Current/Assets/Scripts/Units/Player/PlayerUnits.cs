using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace JC.FDG.Units.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerUnits : MonoBehaviour
    {
        private NavMeshAgent navAgent;

        public UnitStatTypes.Base baseStats;

        public GameObject unitStatDisplay;

        public Image healthBarAmount;

        public float currentHealth;

        public void OnEnable()
        {
            navAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            currentHealth = baseStats.health;
        }

        private void Update()
        {
            HandleHealth();
        }

        public void MoveUnit(Vector3 _destination)
        {
            navAgent.SetDestination(_destination);
        }

        public void TakeDamage(float damage)
        {
            float totalDamage = damage - baseStats.armor;
            currentHealth -= totalDamage;
        }

        private void HandleHealth()
        {
            Camera camera = Camera.main;
            unitStatDisplay.transform.LookAt(unitStatDisplay.transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
            healthBarAmount.fillAmount = currentHealth / baseStats.health;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            InputManager.InputHandler.instance.selectedUnits.Remove(gameObject.transform);
            Destroy(gameObject);
        }
    }
}

