using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MongoPokePlatform;

public class PlayerHealth : MonoBehaviour
{
 public int maxHealth = 3;
 public int health;
 public GameObject[] Hearts;
 public PlayerMovement playerM;

 void Start() {
    health = maxHealth;
 }

 public void Damage(int damage) {
    health -= damage;
    Hearts[health].SetActive(false);
    if (health <= 0) {
        Destroy(gameObject);
        StaticData.score = playerM.score;
        Db.SavePlayerScore(StaticData.playerName, playerM.score);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
 }
}
