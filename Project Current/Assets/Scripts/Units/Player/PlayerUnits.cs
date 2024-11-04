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

        //public ResourceHandler resources;

        public Collider[] rangeColliders;// objects that can be detected

        public Transform aggroTarget;

        public Enemy.EnemyUnit aggroUnit;//detected enemies

        public Interactables.Crystal aggroObject;// detected crystal

        public bool hasAggro = false;

        public float distance;

        public GameObject unitStatDisplay;

        public Image healthBarAmount;

        public float currentHealth;

        public float atkCooldown;

        private bool deathCall;

        public bool isMoving;

        public void Start()// set navigation and health on startup
        {
            navAgent = GetComponent<NavMeshAgent>();
            currentHealth = baseStats.health;
        }

        private void Update()// run functions per frame
        {
            this.HandleHealth();
            atkCooldown -= Time.deltaTime;

            if (!hasAggro)
            {
                this.CheckForEnemyTargets();// look for enemy units
            }
            else
            {
                if(!isMoving)
                {
                    this.Attack();// attack and approach enemy targets
                    this.MoveToAggroTarget();
                }
            }
        }

        private void CheckForEnemyTargets()//detect enemy units within detection range
        {
            rangeColliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange);

            for (int i = 0; i < rangeColliders.Length; i++)
            {
                if (rangeColliders[i].gameObject.layer == Unithandler.instance.eUnitLayer)
                {
                    aggroTarget = rangeColliders[i].gameObject.transform;
                    aggroUnit = aggroTarget.gameObject.GetComponent<Enemy.EnemyUnit>();
                    aggroObject = aggroTarget.gameObject.GetComponent<Interactables.Crystal>();
                    hasAggro = true;
                    break;
                }
            }
        }

        public void MoveUnit(Vector3 destination)// move unit to the destination selected on the map
        {
            isMoving = true;
            Debug.Log(isMoving);
            if (destination != null)
            {
                Debug.Log("Destination Set: " + destination);
                navAgent.SetDestination(destination);
            } else
            {
                Debug.Log("Destination unknown. Please try again.");
            }
            StartCoroutine(waitForRetry());
        }

        IEnumerator waitForRetry()//if theres a new location against a detected object, wait 3 seconds before going back
        {
            yield return new WaitForSeconds(3);
            isMoving = false;
        }

        private void Attack()// remove health from the enemy or mine the crystal based on what object's detected
        {
            if (atkCooldown <= 0 && distance <= baseStats.atkRange + 1 && aggroUnit != null)
            {
                aggroUnit.TakeDamage(baseStats.attack);
                atkCooldown = baseStats.atkSpeed;
            }
            else
            {
                if (atkCooldown <= 0 && distance <= baseStats.atkRange + 1 && baseStats.canMine == true && aggroObject != null)
                {
                    Debug.Log("Mine the Crystals");
                    aggroObject.TakeDamage(baseStats.attack);
                    atkCooldown = baseStats.atkSpeed;
                }
            }
        }

        public void TakeDamage(float damage)// recieve damage based on armor and damage from the target
        {
            float totalDamage = damage - baseStats.armor;
            currentHealth -= totalDamage;
        }

        private void MoveToAggroTarget()// move to detected target
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

                if (distance <= baseStats.aggroRange && deathCall == false)
                {
                    navAgent.SetDestination(aggroTarget.position);
                }
            }
        }

        private void HandleHealth()// change the healthbar with each change in health
        {
            Camera camera = Camera.main;
            unitStatDisplay.transform.LookAt(unitStatDisplay.transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
            healthBarAmount.fillAmount = currentHealth / baseStats.health;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()//destroy upon health reaching 0
        {
            deathCall = true;
            InputManager.InputHandler.instance.selectedUnits.Remove(gameObject.transform);
            Destroy(gameObject);
        }
    }
}

