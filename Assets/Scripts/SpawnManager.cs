using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] _powerUps;

    [SerializeField]
    private bool _stopSpawning = false;

    public void EnemySpawnCoroutine()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (_stopSpawning == false)
        {
            int xRandom = Random.Range(-5, 5);
            Vector3 randomPos = new Vector3(xRandom, 5, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, randomPos, Quaternion.identity);
            //transfer enemy to 'EnemyContainer' gameobject.
            newEnemy.transform.parent = _enemyContainer.transform;

            float randomSec = Random.Range(1f, 5f);
            yield return new WaitForSeconds(randomSec);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (_stopSpawning == false)
        {
            int Xrandom = Random.Range(-5, 5);
            Vector3 randomPos = new Vector3(Xrandom, 5, 0);
            int randomPowerUps = Random.Range(0, 3);
            Instantiate(_powerUps[randomPowerUps], randomPos, Quaternion.identity);

            float randomSec = Random.Range(5f, 10f);
            yield return new WaitForSeconds(randomSec);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
