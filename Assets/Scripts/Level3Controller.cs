using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Controller : MonoBehaviour
{
    private int _zombieCount = 4;

    private int _zombiesKilled = 0;

    public void OnZombieKilled()
    {
        _zombiesKilled++;
        if(_zombieCount == _zombiesKilled)
        {
            SceneManager.LoadScene("End");
        }
    }
}
