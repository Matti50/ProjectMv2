using UnityEngine;

public class Level2Manager : MonoBehaviour
{

    private int _zombieCount = 6;

    private int _zombiesKilled = 0;

    [SerializeField]
    private GameEvent _ableToWin;

    public void OnZombieKilled()
    {
        _zombiesKilled++;

        if(_zombiesKilled == _zombieCount)
        {
            _ableToWin.Raise();
        }
    }
    
}
