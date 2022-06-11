using UnityEngine;
using System.Collections.Generic;

public class ChasePlayerController : MonoBehaviour
{
    private bool _playerDetected;

    private PlayerDetection _playerDetectionSystem;

    private Transform _playerPosition;
    private float _playerSpeed;

    private Animator _animator;
    private static int _zombieWalkSpeedId = Animator.StringToHash("speed");
    private static int _zombieAttackId = Animator.StringToHash("isAttacking");
    private static int _zombieDead = Animator.StringToHash("isDead");

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

    [SerializeField]
    private float _damage;

    [SerializeField]
    private AudioClip _attackSound;
    private AudioClip _defaultSound;

    private AudioSource _audioSource;

    private bool _isAttacking;

    [SerializeField]
    private float _timeToPlayAttackSound;
    private float _counterToPlayAttackSound;

    [SerializeField]
    private float _timeToPlayDefaultSound;
    private float _counterToPlayDefaultSound;

    [SerializeField]
    private float _timeToMakeDamage;
    private float _counterToMakeDamage;

    [SerializeField]
    private GameEvent _damagePlayerEvent;

    private LifeController _lifeController;

    [SerializeField]
    private List<Collider> _colliders;

    [SerializeField]
    private AudioClip _painClip;

    private bool _jobDone;

    [SerializeField]
    private int _zombieId;

    [SerializeField]
    private GameEvent _gotKilled;

    [Header("Testing")]
    public bool killZombie = false;

    private void Awake()
    {
        _lifeController = GetComponent<LifeController>();
        _animator = GetComponent<Animator>();
        _playerDetectionSystem = GetComponent<PlayerDetection>();
        _audioSource = GetComponent<AudioSource>();
        _playerDetectionSystem.OnPlayerDetected += OnPlayerDetectedHandler;
    }

    void Start()
    {
        _defaultSound = _audioSource.clip;
        _counterToPlayAttackSound = Time.time;
        _counterToPlayDefaultSound = Time.time;
        _counterToMakeDamage = Time.time;
        _playerDetected = false;
        _jobDone = false;
    }

    void Update()
    {
        //testing
        if (killZombie && _gotKilled != null) _gotKilled.Raise();

        if (!_playerDetected || _jobDone) return;

        var distanceToPlayer = DistanceToPlayer();

        if(distanceToPlayer >= _distanceToForgetPlayer)
        {
            _playerDetected = false;
            _animator.SetFloat(_zombieWalkSpeedId, 0);
            return;
        }

        if (distanceToPlayer > _distanceToAttackPlayer)
        {
            if (_isAttacking)
            {
                _isAttacking = false;
                _audioSource.clip = _defaultSound;
                _audioSource.Play();
                _counterToPlayDefaultSound = Time.time + _timeToPlayDefaultSound;
                _animator.SetBool(_zombieAttackId, _isAttacking);
            }

            if(_counterToPlayDefaultSound <= Time.time)
            {
                _audioSource.Play();
                _counterToPlayDefaultSound = Time.time + _timeToPlayDefaultSound;
            }
            
            var directionMultiplier = _playerSpeed * _movementTowardsPlayerPredictionTime;

            if (directionMultiplier >= distanceToPlayer)
            {
                directionMultiplier = distanceToPlayer / 2;
            }

            Vector3 finitPos = _playerPosition.position + _playerPosition.forward * directionMultiplier;
            Vector3 direction = (finitPos - transform.position).normalized;

            Move(direction);
        }
        else
        {
            _isAttacking = true;
            if(_counterToPlayAttackSound <= Time.time)
            {
                AttackPlayer();
                _audioSource.clip = _attackSound;
                _audioSource.Play(); 
                _counterToPlayAttackSound = _timeToPlayAttackSound + Time.time;
            }
        }
        RotateTowards(_playerPosition.position, _rotationSpeed);
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
        _animator.SetBool(_zombieAttackId, _isAttacking);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_lifeController.DidIDie()) return;
        if (collision.gameObject.tag == "Player" && _counterToMakeDamage <= Time.time)
        {
            _counterToMakeDamage = _timeToMakeDamage + Time.time;
            _damagePlayerEvent.Raise(new GameEventParam { Damage = _damage });
        }
    }

    public void GetDamage(GameEventParam gameParam)
    {
        if(gameParam.ZombieId != _zombieId)
        {
            return;
        }

        _lifeController.TakeDamage(gameParam.Damage);

        if (_lifeController.DidIDie())
        {
            if(_gotKilled != null)
            {
                _gotKilled.Raise();
            }
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            _colliders.ForEach(col => col.enabled = false);
            _animator.SetBool(_zombieDead, true);
            this.enabled = false;
            return;
        }

        _audioSource.clip = _painClip;
        _audioSource.Play();

        if (!_playerDetected)
        {
            OnPlayerDetectedHandler(gameParam.PlayerPosition, gameParam.PlayerSpeed.Value);
            gameObject.GetComponent<ZombieBWalkController>().enabled = false;
        }
    }

    public void OnPlayerDied()
    {
        _animator.SetFloat(_zombieWalkSpeedId, 0);
        _animator.SetBool(_zombieAttackId, false);
        _jobDone = true;
    }

    public int GetId()
    {
        return _zombieId;
    }

    public void SetId(int id)
    {
        _zombieId = id;
    }
}
