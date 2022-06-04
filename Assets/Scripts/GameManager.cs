using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    
    public bool _dontDestroyOnLoad = true;

    [SerializeField]
    private KeyCode _reloadButton;

    [SerializeField]
    private GameEvent _reloadButtonPressed;

    [SerializeField]
    private Mission _mission;

    //will implement kind of observer pattern to easily pause the game

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

    private void Update()
    {
        if (Input.GetKeyDown(_reloadButton)) _reloadButtonPressed.Raise();
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
