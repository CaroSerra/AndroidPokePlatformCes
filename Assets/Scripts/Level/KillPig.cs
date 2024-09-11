using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillPig : MonoBehaviour
{
    public PlayerMovement playerM;
    public TMP_Text scoretext;

    void OnTriggerEnter2D(Collider2D collison) {
        if(collison.gameObject.tag == "KillPig") {
            Destroy(collison.transform.parent.gameObject);
            playerM.score += 100;
            scoretext.text = playerM.score.ToString();
        }
    }
}
