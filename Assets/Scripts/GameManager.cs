using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    
    public bool _dontDestroyOnLoad = true;

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

    private void Awake()
    {
        if(_instance == null)
        {
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

    private void Start()
    {
        _loadFirstMission.Raise(new UIMissionChanged { MissionDescription = _mission.Description });
    }

    private void Update()
    {
        if (Input.GetKeyDown(_reloadButton)) _reloadButtonPressed.Raise();

        if (Input.GetKeyDown(_hintButton)) _showHint.Raise();
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
}
