using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
    [SerializeField]
    private Transform _followTarget;

    [SerializeField]
    private float _mouseRotationSpeed;

    private bool _isAiming = false;

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        MouseInputCameraRotate();
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

    public void IsAiming(bool isAiming) 
    {
        _isAiming = isAiming;
    }
}
