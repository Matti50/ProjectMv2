using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2WinningZone : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            GameManager.ChangeLevelKey("ThirdLevel");
            SceneManager.LoadScene("House");
        }
    }
}
