using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int _capacity;

    [SerializeField]
    private IPickeable[] _inventory;

    private IPickeable _currentEquiped;

    private int _ammountOfItems;

    [SerializeField]
    private GameEvent _gunPickedUp;

    [SerializeField]
    private GameEvent _healPickedUp;

    private void Awake()
    {
        _ammountOfItems = 0;
        _inventory = new IPickeable[_capacity];
    }

    // poner en el update lo de ir cambiando de item
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            if (Input.GetKeyDown(KeyCode.E)) //mover esto a un input controller
            {
                var item = other.GetComponent<IPickeable>();
                PutOnInventory(item);
                if (item.Id == 1) 
                {
                    _gunPickedUp.Raise();
                }
                else
                {
                    _healPickedUp.Raise();
                }
            } 
        }
    }

    private void PutOnInventory(IPickeable item) 
    {
        _inventory[_ammountOfItems] = item;
        Debug.Log(_inventory[_ammountOfItems]);
        _ammountOfItems++;
    }
}
