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

    private bool _isSprinting = false;

    [Header("Jump")]

    [SerializeField]
    private float _jumpForce = 5f;

    private bool _isGrounded = true;

    private Rigidbody _rigidBody;

    private static int IsWalkingParameterId = Animator.StringToHash("isWalking");
    private static int IsRunningParameterId = Animator.StringToHash("isRunning");

    private Animator _animator;

    private GameManager _gameManager;

    //private LifeController _lifeController;

    private void Awake()
    {
        //_rigidBody = gameObject.GetComponent<Rigidbody>();
        _animator = gameObject.GetComponentInChildren<Animator>();
        //_gameManager = FindObjectOfType<GameManager>();
        //_lifeController = gameObject.GetComponent<LifeController>();
    }

    private void Start()
    {
        _jumpForce = 6f;
        //UIController.Instance.SetLife(_lifeController.GetCurrentLife());
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        var inputZ = Input.GetAxis("Vertical");
        var direction = new Vector3(0f, 0f, inputZ);

        if (direction.magnitude == 0)
        {
            _animator.SetBool(IsWalkingParameterId, false);
            _currentSpeed = 0;
            return;
        }
        _animator.SetBool(IsWalkingParameterId, true);
        _currentSpeed = _walkSpeed;

        if (Input.GetKeyDown(KeyCode.LeftShift)) _isSprinting = true;

        else if (Input.GetKeyUp(KeyCode.LeftShift)) _isSprinting = false;

        if (_isSprinting)
            _currentSpeed = _walkSpeed * _sprintMultiplier;

        _animator.SetBool(IsRunningParameterId, _isSprinting);

        direction.Normalize();

        transform.Translate(direction * _currentSpeed * Time.deltaTime, Space.Self);
    }

    private void Jump()
    {
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _isGrounded = false;
            _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)Layers.Floor) _isGrounded = true;
    }

    public float GetSpeed()
    {
        return _currentSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == (int)Layers.ScapePoint)
        {
            _gameManager.FinishedLevel1();
        }
    }
}
