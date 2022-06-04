using UnityEngine;
using System.Collections.Generic;

public class ZombieBWalkController : MonoBehaviour
{
    private Animator _animator;
    private static int _zombieWalkSpeedId = Animator.StringToHash("speed");

    [SerializeField]
    private List<Transform> _waypoints;
    private int _waypointIndex;

    [SerializeField]
    private float _minimumDistanceToWaypoint;

    [SerializeField]
    private int _rotationTowardsWaypointSpeed;

    [SerializeField]
    private float _speed;

    private PlayerDetection _playerDetectionSystem;

    private AudioSource _audioSource;

    private float _timeToPlaySound = 17f;

    private float _counterToPlaySound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _playerDetectionSystem = GetComponent<PlayerDetection>();
        _playerDetectionSystem.OnPlayerDetected += OnPlayerDetectionHandler;

    }

    private void Start()
    {
        _animator.SetFloat(_zombieWalkSpeedId, _speed);
        _counterToPlaySound = Time.time;
    }

    private void Update()
    {
        Patrol();
        PlaySound();
    }

    private void Patrol()
    {
        var initialDistance = _waypoints[_waypointIndex].position - transform.position;

        var direction = initialDistance.normalized;


        transform.LookAt(initialDistance * _rotationTowardsWaypointSpeed);

        Move(direction);

        var distanceToWaypoint = initialDistance.magnitude;

        if (distanceToWaypoint < _minimumDistanceToWaypoint)
            CheckNextWaypoint();
    }

    private void CheckNextWaypoint()
    {
        _waypointIndex++;

        if (_waypointIndex == _waypoints.Count)
            _waypointIndex = 0;
    }

    private void Move(Vector3 direction)
    {
        transform.position += _speed * Time.deltaTime * direction;
    }

    private void OnPlayerDetectionHandler(Transform playerPosition, float playerSpeed)
    {
        this.enabled = false;
    }

    private void PlaySound()
    {
        if(_counterToPlaySound <= Time.time)
        {
            _audioSource.Play();
            _counterToPlaySound = Time.time + _timeToPlaySound;
        }
    }
}
