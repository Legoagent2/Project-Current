using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JC.FDG.Interactables
{
    public class Crystal : MonoBehaviour
    {
        public ResourceHandler resources;

        public int energyGiveAmount;

        public float currentHealth = 999;

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            resources.crystalAmount += energyGiveAmount;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("CrystalDead");
            Destroy(gameObject);
        }
    }
}