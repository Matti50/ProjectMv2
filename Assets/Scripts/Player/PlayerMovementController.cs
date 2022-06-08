using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement")]

    [SerializeField]
    private float _walkSpeed;

    [SerializeField]
    private float _sprintMultiplier = 1.5f;

    [SerializeField]
    private float _currentSpeed = 0f;

    [SerializeField]
    private Transform _cameraPosition;

    private bool _isSprinting = false;

    private static int IsWalkingParameterId = Animator.StringToHash("isWalking");
    private static int IsRunningParameterId = Animator.StringToHash("isRunning");

    private Animator _animator;
    private AudioSource _audioSource;

    private float _timeToPlaySound = 0.7f;

    private float _soundCounter;

    private CharacterController _controller;

    private float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = gameObject.GetComponentInChildren<Animator>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        _soundCounter = Time.time;
    }

    void Update()
    {
        Move();
        ResetPlayerY();
    }

    private void Move()
    {
        var inputZ = Input.GetAxisRaw("Vertical");
        var inputX = Input.GetAxisRaw("Horizontal");
        var direction = new Vector3(inputX, 0f, inputZ);
        direction.Normalize();

        _animator.SetBool(IsWalkingParameterId, true);
        _currentSpeed = _walkSpeed;

        if (Input.GetKeyDown(KeyCode.LeftShift)) _isSprinting = true;

        else if (Input.GetKeyUp(KeyCode.LeftShift)) _isSprinting = false;

        if (_isSprinting)
            _currentSpeed = _walkSpeed * _sprintMultiplier;

        _animator.SetBool(IsRunningParameterId, _isSprinting);

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cameraPosition.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            _controller.Move(moveDirection.normalized * _currentSpeed * Time.deltaTime);
        }
        else
        {
            _animator.SetBool(IsWalkingParameterId, false);
            _currentSpeed = 0;
            return;
        }

        if (_soundCounter < Time.time)
        {
            _audioSource.Play();
            _soundCounter = Time.time + _timeToPlaySound;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == 14)
        {
            var angle = transform.eulerAngles + 180f * Vector3.up;
            transform.eulerAngles = angle;
        }
    }

    public float GetSpeed()
    {
        return _currentSpeed;
    }

    private void ResetPlayerY()
    {
        if(transform.position.y > 0.01f)
        {
            transform.position.Set(transform.position.x, 0f,transform.position.z); 
        }
    }
}
