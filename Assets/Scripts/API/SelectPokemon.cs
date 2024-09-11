using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectPokemon : MonoBehaviour
{
    public new TMP_Text name;
    public CargarEscena sceneLoad;

    public void Choose() {
        StaticData.pokeNombre = name.text;
        sceneLoad.PasarAEscena();
    }
}
