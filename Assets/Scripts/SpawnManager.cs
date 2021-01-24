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
    private GameObject _powerupContainer;

    private bool _stopSpawning = false;

    [SerializeField]
    private GameObject[] powerups;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(6.0f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, powerups.Length);
            GameObject newEnemy = Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(Random.RandomRange(3, 8));
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.RandomRange(1, 4));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
