using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowPokemonName : MonoBehaviour
{
    public new TMP_Text name;
    // Start is called before the first frame update
    void Start()
    {
        name.text = StaticData.pokeNombre;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
