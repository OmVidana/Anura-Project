using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

namespace Anura
{
    public class GameOver : MonoBehaviour
    {
        public void Salir(string nombre)
        {
            SceneManager.LoadScene(nombre);

        }

        public void Reiniciar()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
