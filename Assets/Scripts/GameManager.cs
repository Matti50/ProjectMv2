using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    
    public bool _dontDestroyOnLoad = true;

    public static void ChangeLevelKey(string key)
    {
        LevelKey = key;
    }

    [SerializeField]
    private KeyCode _reloadButton;

    [SerializeField]
    private KeyCode _hintButton;

    [SerializeField]
    private GameEvent _reloadButtonPressed;

    [SerializeField]
    private GameEvent _showHint;

    [SerializeField]
    private Mission _mission;

    [SerializeField]
    private UIEvent _loadFirstMission;

    private bool _gameStarted;

    private Dictionary<string, Mission> _firstMissions;

    private static string LevelKey;

    [SerializeField]
    private Mission _level1FirstMission;

    [SerializeField]
    private Mission _level2FirstMission;

    [SerializeField]
    private Mission _level3FirstMission;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            LevelKey = "FirstLevel";
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

    private void Start()
    {
        _firstMissions = new Dictionary<string, Mission>();
        _firstMissions.Add("FirstLevel", _level1FirstMission);
        _firstMissions.Add("SecondLevel", _level2FirstMission);
        _firstMissions.Add("ThirdLevel", _level3FirstMission);
        _loadFirstMission.Raise(new UIMissionChanged { MissionDescription = _firstMissions[LevelKey].Description });
    }

    private void Update()
    {
        if (!_gameStarted) return;

        if (Input.GetKeyDown(_reloadButton)) _reloadButtonPressed.Raise();

        if (Input.GetKeyDown(_hintButton)) _showHint.Raise();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }

    public void ChangeMission(Mission mission)
    {
        _loadFirstMission.Raise(new UIMissionChanged { MissionDescription = mission.Description });
    }

    public void StartGame()
    {
        _gameStarted = true;
    }
}
