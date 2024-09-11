using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    //Función que carga la siguiente escena
    public void CargarEscena () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
