using UnityEngine;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("MainMenu");        
    }
}
