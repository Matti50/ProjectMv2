using UnityEngine;

public class GunPicker : MonoBehaviour
{

    [SerializeField]
    private GameObject _gunPosition;


    
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var gun = other.gameObject.GetComponent<IGun>();

                // Put gun in inventory
                //_gunPickedUp.Raise();
            }
        }
    }

    private void EquipWeapon(IGun gun)
    {
        //esto creo que podria mejorarse mejorando el sistema de eventos
        var createdGun = Instantiate(gun.GunData.GunPrefab, _gunPosition.transform.position, _gunPosition.transform.rotation, _gunPosition.transform);

        //for pistol only

    }
}
