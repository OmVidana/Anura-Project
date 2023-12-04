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
        
        public GameObject anura;
        public GameObject uri;
        private Player _active;
        private Player _disable;
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
            _active = _activePlayer.GetComponent<Player>();
            _disablePlayer = uri;
            _disable = _disablePlayer.GetComponent<Player>();
        }

        void Start()
        {
            _activePlayer.SetActive(true);
            _disablePlayer.SetActive(false);
        }

        private void Update()
        {
            if (_input.actions["Switch"].triggered && !_onCooldownSwitch && _activePlayer.GetComponent<Player>().IsGrounded())
            {
                _disablePlayer.transform.position = _activePlayer.transform.position + new Vector3(0, 0.5f,0);
                _disable.spriteRenderer.flipX = _active.spriteRenderer.flipX;
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
            (_active, _disable) = (_disable, _active);
            (_activePlayer, _disablePlayer) = (_disablePlayer, _activePlayer);
            yield return new WaitForSeconds(1.0f);
            _onCooldownSwitch = false;
        }
    }
}