using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class asteroidmanager : MonoBehaviour
{
    [SerializeField] private asteroid _asteroidPrefab;
    [SerializeField] private GameObject _pickupPrefab;
    [SerializeField] private int _asteroidsPerAxis = 10;
    [SerializeField] private int _gridSpacing = 10;

    private List<asteroid> _asteroidList = new List<asteroid>();

    private void OnEnable()
    {
        GameManager.OnStartGame += PlaceAsteroids;
        GameManager.OnRespawnPickup += SpawnPickup;
        GameManager.OnPlayerDestroyed += DestroyAsteroids;
    }

    private void OnDisable()
    {
        GameManager.OnStartGame -= PlaceAsteroids;
        GameManager.OnRespawnPickup -= SpawnPickup;
        GameManager.OnPlayerDestroyed -= DestroyAsteroids;
    }

    private void PlaceAsteroids()
    {
        for (var x = 0; x < _asteroidsPerAxis; x++)
        {
            for (var y = 0; y < _asteroidsPerAxis; y++)
            {
                for (var z = 0; z < _asteroidsPerAxis; z++)
                {
                    var xPos = transform.position.x + (x * _gridSpacing) + RandomGridOffset();
                    var yPos = transform.position.y + (y * _gridSpacing) + RandomGridOffset();
                    var zPos = transform.position.z + (z * _gridSpacing) + RandomGridOffset();
                    var name = string.Format("Asteroid ({0},{1},{2})", x, y, z);
                    SpawnAsteroid(name, new Vector3(xPos, yPos, zPos));
                }
            }
        }
        SpawnPickup();
    }

    private float RandomGridOffset()
    {
        return Random.Range(-_gridSpacing / 2f, _gridSpacing / 2f);
    }

    private void SpawnAsteroid(string name, Vector3 asteroidPosition)
    {
        var spawnedAsteroid = Instantiate(_asteroidPrefab, asteroidPosition, Quaternion.identity, transform);
        spawnedAsteroid.name = name;
        _asteroidList.Add(spawnedAsteroid);
    }

    private void SpawnPickup()
    {
        var randomAsteroid = _asteroidList[Random.Range(0, _asteroidList.Count)];
        var spawnedPickup = Instantiate(_pickupPrefab, randomAsteroid.transform.position, Quaternion.identity, transform);
        spawnedPickup.name = randomAsteroid.name.Replace("Asteroid", "Pickup");
        _asteroidList.Remove(randomAsteroid);
        Destroy(randomAsteroid.gameObject);
    }


    private void DestroyAsteroids()
    {
        foreach (var asteroid in _asteroidList)
        {
            asteroid.SelfDestruct();
        }
        _asteroidList.Clear();
    }
}
