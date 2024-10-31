using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace JC.FDG.Units.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyUnit : MonoBehaviour
    {
        private NavMeshAgent navAgent;

        public UnitStatTypes.Base baseStats;

        private Collider[] rangeColliders;

        private Transform aggroTarget;

        private Player.PlayerUnits aggroUnit;

        private bool hasAggro = false;

        private float distance;

        public GameObject unitStatDisplay;

        public Image healthBarAmount;

        public float currentHealth;

        public float atkCooldown;

        private bool deathCall;

        private void Start()
        {
            navAgent = GetComponent<NavMeshAgent>();
            currentHealth = baseStats.health;
        }

        private void Update()
        {
            HandleHealth();
            atkCooldown -= Time.deltaTime;

            if (!hasAggro)
            {
                CheckForEnemyTargets();
            }
            else
            {
                Attack();
                MoveToAggroTarget();
            }
        }

        private void CheckForEnemyTargets()
        {
            rangeColliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange);

            for (int i = 0; i < rangeColliders.Length; i++)
            {
                if (rangeColliders[i].gameObject.layer == Unithandler.instance.pUnitLayer)
                {
                    aggroTarget = rangeColliders[i].gameObject.transform;
                    aggroUnit = aggroTarget.gameObject.GetComponent<Player.PlayerUnits>();
                    hasAggro = true;
                    break;
                }
            }
        }

        private void Attack()
        {
            if(atkCooldown <= 0 && distance <= baseStats.atkRange + 1)
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
                    if (deathCall == false)
                    {
                        navAgent.SetDestination(aggroTarget.position);
                    }
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
            deathCall = true;
            Destroy(gameObject);
        }
    }
}