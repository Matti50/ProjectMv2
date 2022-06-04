using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class AimGun : MonoBehaviour
{
    private Animator _animator;
    private PlayerRotationController _rotationController;
    private Inventory _inventory;

    [SerializeField]
    private CinemachineVirtualCamera _mainCamera;

    [SerializeField]
    private CinemachineVirtualCamera _aimCamera;

    [SerializeField]
    private Image _crossHair;

    private static int _isIdleAimingId = Animator.StringToHash("isAiming");

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        _animator = GetComponent<Animator>();
        _rotationController = GetComponent<PlayerRotationController>();
    }
    
    void Update()
    {
        var rightMouseInput = Input.GetKey(KeyCode.Mouse1);
        var hInput = Input.GetAxis("Horizontal");
        var vInput = Input.GetAxis("Vertical");

        if(rightMouseInput && hInput == 0 && vInput == 0 && _inventory.isGunEquiped(out var gun)) 
        {
            _crossHair.gameObject.SetActive(true);   
            _rotationController.IsAiming(true);
            _mainCamera.gameObject.SetActive(false);
            _aimCamera.gameObject.SetActive(true);
            _animator.SetBool(_isIdleAimingId, true);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var equipedGun = _inventory.GetEquipedGun();
                if(equipedGun != null)
                {
                    equipedGun.Shoot();
                }
            }
        }
        else
        {
            _crossHair.gameObject.SetActive(false);
            _rotationController.IsAiming(false);
            _aimCamera.gameObject.SetActive(false);
            _mainCamera.gameObject.SetActive(true);
            _animator.SetBool(_isIdleAimingId, false);
        }
    }
}
