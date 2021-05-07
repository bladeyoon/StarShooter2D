﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _PowerUp;

    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private bool _stopSpawning = false;

    //spawn game objects every 3 seconds
    //create a coroutine of type IEnumerator -- yield events
    // while loop

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        // while loop (infinite loop)
           // instantiate enemy prefab
           // yield wait for 3 seconds
        while (_stopSpawning == false)
        {
            int xRandom = Random.Range(-5, 5);
            Vector3 randomPos = new Vector3(xRandom, 5, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, randomPos, Quaternion.identity);
            //transfer enemy to 'EnemyContainer' gameobject.
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while(_stopSpawning == false)
        {
            int Xrandom = Random.Range(-5, 5);
            Vector3 randomPos = new Vector3(Xrandom, 5, 0);
            Instantiate(_PowerUp, randomPos, Quaternion.identity);

            float randomSec = Random.Range(5f, 10f);
            yield return new WaitForSeconds(randomSec);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
