using UnityEngine;
using Assets.Scripts.Utils;
using System.Collections.Generic;

public class ZombieController : MonoBehaviour
{
    [SerializeField]
    private float _walkSpeed;

    [SerializeField]
    private float _rotationSpeed = 5f;

    [SerializeField]
    private float _timeToMakeDamage = 0.5f;

    [SerializeField]
    private float _distanceToForgetPlayer;

    [SerializeField]
    private float _viewDistance;

    [SerializeField]
    private float _minimumDistanceToAttack;

    [SerializeField]
    private float _movementTowardsPlayerPredictionTime;

    [SerializeField]
    private int _damage;

    [SerializeField]
    private List<Transform> _wayPoints;
    private int _waypointIndex;
    private float _minimumDistanceToWaypoint;
    private float _rotationTowardsWaypointSpeed;

    private bool _sawPlayer;

    private Animator _animator;

    private static int SawPlayerParameterId = Animator.StringToHash("sawPlayer");
    private static int AttackPlayerParameterId = Animator.StringToHash("canAttack");

    public Transform _pointOfView;

    private Transform _playerPosition;

    private float _playerSpeed;

    private float _damageTimeCounter;

    private float _timeToDetectPlayer;

    private float _detectionCounter;

    private LifeController _lifeController;

    void Start()
    {
        _minimumDistanceToWaypoint = 4f;
        _waypointIndex = 0;
        _walkSpeed = 3f;
        _damage = 10;
        _timeToDetectPlayer = 0.5f;
        _minimumDistanceToAttack = 6f;
        _viewDistance = 50f;
        _distanceToForgetPlayer = 30f;
        _damageTimeCounter = 0f;
        _detectionCounter = 0f;
        _movementTowardsPlayerPredictionTime = 2f;
        _sawPlayer = false;
        _animator = GetComponent<Animator>();
        _lifeController = GetComponent<LifeController>();
        _rotationTowardsWaypointSpeed = 100f;
    }

    private void Update()
    {
        if (_lifeController.DidIDie()) Destroy(gameObject); //cambiar a ragdoll

        if (_sawPlayer)
        {
            if (DistanceToPlayer() > _minimumDistanceToAttack)
            {
                _animator.SetBool(AttackPlayerParameterId, false);
                _animator.SetBool(SawPlayerParameterId, true);
                ChasePlayer();
            }
            else
            {
                AttackPlayer();
            }
            RotateTowards(_playerPosition.position, _rotationSpeed);
        }
        else
        {
            _animator.SetBool(SawPlayerParameterId, false);
           if(_wayPoints.Count != 0)
           {
                _animator.SetBool(SawPlayerParameterId, true);
                Patrol();
           }
        }
    }

    private void FixedUpdate()
    {
        if (_sawPlayer) return;
        CanSeePlayer();
    }

    private void ChasePlayer()
    {
        var distanceToPlayer = DistanceToPlayer();

        if (distanceToPlayer > _distanceToForgetPlayer && _detectionCounter <= Time.time)
        {
            _detectionCounter = Time.time + _timeToDetectPlayer;
            _sawPlayer = false;
            return;
        }

        var directionMultiplier = _playerSpeed * _movementTowardsPlayerPredictionTime;

        if (directionMultiplier >= distanceToPlayer)
        {
            directionMultiplier = distanceToPlayer / 2;
        }

        Vector3 finitPos = _playerPosition.position + _playerPosition.forward * directionMultiplier;
        Vector3 direction = (finitPos - transform.position).normalized;

        Move(direction);
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    private void CanSeePlayer()
    {
        var hit = Physics.Raycast(_pointOfView.position,_pointOfView.forward,out var hitInfo, _viewDistance);

        if (!hit) return;

        if (hitInfo.collider.gameObject.CompareTag(Tags.Player))
        {
            _playerPosition = hitInfo.transform;
            _playerSpeed = hitInfo.collider.gameObject.GetComponent<PlayerMovementController>().GetSpeed();
            _sawPlayer = true;
        }
    }

    private void Move(Vector3 direction)
    {
        transform.position += SpeedModifier() * Time.deltaTime * direction;
    }

    private void AttackPlayer()
    {
        _animator.SetBool(AttackPlayerParameterId, true);
    }

    private void RotateTowards(Vector3 targetPosition, float rotationSpeed)
    {
        var targetRefDistance = targetPosition - transform.position;
        var newRotation = Quaternion.LookRotation(targetRefDistance);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
    }

    private float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, _playerPosition.position);
    }

    private void Patrol()
    {
        var initialDistance = _wayPoints[_waypointIndex].position - transform.position;

        var direction = initialDistance.normalized;


        transform.LookAt(initialDistance * _rotationTowardsWaypointSpeed);

        Move(direction);

        var distanceToWaypoint = initialDistance.magnitude;

        if(distanceToWaypoint < _minimumDistanceToWaypoint)
            CheckNextWaypoint();
    }

    private void CheckNextWaypoint()
    {
        _waypointIndex++;

        if(_waypointIndex == _wayPoints.Count)
            _waypointIndex = 0;
    }

    protected virtual float SpeedModifier()
    {
        return _walkSpeed;
    }

    protected virtual int DamageModifier()
    {
        return _damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(Tags.Player) && _damageTimeCounter <= Time.time)
        {
            var lifeController = collision.gameObject.GetComponent<LifeController>();
            lifeController.TakeDamage(DamageModifier());
            UIController.Instance.SetLife(lifeController.GetCurrentLife());
            _damageTimeCounter = Time.time + _timeToMakeDamage;
        }
    }
}
