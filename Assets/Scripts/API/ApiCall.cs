using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class ApiCall : MonoBehaviour
{

    // Método usado para obtener datos de una API
    // Primer argumento: URI
    // Segundo argumento: Callback (referencia a método al que llamará cuando los datos lleguen)
    public IEnumerator Request (string uri, Action<Dictionary<string, dynamic>> callback) {
        using (UnityWebRequest req = UnityWebRequest.Get(uri)) {
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError) {
                Debug.Log("ApiCall: Can't establish a connection with the API. Internet connection may be unstable");
            }

            else {
                string text = req.downloadHandler.text;
                var csData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(text);
                callback(csData);
            }
        }
    }
/*
    public IEnumerator RequestString (string uri, Action<Dictionary<string, string>> callback) {
        using (UnityWebRequest req = UnityWebRequest.Get(uri)) {
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError) {
                Debug.Log("ApiCall: Can't establish a connection with the API. Internet connection may be unstable");
            }

            else {
                string text = req.downloadHandler.text;
                var csData = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);
                callback(csData);
            }
        }
    }

// Solicita un diccionario de diccionarios
    public IEnumerator RequestDictionary (string uri, Action<Dictionary<string, Dictionary<string, string>>> callback) {
        using (UnityWebRequest req = UnityWebRequest.Get(uri)) {
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError) {
                Debug.Log("ApiCall: Can't establish a connection with the API. Internet connection may be unstable");
            }

            else {
                string text = req.downloadHandler.text;
                var csData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(text);
                callback(csData);
            }
        }
    }
*/

    // Método usado para loggear todos los parámetros de un diccionario
    // Útil para el desarrollo
    public void LogDictionaryParameters (Dictionary<string, string> dict) {
        foreach (var parameter in dict) {
            Debug.Log($"Parámetro: {parameter}");
        }
    }
}
