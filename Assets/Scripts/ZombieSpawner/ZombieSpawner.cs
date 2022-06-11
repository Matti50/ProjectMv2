using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _zombiePrefab;

    [SerializeField]
    private int _ammountOfZombiesToSpawn;

    [SerializeField]
    private List<Transform> _zombieLocations;

    [SerializeField]
    private Quaternion _faceRotation;

    [SerializeField]
    private bool _zombiesOn;

    public void SpawnZombies()
    {
        if (_ammountOfZombiesToSpawn != _zombieLocations.Count)
        {
            Debug.LogError("La cantidad de zombies debe ser igual a la cantidad de ubicaciones en la lista");
        }
        else
        {
            for(var i = 0; i < _ammountOfZombiesToSpawn; i++)
            {
                var zombie = Instantiate(_zombiePrefab, _zombieLocations.ElementAt(i).position, _faceRotation);
                zombie.GetComponent<ChasePlayerController>().SetId(Random.Range(30, 1000));
                if (_zombiesOn)
                    zombie.GetComponent<PlayerDetection>().Enable();
            }
        }
    }

    public int AmmountOfZombies()
    {
        return _ammountOfZombiesToSpawn;
    }
}
