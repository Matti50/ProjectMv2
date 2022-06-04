using UnityEngine;

public class PistolController : MonoBehaviour, IGun
{
    private AudioSource _audioSource;

    [SerializeField]
    private Gun _gunData;

    [SerializeField]
    private GameEvent _pistolShootEvent;

    private int _currentBullets;

    void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

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

    public Sprite GetImage()
    {
        return GunData.Image;
    }

    public void Shoot()
    {
        if (_currentBullets == 0) return;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray originRay = Camera.main.ScreenPointToRay(screenCenterPoint);

        _audioSource.Play();
        _pistolShootEvent.Raise();
        _currentBullets--;

        if (Physics.Raycast(originRay, out RaycastHit hitInfo, 50f, LayerMask.GetMask("Shooteable")))
        {
            //hit shooteables
        }
    }

    //reload

    public string GetName()
    {
        return GunData.Name;
    } 

    public int TotalBullets()
    {
        return GunData.BulletsInMagazine;
    }

    public int CurrentBullets()
    {
        return _currentBullets;
    }
}
