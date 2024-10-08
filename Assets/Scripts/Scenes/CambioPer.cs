using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using Anura;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class CambioPer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerManager _playerManager;
    public List<Sprite> spriteList;
    public Image Character;

    // Update is called once per frame
    private void Update()
    {
        if (_playerManager._input.actions["Switch"].triggered && !_playerManager.onCooldownSwitch && _playerManager.activePlayer.GetComponent<Player>().IsGrounded() && !_playerManager.activePlayer.GetComponent<Player>().IsInsideTube())
        {
            spriteList.RemoveAt(0);
            spriteList.Add(Character.sprite);
            Character.sprite = spriteList[0];
        }
    }
}