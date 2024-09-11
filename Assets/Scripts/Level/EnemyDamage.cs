using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    public PlayerHealth playerHealth;

    private void OnCollisionEnter2D(Collision2D collison) {
        if(collison.gameObject.tag == "Player") {
            playerHealth.Damage(damage);
        }
    }

    void OnEnable() {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }
}
