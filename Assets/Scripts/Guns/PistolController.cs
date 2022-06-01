using UnityEngine;

public class PistolController : MonoBehaviour, IGun
{
    [SerializeField]
    private Gun _gunData;

    private void Start()
    {
        GunData = _gunData;
    }

    public Gun GunData { get => _gunData; set => _gunData = value; }

    public GameObject GetItem => gameObject;

    public (Vector3,Vector3) GetOkRotationAndPosition() 
    {
        // first rotation, then position
        return (new Vector3(0f, 14f, 75f), new Vector3(0.0068f, 0.0166f, 0.0433f));
    }

    public void GetPickedUp()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.SetActive(false);
    }
}
