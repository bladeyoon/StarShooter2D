using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {

    }

    //spawn game objects every 5 seconds
    //create a coroutine of type IEnumerator -- yield events
    // while loop

    IEnumerator SpawnEnemies()
    {
        //while loop (infinite loop)
        // instantiate enemy prefab
        // yield wait for 5 seconds
        while (true)
        {
            int xRandom = Random.Range(-5, 5);
            Vector3 randomPos = new Vector3(xRandom, 5, 0);
            Instantiate(enemyPrefab, randomPos, Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }
}
