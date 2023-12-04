using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class HealingItem : MonoBehaviour
    {
        public int healAmount;
        public float respawnTime;
        public HealingItemsManager manager;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerManager playerManager = other.gameObject.GetComponent<Player>().playerManager;
                if (playerManager.currentHealth < playerManager.playersHealth)
                {
                    playerManager.currentHealth = Math.Min(playerManager.currentHealth + healAmount,
                        playerManager.playersHealth);

                    gameObject.SetActive(false);
                    manager.StartCoroutine(manager.RespawnHealing(respawnTime, gameObject));
                }
            }
        }
    }
}
