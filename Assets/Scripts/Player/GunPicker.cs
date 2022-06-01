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
        createdGun.transform.localEulerAngles = new Vector3(0f, 14f, 75f);
        createdGun.transform.localPosition = new Vector3(0.0068f, 0.0166f, 0.0433f);

    }
}
