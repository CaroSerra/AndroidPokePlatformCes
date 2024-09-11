using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class ItemControl : MonoBehaviour
{
    //Clase con la que se accede a la API
    public ApiCall api;

    public GameObject item;
    public GameObject panel;
    public GameObject loading;

    void Start()
    {
        MakeRequests();
    }

    void MakeRequests() {
        int loaded = 0;
        string uri = "https://pokeapi.co/api/v2/pokemon/?limit=30";

        StartCoroutine(api.Request(uri, Callback));

        void Callback(Dictionary<string, dynamic> data)
        {
            // api.LogDictionaryParameters(data);
            /*foreach (var pokemon in data["results"])
            {
                Debug.Log(pokemon["name"]);
            }*/
            
            RectTransform panelRT = panel.GetComponent<RectTransform>();
            int items = 0;

            foreach(var pokemon in data["results"]) {
                loaded++;
                if (loaded > 28) loading.SetActive(false);
                string uri = pokemon["url"];
                // string uri = "";
                // if (pokemon.Key != "url") continue;
                // uri = pokemon.Key;

                /*yield return */StartCoroutine( api.Request(uri, PokemonCallback ));


                void PokemonCallback(Dictionary<string, dynamic> data) {
                    if (data["name"] == null) return;

                    GameObject newItem = Instantiate(item, panel.transform);
                    RectTransform newItemRT = newItem.GetComponent<RectTransform>();

                    /*if (items >= 3) */panelRT.offsetMin = new Vector2(panelRT.offsetMin.x, -201f * items);


                    if (items > 0) {
                        newItemRT.localPosition = new Vector3(16.2f, (207f -201f * items), 0);
                    }

                    else {
                        newItemRT.localPosition = new Vector3(16.2f, 207f, 0);
                    }

                    foreach(Transform child in newItem.transform) {
                        if (child.name == "NombrePoke") {
                            TMP_Text pokeName = child.GetComponent<TMP_Text>();
                            pokeName.text = data["name"];
                        }

                        else if (child.name == "TipoPoke") {
                            TMP_Text pokeType = child.GetComponent<TMP_Text>();
                            //var modifiedData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>[]>(data["types"]);
                            pokeType.text = data["types"][0]["type"]["name"];
                        }
                    }

                    newItemRT.sizeDelta = new Vector2(-28.3f, -690);
                    newItemRT.localScale = new Vector3(1.15f, 1.15f, 1.15f);

                    items++;
                }
                
            }
        }
    
    }
}
