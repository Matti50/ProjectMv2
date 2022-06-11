using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Controller : MonoBehaviour
{
    private int _zombiesKilled = 0;

    private int _zombieCount;

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
        if(_zombieCount == _zombiesKilled)
        {
            SceneManager.LoadScene("End");
        }
    }
}
