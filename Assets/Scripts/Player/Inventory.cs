using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int _capacity;

    [SerializeField]
    private Transform _itemPosition;

    [SerializeField]
    private UIEvent _objectEquipedEvent;

    private IPickeable[] _inventory;

    private GameObject _currentEquiped;

    private int _ammountOfItems;

    private void Awake()
    {
        _ammountOfItems = 0;
        _inventory = new IPickeable[_capacity];
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            var ok = int.TryParse(Input.inputString, out int key);
            if (ok)
            {
                UnequipItem();
                Equip(key);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            if (Input.GetKeyDown(KeyCode.E)) //mover esto a un input controller como action button pressed, posiblemente un evento
            {
                if (!AbleToPick()) return;

                var item = other.GetComponent<IPickeable>();
                PutOnInventory(item);
                item.GetPickedUp();
            }
        }
    }

    private void PutOnInventory(IPickeable item)
    {
        _inventory[_ammountOfItems] = item;
        _ammountOfItems++;
    }

    private void PutItemOnHandPosition(IPickeable item)
    {
        _currentEquiped = item.GetItem;
        _currentEquiped.transform.parent = _itemPosition;
        _currentEquiped.transform.position = _itemPosition.position;

        (Vector3 correctRotation, Vector3 correctPosition) = item.GetOkRotationAndPosition();
        _currentEquiped.transform.localEulerAngles = correctRotation;
        _currentEquiped.transform.localPosition = correctPosition;

        if (isGunEquiped())
        {
            _objectEquipedEvent.Raise(new UiElementEquipedParam(item.GetImage(),0,12)); //cambiar al del arma
        }
        else
        {
            _objectEquipedEvent.Raise(new UiElementEquipedParam(item.GetImage(), null, null));
        }
        

        _currentEquiped.SetActive(true);
    }

    private void UnequipItem()
    {
        if (_currentEquiped != null)
        {
            _currentEquiped.SetActive(false);
            _currentEquiped.transform.parent = null;
        }
    }

    private void Equip(int position)
    {
        if (position == 0 || _inventory[position - 1] == null) 
        {
            _objectEquipedEvent.Raise(null);
            _currentEquiped = null;
            return;
        } 

        PutItemOnHandPosition(_inventory[position - 1]);
    }

    private bool AbleToPick() 
    {
        return _ammountOfItems < _capacity;
    }

    public bool isGunEquiped()
    {
        bool isSomethingEquiped = _currentEquiped != null;

        if (isSomethingEquiped)
        {
            var isGun = _currentEquiped.TryGetComponent(out IGun component); //TODO: intentar optimizar
            return isGun;
        }
        else 
        { 
            return false; 
        }
    }

    public IGun GetEquipedGun()
    {
        if (!isGunEquiped()) return null;

        return _currentEquiped.GetComponent<IGun>();
    }
}
