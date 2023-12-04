using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using Anura;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class Vida : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    public Image Health;
    private int _currentHealth;
    // Start is called before the first frame update
    private void Start()
    {
        _currentHealth = _playerManager.currentHealth;
    }


    // Update is called once per frame
    private void Update()
    {
        if (_currentHealth != _playerManager.currentHealth)
        {
            _currentHealth = _playerManager.currentHealth;
            int caseHealth = (int)Mathf.Floor(_currentHealth * 0.5f);
            switch (caseHealth)
            {
                case 4:
                    Health.sprite = spriteArray[0];
                    break;
                case 3:
                    Health.sprite = spriteArray[1];
                    break;
                case 2:
                    Health.sprite = spriteArray[2];
                    break;
                case 1:
                    Health.sprite = spriteArray[3];
                    break;
                case 0:
                    Health.sprite = spriteArray[4];
                    break;
            }
        }
    }
}