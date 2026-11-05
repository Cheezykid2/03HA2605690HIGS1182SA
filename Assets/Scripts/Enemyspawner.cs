using UnityEngine;

public class Enemyspawner : MonoBehaviour 
{
    [SerializeField] private GameObject _enemyPrefab;
    [Tooltip("Time between spawns in seconds")][SerializeField] private float _spawnTime = 5f;

    private void OnEnable()
    {
        GameManager.OnStartGame += StartSpawning;
        GameManager.OnPlayerDestroyed += StopSpawning;
    }

    private void OnDisable()
    {
        GameManager.OnStartGame -= StartSpawning;
        GameManager.OnPlayerDestroyed += StopSpawning;
    }

    private void SpawnEnemy()
    {
        Instantiate(_enemyPrefab, transform.position, Quaternion.identity, transform);
    }

    private void StartSpawning()
    {
        InvokeRepeating("SpawnEnemy", _spawnTime, _spawnTime);
    }

    private void StopSpawning()
    {
        CancelInvoke();
    }
}
