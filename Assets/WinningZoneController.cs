using UnityEngine;

public class WinningZoneController : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Debug.Log("hola");
            //take to next level
        }
    }
}
