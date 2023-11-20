using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Anura
{
    public class PlayerManager : MonoBehaviour
    {
        public int playersHealth;
        private Queue<GameObject> _playerPool;
        public GameObject anura;
        public GameObject uri;
        private GameObject _activePlayer;
        private GameObject _disablePlayer;
        private bool _onCooldownSwitch;
        private PlayerInput _input;

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();

            _playerPool = new Queue<GameObject>();
            _playerPool.Enqueue(anura);
            _playerPool.Enqueue(uri);

            _activePlayer = Instantiate(_playerPool.Dequeue(), transform.position, Quaternion.identity, transform);
            _disablePlayer = Instantiate(_playerPool.Dequeue(), transform.position, _activePlayer.transform.rotation, transform);
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
                _disablePlayer.transform.position = _activePlayer.transform.position + new Vector3(0, anura.transform.localScale.y * 0.5f, 0);
                _disablePlayer.transform.rotation = _activePlayer.transform.rotation;
                StartCoroutine(SwitchActivePlayer());
            }
        }

        IEnumerator SwitchActivePlayer()
        {
            _onCooldownSwitch = true;
            _activePlayer.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _disablePlayer.SetActive(true);
            (_activePlayer, _disablePlayer) = (_disablePlayer, _activePlayer);
            yield return new WaitForSeconds(1.0f);
            _onCooldownSwitch = false;
        }
    }
}