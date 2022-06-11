using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    void Start()
    {
        if (!GameManager.GameStarted())
            GameManager.StartGame();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
