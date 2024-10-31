using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JC.FDG.Interactables
{
    public class Crystal : MonoBehaviour
    {
        public void TakeDamage(float damage)
        {
            float totalDamage = damage - baseStats.armor;
            currentHealth -= totalDamage;
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}