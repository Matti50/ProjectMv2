using UnityEngine;

public class PistolController : MonoBehaviour, IGun
{
    private AudioSource _audioSource;

    [SerializeField]
    private Gun _gunData;

    [SerializeField]
    private GameEvent _pistolShootEvent;

    [SerializeField]
    private UIEvent _updateUIBullets;

    [SerializeField]
    private AudioClip _fire;

    [SerializeField]
    private AudioClip _noBullets;

    [SerializeField]
    private AudioClip _reload;

    private int _currentBullets;

    private int _totalBullets = 24;

    private float _counterToShoot;

    private float _timeToShoot;

    void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        _counterToShoot = Time.time;
        _timeToShoot = _gunData.Recoil;
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
        if (_currentBullets == 0)
        {
            _audioSource.clip = _noBullets;
            _audioSource.Play();
            return;
        }

        if (_counterToShoot > Time.time) return;


        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray originRay = Camera.main.ScreenPointToRay(screenCenterPoint);

        _audioSource.clip = _fire;
        _audioSource.Play();
        _pistolShootEvent.Raise();
        _currentBullets--;
        _updateUIBullets.Raise(new BulletsUIParam { CurrentBullets = _currentBullets, TotalBullets = _totalBullets });
        _counterToShoot = Time.time + _timeToShoot;
        if (Physics.Raycast(originRay, out RaycastHit hitInfo, 50f))
        {

            if(hitInfo.collider.gameObject.layer == 12)
            {
                //make the zombie get damage
                //call the zombie damage animation
            }
        }
    }

    public void Reload()
    {
        if (_totalBullets == 0 || !gameObject.activeSelf) return;

        var toReload = GunData.BulletsInMagazine - _currentBullets;

        var toActuallyReload = Mathf.Min(toReload, _totalBullets);

        _currentBullets += toActuallyReload;

        _totalBullets -= toActuallyReload;

        _audioSource.clip = _reload;
        _audioSource.Play();
        _updateUIBullets.Raise(new BulletsUIParam{CurrentBullets = _currentBullets, TotalBullets = _totalBullets});
    }

    public string GetName()
    {
        return GunData.Name;
    } 

    public int TotalBullets()
    {
        return _totalBullets;
    }

    public int CurrentBullets()
    {
        return _currentBullets;
    }
}
