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
        protected internal GameObject activePlayer;
        private GameObject _disablePlayer;
        protected internal bool onCooldownSwitch;
        private bool _insideTube;
        public PlayerInput _input;
        public CinemachineVirtualCamera vc;
        [SerializeField] private GameObject _gameOver;
        
        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            activePlayer = anura;
            _disablePlayer = uri;
            currentHealth = playersHealth;
        }

        void Start()
        {
            activePlayer.SetActive(true);
            _disablePlayer.SetActive(false);
        }

        private void Update()
        {
            if (_input.actions["Switch"].triggered && !onCooldownSwitch && activePlayer.GetComponent<Player>().IsGrounded() && !activePlayer.GetComponent<Player>().IsInsideTube())
            {
                _disablePlayer.transform.position = activePlayer.transform.position + new Vector3(0, 0.5f, 0);
                _disablePlayer.GetComponent<Player>().spriteRenderer.flipX = activePlayer.GetComponent<Player>().spriteRenderer.flipX;
                StartCoroutine(SwitchActivePlayer());
            }
        }

        IEnumerator SwitchActivePlayer()
        {
            onCooldownSwitch = true;
            activePlayer.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _disablePlayer.SetActive(true);
            vc.Follow = _disablePlayer.transform;
            (activePlayer, _disablePlayer) = (_disablePlayer, activePlayer);
            yield return new WaitForSeconds(1.0f);
            onCooldownSwitch = false;
        }

        public void Death()
        {
            activePlayer.SetActive(false);
            GameObject.Find("DamageIndicator").SetActive(false);
            _gameOver.SetActive(true);
        }
    }
}