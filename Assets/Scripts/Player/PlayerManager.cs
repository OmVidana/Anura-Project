using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Anura
{
    public class PlayerManager : MonoBehaviour
    {
        [Range(0, 100)] public int playersHealth;
        public int currentHealth;
        
        public GameObject anura;
        public GameObject uri;
        private GameObject _activePlayer;
        private GameObject _disablePlayer;
        private bool _onCooldownSwitch;
        public PlayerInput _input;
        public CinemachineVirtualCamera vc;

        public LayerMask enemyLayer;
        private bool _attackOnCooldown;
        
        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _activePlayer = anura;
            _disablePlayer = uri;
            currentHealth = playersHealth;
        }

        void Start()
        {
            _activePlayer.SetActive(true);
            _disablePlayer.SetActive(false);
        }

        private void Update()
        {
            if (_input.actions["Switch"].triggered && !_onCooldownSwitch && _activePlayer.GetComponent<Player>().IsGrounded() && !_activePlayer.GetComponent<Player>().attackOnCooldown && !_activePlayer.GetComponent<Player>().hitOnCooldown)
            {
                _disablePlayer.transform.position = _activePlayer.transform.position + new Vector3(0, 0.5f,0);
                _disablePlayer.GetComponent<Player>().spriteRenderer.flipX = _activePlayer.GetComponent<Player>().spriteRenderer.flipX;
                StartCoroutine(SwitchActivePlayer());
            }
        }

        IEnumerator SwitchActivePlayer()
        {
            _onCooldownSwitch = true;
            _activePlayer.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _disablePlayer.SetActive(true);
            vc.Follow = _disablePlayer.transform;
            (_activePlayer, _disablePlayer) = (_disablePlayer, _activePlayer);
            yield return new WaitForSeconds(1.0f);
            _onCooldownSwitch = false;
        }

        public void Death()
        {
            _activePlayer.SetActive(false);
            GameObject.Find("DamageIndicator").SetActive(false);
            // Load Scene
        }
    }
}