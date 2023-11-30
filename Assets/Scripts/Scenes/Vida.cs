using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class Vida : MonoBehaviour
{
    [SerializeField] private GameObject GameOver;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    public Image Health;
    public int Life = 4;
    // Start is called before the first frame update
    public void Start()
    {
        Time.timeScale = 1f;
    }


    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            Life--;
            Health.sprite = spriteArray[0];
        }
        switch (Life)
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
                Time.timeScale = 0f;
                GameOver.SetActive(true);
                break;
            default:
                Life = 4;
                break;
        }

    }
}
