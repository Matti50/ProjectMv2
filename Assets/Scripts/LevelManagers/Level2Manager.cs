using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    private int _zombieCount;

    private int _zombiesKilled = 0;

    [SerializeField]
    private GameEvent _ableToWin;

    private ZombieSpawner _zombieSpawner;

    private void Awake()
    {
        _zombieSpawner = GetComponent<ZombieSpawner>();
        _zombieCount = _zombieSpawner.AmmountOfZombies();
    }

    private void Start()
    {
        if (!GameManager.GameStarted())
            GameManager.StartGame();
        _zombieSpawner.SpawnZombies();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnZombieKilled()
    {
        _zombiesKilled++;

        if(_zombiesKilled == _zombieCount)
        {
            _ableToWin.Raise();
        }
    }
    
}
