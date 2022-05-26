using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _possibleZombieLocations;

    private List<Transform> _alreadyPickedLocations;

    [SerializeField]
    private int _maxAmmountOfZombiesToSpawn;

    [SerializeField]
    private GameObject _zombiePrefab;

    [SerializeField]
    private Dictionary<int, GameObject> _zombies;

    private void Start()
    {
        _zombies = new Dictionary<int, GameObject>();
        _zombies.Add(0, _zombiePrefab);
        _alreadyPickedLocations = new List<Transform>();
    }

    void Update()
    {
        for(int i = 0; i < _maxAmmountOfZombiesToSpawn; i++)
        {
            Transform randomLocation = _possibleZombieLocations[Random.Range(0, _maxAmmountOfZombiesToSpawn)];

            while (_alreadyPickedLocations.Contains(randomLocation))
            {
                randomLocation = _possibleZombieLocations[Random.Range(0, _maxAmmountOfZombiesToSpawn)];
            }

            _alreadyPickedLocations.Add(randomLocation);

            GameObject randomZombie = _zombies[Random.Range(0,_zombies.Keys.Count)];

            Instantiate(randomZombie, randomLocation.position, randomLocation.rotation);
        }
        Destroy(this);
    }
}
