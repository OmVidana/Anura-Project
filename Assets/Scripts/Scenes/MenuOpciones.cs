using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Audio;


public class MenuOpciones : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public void PantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void CambiarVolume(float volumen)
    {
        audioMixer.SetFloat("Volumen", volumen);
    }

    public void CambiarCalidad(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}
