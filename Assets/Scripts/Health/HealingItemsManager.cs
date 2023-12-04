using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class HealingItemsManager : MonoBehaviour
    {
        public IEnumerator RespawnHealing(float respawnTime, GameObject healingItem)
        {
            yield return new WaitForSeconds(respawnTime);
            healingItem.SetActive(true);
        }
    }
}
