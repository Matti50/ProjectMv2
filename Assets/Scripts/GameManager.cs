using UnityEngine;
using UnityEditor;
using Assets.Scripts;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    
    public bool _dontDestroyOnLoad = true;

    private int _experience;

    private bool _reachToLevel1ScapePoint;

    [SerializeField]
    private Mission _mission;

    private GameStates _gameState = GameStates.Playing;
    //will implement kind of observer pattern to easily pause the game

    private void Awake()
    {
        if(_instance == null)
        {
            _experience = 0;
            _reachToLevel1ScapePoint = false;
            _instance = this;

            if (_dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        //check game paused
        //if finished level1 go to the next level
    }

    public void AddExperience(int expPoints)
    {
        _experience += expPoints;
        Debug.Log(_experience);
    }

    public void FinishedLevel1()
    {
        Debug.Log("Finished level 1");
        _reachToLevel1ScapePoint = true;
    }

    public void QuitGame()
    {
        if (Application.isEditor)
        {
            EditorApplication.ExitPlaymode();
        }
        else
        {
            Application.Quit();
        }
    }

    public string GetMissionDescription()
    {
        return _mission.Description;
    }
}
