using UnityEngine;

public class ChasePlayerController : MonoBehaviour
{
    private bool _playerDetected;

    private PlayerDetection _playerDetectionSystem;

    private Transform _playerPosition;
    private float _playerSpeed;

    private Animator _animator;
    private static int _zombieWalkSpeedId = Animator.StringToHash("speed");

    [SerializeField]
    private float _chaseSpeed;

    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private float _movementTowardsPlayerPredictionTime;

    [SerializeField]
    private float _distanceToAttackPlayer;

    [SerializeField]
    private float _distanceToForgetPlayer;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerDetectionSystem = GetComponent<PlayerDetection>();

        _playerDetectionSystem.OnPlayerDetected += OnPlayerDetectedHandler;
    }

    void Start()
    {
        _playerDetected = false;
    }


    void Update()
    {
        if (!_playerDetected) return;

        var distanceToPlayer = DistanceToPlayer();

        if(distanceToPlayer >= _distanceToForgetPlayer)
        {
            _playerDetected = false;
            _animator.SetFloat(_zombieWalkSpeedId, 0);
            return;
        }

        if (distanceToPlayer <= _distanceToAttackPlayer)
        {
            var directionMultiplier = _playerSpeed * _movementTowardsPlayerPredictionTime;

            if (directionMultiplier >= distanceToPlayer)
            {
                directionMultiplier = distanceToPlayer / 2;
            }

            Vector3 finitPos = _playerPosition.position + _playerPosition.forward * directionMultiplier;
            Vector3 direction = (finitPos - transform.position).normalized;

            Move(direction);
            RotateTowards(_playerPosition.position, _rotationSpeed);
        }
        else
        {
            AttackPlayer();
        }
    }

    private void Move(Vector3 direction)
    {
        transform.position += _chaseSpeed * Time.deltaTime * direction;
    }

    private void OnPlayerDetectedHandler(Transform playerPosition, float playerSpeed)
    {
        _playerPosition = playerPosition;
        _playerSpeed = playerSpeed;
        _animator.SetFloat(_zombieWalkSpeedId, _chaseSpeed);
        _playerDetected = true;
    }

    private float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, _playerPosition.position);
    }

    private void RotateTowards(Vector3 targetPosition, float rotationSpeed)
    {
        var targetRefDistance = targetPosition - transform.position;
        var newRotation = Quaternion.LookRotation(targetRefDistance);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
    }

    private void AttackPlayer()
    {
        //animator 
        //zombie sound attack
    }
}
