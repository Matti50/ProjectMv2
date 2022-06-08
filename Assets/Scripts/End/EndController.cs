using UnityEngine;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKey) SceneManager.LoadScene("MainMenu");        
    }
}
