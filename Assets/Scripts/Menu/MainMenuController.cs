using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private MainMenuItem  _defaultMenu;

    private MainMenuItem _currentMenu;

    private void Awake()
    {
        var allMenus = GetComponentsInChildren<MainMenuItem>();

        foreach(var menu in allMenus)
        {
            menu.Close();
        }
    }

    private void Start()
    {
        if (_defaultMenu == null) return;
        _currentMenu = _defaultMenu;
        _currentMenu.Open();
    }

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName ?? SceneNames.LevelOne);
    }

    public void ChangeMenu(MainMenuItem menuItem)
    {
        if(_currentMenu != null)
        {
            _currentMenu.Close();

        }

        if(_currentMenu != menuItem)
        {
            _currentMenu = menuItem;
            _currentMenu.Open();
        }
        else
        {
            _currentMenu = null;
        }
    }
}
