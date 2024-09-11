using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigSpawner : MonoBehaviour
{
    public PlayerHealth player;
    public GameObject pig;
    public float secBetweenSpawn = 8f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine( SummonPigs() );
    }

    IEnumerator SummonPigs () {
        yield return new WaitForSeconds(secBetweenSpawn);
        float x = Random.Range(-4f, 27f);
        float y = Random.Range(-1f, 16f);

        var newPig = Instantiate(pig, new Vector3(x, y, 0f), Quaternion.Euler(0f, 0f, 0f));
        newPig.GetComponent<EnemyDamage>().playerHealth = player;
        StartCoroutine( SummonPigs() );
    }
}
