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
        public NavMeshAgent navAgent;

        public UnitStatTypes.Base baseStats;

        public Collider[] rangeColliders;

        public Transform aggroTarget;

        public Enemy.EnemyUnit aggroUnit;

        public bool hasAggro = false;

        public float distance;

        public GameObject unitStatDisplay;

        public Image healthBarAmount;

        public float currentHealth;

        public float atkCooldown;

        public void Start()
        {
            navAgent = GetComponent<NavMeshAgent>();
            currentHealth = baseStats.health;
        }

        private void Update()
        {
            this.HandleHealth();
            atkCooldown -= Time.deltaTime;

            if (!hasAggro)
            {
                this.CheckForEnemyTargets();
            }
            else
            {
                this.Attack();
                this.MoveToAggroTarget();
            }
        }

        private void CheckForEnemyTargets()
        {
            rangeColliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange);

            for (int i = 0; i < rangeColliders.Length; i++)
            {
                if (rangeColliders[i].gameObject.layer == Unithandler.instance.eUnitLayer)
                {
                    aggroTarget = rangeColliders[i].gameObject.transform;
                    aggroUnit = aggroTarget.gameObject.GetComponent<Enemy.EnemyUnit>();
                    hasAggro = true;
                    break;
                }
            }
        }

        public void MoveUnit(Vector3 destination)
        {
            if (destination != null)
            {
                Debug.Log("Destination Set: " + destination);
                navAgent.SetDestination(destination);
            } else
            {
                Debug.Log("Destination unknown. Please try again.");
            }
        }

        private void Attack()
        {
            if (atkCooldown <= 0 && distance <= baseStats.atkRange + 1)
            {
                aggroUnit.TakeDamage(baseStats.attack);
                atkCooldown = baseStats.atkSpeed;
            }
        }

        public void TakeDamage(float damage)
        {
            float totalDamage = damage - baseStats.armor;
            currentHealth -= totalDamage;
        }

        private void MoveToAggroTarget()
        {
            if (aggroTarget == null)
            {
                navAgent.SetDestination(transform.position);
                hasAggro = false;
            }
            else
            {
                distance = Vector3.Distance(aggroTarget.position, transform.position);
                navAgent.stoppingDistance = (baseStats.atkRange + 1);

                if (distance <= baseStats.aggroRange)
                {
                    navAgent.SetDestination(aggroTarget.position);
                }
            }
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

