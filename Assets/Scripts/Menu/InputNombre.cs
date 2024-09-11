using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MongoPokePlatform;

public class InputNombre : MonoBehaviour
{
    public TMP_InputField inputNombre;
    public Button jugar;

    public void CrearJugador()
    {
        Db.CreatePlayer(inputNombre.text);
        StaticData.playerName = inputNombre.text;
    }

    //Comprobación nombre válido
    public void BotonActivo() {
        jugar.interactable = (inputNombre.text.Length != 0 && inputNombre.text.Length < 8);
    }

    
}
