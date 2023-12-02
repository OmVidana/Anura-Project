using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class CambioPer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    public Image Character;
    // Start is called before the first frame update
    public void Start()
    {

    }


    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Character.sprite == spriteArray[0])
            {
                Character.sprite = spriteArray[1];
            } else
            {
                Character.sprite = spriteArray[0];
            }
        }
    }
}
