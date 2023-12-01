using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;
namespace Anura
{
    public class Final : MonoBehaviour
    {
        public void Salir(string nombre)
        {
            SceneManager.LoadScene(nombre);

        }
    }
}
