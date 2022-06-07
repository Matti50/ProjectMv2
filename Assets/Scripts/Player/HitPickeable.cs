using UnityEngine;

public class HitPickeable : MonoBehaviour
{

    [SerializeField]
    private Inventory _inventory; 

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 11)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _inventory.PickUpItem(other.GetComponent<IPickeable>());
            }
        }
    }
}
