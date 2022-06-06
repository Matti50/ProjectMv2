using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningZoneController : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Debug.Log("hola");
            SceneManager.LoadScene("Yard");
        }
    }
}
