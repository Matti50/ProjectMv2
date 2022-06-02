using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
    private static int IsTurningId = Animator.StringToHash("isTurning");
    private static int IsTurningLeftId = Animator.StringToHash("isTurningLeft");

    [SerializeField]
    private Transform _followTarget;

    [SerializeField]
    private float _mouseRotationSpeed;

    [SerializeField]
    private float _keyboardRotationSpeed;

    private bool _isAiming = false;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        MouseInputCameraRotate();
        HorizontalInputRotate();
    }

    private void MouseInputCameraRotate()
    {
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");

        if (!_isAiming)
        {
            _followTarget.rotation *= Quaternion.AngleAxis(-mouseY * _mouseRotationSpeed, Vector3.right);
        }
        else
        {
            _followTarget.rotation *= Quaternion.AngleAxis(-mouseY * _mouseRotationSpeed, Vector3.right);
        }

        _followTarget.rotation *= Quaternion.AngleAxis(mouseX * _mouseRotationSpeed, Vector3.up);

        var angles = _followTarget.localEulerAngles;
        angles.z = 0;

        var angle = _followTarget.localEulerAngles.x;

        if (angle > 180 && angle < 340)
            angles.x = 340;
        else if (angle < 180 && angle > 40)
            angles.x = 40;

        _followTarget.localEulerAngles = angles;

        if (_isAiming)
        {
            transform.rotation = Quaternion.Euler(0, _followTarget.rotation.eulerAngles.y, 0);
            _followTarget.localEulerAngles = new Vector3(angles.x, 0, 0);
        }
    }

    private void HorizontalInputRotate()
    {
        var xInput = Input.GetAxis("Horizontal");

        bool isTurning = xInput != 0f;
        if (isTurning)
        {
            _animator.SetBool(IsTurningId, isTurning);
            _animator.SetBool(IsTurningLeftId, xInput < 0f);
        }
        else
        {
            _animator.SetBool(IsTurningId, false);
        }

        //need to smooth this
        transform.Rotate(Vector3.up, xInput * _keyboardRotationSpeed * Time.deltaTime, Space.Self);
    }

    public void IsAiming(bool isAiming) 
    {
        _isAiming = isAiming;
    }
}
