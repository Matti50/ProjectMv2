using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    [SerializeField]
    private Transform _shootPosition;

    [SerializeField]
    private int _experiencePerShot;

    private IGun _currentGun;

    private float _timeToShoot;

    private GameManager _gameManager;

    private IGun[] _inventory;
    private int _maxAmmountOfItems;
    private int _currentAmmountOfItems;

    private int _totalBullets;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        _currentAmmountOfItems = 0;
        _maxAmmountOfItems = 4;
        _inventory = new IGun[_maxAmmountOfItems];
        _experiencePerShot = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _currentGun != null && _timeToShoot <= Time.time)
        {
            _timeToShoot = Time.time + _currentGun.GunData.Recoil;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_totalBullets == 0) return;

        UIController.Instance.SetBullets(--_totalBullets);
        var hit = Physics.Raycast(_shootPosition.transform.position, _shootPosition.transform.forward,out var rayCastInfo,_currentGun.GunData.ShotDistance);
        
        if(hit && rayCastInfo.collider.gameObject.layer == (int)Layers.Zombie)
        {
            var lifeController = rayCastInfo.collider?.gameObject?.GetComponent<LifeController>();
            if (lifeController != null)
            {
                _gameManager.AddExperience(_experiencePerShot);
                lifeController.TakeDamage(_currentGun.GunData.ShootDamage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int)Layers.Gun)
        {
            if (_currentAmmountOfItems < _maxAmmountOfItems)
            {
                var pickedGun = other.gameObject.GetComponent<IGun>();
                _inventory[_currentAmmountOfItems] = pickedGun;
                _currentAmmountOfItems++;
                
                if (_currentGun == null)
                {
                    _currentGun = pickedGun;
                    _totalBullets = _currentGun.GunData.BulletsInMagazine;
                    UIController.Instance.SetBullets(_totalBullets);
                    Instantiate(pickedGun.GunData.GunPrefab, _shootPosition.position, _shootPosition.rotation, _shootPosition);
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
