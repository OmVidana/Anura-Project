using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Anura
{
    public class Finish : MonoBehaviour
    {
        [SerializeField] private GameObject GFinish;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Time.timeScale = 0f;
                GFinish.SetActive(true);
            }
        }
    }
}
